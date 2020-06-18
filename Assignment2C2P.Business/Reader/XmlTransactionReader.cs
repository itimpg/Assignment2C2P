using Assignment2C2P.Business.Reader.Interface;
using Assignment2C2P.Business.Validator.Interface;
using Assignment2C2P.Domain;
using Assignment2C2P.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Globalization;
using Assignment2C2P.Business.Model;
using Assignment2C2P.Business.Model.Xml;

namespace Assignment2C2P.Business.Reader
{
    public class XmlTransactionReader : IXmlTransactionReader
    {
        private readonly IXmlTransactionValidator _validator;

        public XmlTransactionReader(IXmlTransactionValidator validator)
        {
            _validator = validator;
        }

        public IList<TransactionItem> Read(Stream stream)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Transactions));
                var transaction = (Transactions)serializer.Deserialize(XmlReader.Create(stream));
                _validator.Validate(transaction.Transaction);

                return transaction.Transaction
                    .Select(t => new TransactionItem
                    {
                        TransactionId = t.id,
                        CurrencyCode = t.PaymentDetails.CurrencyCode,
                        Amount = decimal.Parse(t.PaymentDetails.Amount),
                        TransactionDate = DateTime.ParseExact(t.TransactionDate, "yyyy-MM-ddThh:mm:ss", new CultureInfo("en-US")),
                        Status = StaticValue.XmlStatusList[t.Status]
                    })
                    .ToList();

            }
            catch (InvalidOperationException)
            {
                throw new UnKnowFormatException();
            }
        }
    }
}
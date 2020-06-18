using Assignment2C2P.Business.Model;
using Assignment2C2P.Business.Model.Xml;
using Assignment2C2P.Business.Reader.Interface;
using Assignment2C2P.Business.Validator.Interface;
using Assignment2C2P.Domain;
using Assignment2C2P.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

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
            var result = new List<TransactionItem>();
            var errorList = new List<string>();
            Transactions transaction;
            try
            {
                var serializer = new XmlSerializer(typeof(Transactions));
                transaction = (Transactions)serializer.Deserialize(XmlReader.Create(stream));
            }
            catch (InvalidOperationException)
            {
                throw new UnKnowFormatException();
            }

            if (transaction.Transaction == null)
            {
                throw new TransactionValidateErrorException("Transaction is empty");
            }

            foreach (var t in transaction.Transaction)
            {
                try
                {
                    _validator.Validate(t);

                    result.Add(new TransactionItem
                    {
                        TransactionId = t.id,
                        CurrencyCode = t.PaymentDetails.CurrencyCode,
                        Amount = decimal.Parse(t.PaymentDetails.Amount),
                        TransactionDate = DateTime.ParseExact(t.TransactionDate, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture),
                        Status = StaticValue.XmlStatusList[t.Status]
                    });
                }
                catch (RecordInvalidException ex)
                {
                    errorList.Add(ex.Message);
                }
            }

            if (errorList.Any())
            {
                var errorMessage = string.Join(Environment.NewLine, errorList);
                throw new TransactionValidateErrorException(errorMessage);
            }

            return result;
        }
    }
}
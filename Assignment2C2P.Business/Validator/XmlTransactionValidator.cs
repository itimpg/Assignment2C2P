using Assignment2C2P.Business.Model;
using Assignment2C2P.Business.Model.Xml;
using Assignment2C2P.Business.Validator.Interface;
using Assignment2C2P.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Assignment2C2P.Business.Validator
{
    public class XmlTransactionValidator : IXmlTransactionValidator
    {
        public bool Validate(TransactionsTransaction trans, out string errorMessage)
        {
            var errorList = new List<string>();

            if (trans.id.Length > 50)
            {
                errorList.Add($"> Transaction Id's length should be less than 50 but was {trans.id.Length}");
            }

            if (!decimal.TryParse(trans.PaymentDetails?.Amount, out _))
            {
                errorList.Add($"> Amount should be decimal number but was {trans.PaymentDetails?.Amount}");
            }

            if (!CurrencyHelper.GetCurrencyCodes().Contains(trans.PaymentDetails?.CurrencyCode))
            {
                errorList.Add($"> CurrencyCode should be in ISO4217 format but was {trans.PaymentDetails?.CurrencyCode}");
            }

            if (!DateTime.TryParseExact(trans.TransactionDate, "yyyy-MM-ddTHH:mm:ss", null, DateTimeStyles.None, out _))
            {
                errorList.Add($"> Transaction Date should be in format (yyyy-MM-ddTHH:mm:ss) but was {trans.TransactionDate}");
            }

            if (!StaticValue.XmlStatusList.ContainsKey(trans.Status))
            {
                errorList.Add($"> Status should be (Approved/Rejected/Done) but was {trans.Status}");
            }

            if (errorList.Any())
            {
                errorList.Insert(0, $"Cannot import transaction {trans.id}");
                errorMessage = string.Join(Environment.NewLine, errorList);
                return false;
            }
            else
            {
                errorMessage = string.Empty;
                return true;
            }
        }
    }
}

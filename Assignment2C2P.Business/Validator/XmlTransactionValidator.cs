using Assignment2C2P.Business.Model;
using Assignment2C2P.Business.Model.Xml;
using Assignment2C2P.Business.Validator.Interface;
using Assignment2C2P.Shared;
using Assignment2C2P.Shared.Exceptions;
using System;
using System.Globalization;
using System.Linq;

namespace Assignment2C2P.Business.Validator
{
    public class XmlTransactionValidator : IXmlTransactionValidator
    {
        public void Validate(TransactionsTransaction[] transactions)
        {
            string errorResult = string.Empty;
            foreach (var trans in transactions)
            {
                string errorMessage = string.Empty;

                if (trans.id.Length > 50)
                {
                    errorMessage += $"> Transaction Id's length should be less than 50 but was {trans.id.Length}" + Environment.NewLine;
                }

                if (!decimal.TryParse(trans.PaymentDetails.Amount, out decimal amount))
                {
                    errorMessage += $"> Amount should be decimal number but was {trans.PaymentDetails.Amount}" + Environment.NewLine;
                }

                if (!CurrencyHelper.GetCurrencyCodes().Contains(trans.PaymentDetails.CurrencyCode))
                {
                    errorMessage += $"> CurrencyCode should be in ISO4217 format but was {trans.PaymentDetails.CurrencyCode}" + Environment.NewLine;
                }

                if (!DateTime.TryParseExact(trans.TransactionDate, "yyyy-MM-ddThh:mm:ss", null, DateTimeStyles.None, out DateTime transactionDate))
                {
                    errorMessage += $"> Transaction Date should be in format (yyyy-MM-ddThh:mm:ss) but was {trans.TransactionDate}" + Environment.NewLine;
                }

                if (!StaticValue.XmlStatusList.ContainsKey(trans.Status))
                {
                    errorMessage += $"> Status should be (Approved/Rejected/Done) but was {trans.Status}" + Environment.NewLine;
                }

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    errorResult += $"Cannot import {trans.id}" + Environment.NewLine;
                    errorResult += errorMessage;
                }
            }

            if (!string.IsNullOrEmpty(errorResult))
            {
                throw new TransactionValidateErrorException(errorResult);
            }
        }
    }
}

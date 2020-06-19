using Assignment2C2P.Business.Model;
using Assignment2C2P.Business.Validator.Interface;
using Assignment2C2P.Shared;
using Assignment2C2P.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Assignment2C2P.Business.Validator
{
    public class CsvTransactionValidator : ICsvTransactionValidator
    {
        public bool Validate(string[] fields, out string errorMessage)
        {
            var errorList = new List<string>();

            if (fields[0].Length > 50)
            {
                errorList.Add($"> Transaction Id's length should be less than 50 but was {fields[0].Length}");
            }

            if (!decimal.TryParse(fields[1], out _))
            {
                errorList.Add($"> Amount should be decimal number but was {fields[1]}");
            }

            if (!CurrencyHelper.GetCurrencyCodes().Contains(fields[2]))
            {
                errorList.Add($"> CurrencyCode should be in ISO4217 format but was {fields[2]}");
            }

            if (!DateTime.TryParseExact(fields[3], "dd/MM/yyyy HH:mm:ss", null, DateTimeStyles.None, out _))
            {
                errorList.Add($"> Transaction Date should be in format (dd/MM/yyyy HH:mm:ss) but was {fields[3]}");
            }

            if (!StaticValue.CsvStatusList.ContainsKey(fields[4]))
            {
                errorList.Add($"> Status should be (Approved/Failed/Finished) but was {fields[4]}");
            }

            if (errorList.Any())
            {
                errorList.Insert(0, $"Cannot import transaction {fields[0]}");
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

using Assignment2C2P.Business.Model;
using Assignment2C2P.Business.Reader.Interface;
using Assignment2C2P.Business.Validator.Interface;
using Assignment2C2P.Domain;
using Assignment2C2P.Shared.Exceptions;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Assignment2C2P.Business.Reader
{
    public class CsvTransactionReader : ICsvTransactionReader
    {
        private ICsvTransactionValidator _validator;

        public CsvTransactionReader(ICsvTransactionValidator validator)
        {
            _validator = validator;
        }

        public IList<TransactionItem> Read(Stream stream)
        {
            var result = new List<TransactionItem>();
            var errorList = new List<string>();
            using (TextFieldParser parser = new TextFieldParser(stream, Encoding.Default))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    try
                    {
                        var fields = parser.ReadFields();
                        if (fields.Length != 5)
                        {
                            throw new UnKnowFormatException();
                        }

                        _validator.Validate(fields);

                        result.Add(new TransactionItem
                        {
                            TransactionId = fields[0],
                            Amount = decimal.Parse(fields[1]),
                            CurrencyCode = fields[2],
                            TransactionDate = DateTime.ParseExact(fields[3], "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                            Status = StaticValue.CsvStatusList[fields[4]]
                        });
                    }
                    catch (RecordInvalidException ex)
                    {
                        errorList.Add(ex.Message);
                    }
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

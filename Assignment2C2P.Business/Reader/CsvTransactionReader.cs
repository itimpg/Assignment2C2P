using Assignment2C2P.Business.Reader.Interface;
using Assignment2C2P.Business.Validator.Interface;
using Assignment2C2P.Domain;
using System;
using System.Collections.Generic;
using System.IO;

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
            throw new NotImplementedException();
        }
    }
}

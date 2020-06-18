using System;

namespace Assignment2C2P.Shared.Exceptions
{
    public class TransactionValidateErrorException : Exception
    {
        public TransactionValidateErrorException(string message)
            : base(message)
        { }
    }
}

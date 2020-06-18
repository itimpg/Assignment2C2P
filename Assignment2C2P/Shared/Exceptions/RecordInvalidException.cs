using System;

namespace Assignment2C2P.Shared.Exceptions
{
    public class RecordInvalidException : Exception
    {
        public RecordInvalidException(string message)
            : base(message)
        { }
    }
}

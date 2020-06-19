namespace Assignment2C2P.Business.Validator.Interface
{
    public interface ICsvTransactionValidator
    {
        bool Validate(string[] fields, out string errorMessage);
    }
}

using Assignment2C2P.Business.Model.Xml;

namespace Assignment2C2P.Business.Validator.Interface
{
    public interface IXmlTransactionValidator
    {
        bool Validate(TransactionsTransaction trans, out string errorMessage);
    }
}

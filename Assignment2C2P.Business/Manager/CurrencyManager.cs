using Assignment2C2P.Business.Manager.Interface;
using Assignment2C2P.DataAccess.Repository.Interface;
using System.Collections.Generic;

namespace Assignment2C2P.Business.Manager
{
    public class CurrencyManager : ICurrencyManager
    {
        private ITransactionRepository _repository;

        public CurrencyManager(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public List<string> GetCurrencies()
        {
            return _repository.GetCurrencies();
        }
    }
}

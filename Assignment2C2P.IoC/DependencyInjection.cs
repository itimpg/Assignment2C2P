using Assignment2C2P.Business.Manager;
using Assignment2C2P.Business.Manager.Interface;
using Assignment2C2P.Business.Reader;
using Assignment2C2P.Business.Reader.Interface;
using Assignment2C2P.Business.Validator;
using Assignment2C2P.Business.Validator.Interface;
using Assignment2C2P.DataAccess.Repository;
using Assignment2C2P.DataAccess.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment2C2P.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterIoC(this IServiceCollection services)
        {
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<ITransactionManager, TransactionManager>();
            services.AddTransient<ICurrencyManager, CurrencyManager>();

            services.AddTransient<IXmlTransactionValidator, XmlTransactionValidator>();
            services.AddTransient<ICsvTransactionValidator, CsvTransactionValidator>();
            services.AddTransient<IXmlTransactionReader, XmlTransactionReader>();
            services.AddTransient<ICsvTransactionReader, CsvTransactionReader>();
            services.AddTransient<ITransactionReaderFactory, TransactionReaderFactory>();

            return services;
        }
    }
}

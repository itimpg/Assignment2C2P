using Assignment2C2P.Business.Manager;
using Assignment2C2P.Business.Manager.Interface;
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

            return services;
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DBContext
{
    public class ContextBuilder<T>
        where T : DbContext
    {
        #region public methods
        public T GetContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<T>();
            optionsBuilder.UseSqlServer(connectionString);
            var context = Activator.CreateInstance(typeof(T),
              new object[] { optionsBuilder.Options }) as T;
            return context;
        }
        public static void GetPolarBearSaver9001Context(IConfiguration configuration)
        {
            var contextBuilder = new ContextBuilder<Models.EPC_PolarBearSaver9001Context>();
            Context.polarBearSaver9001Context =  contextBuilder.GetContext(configuration.GetConnectionString(name: "DefaultConnection"));
        }
        #endregion
    }
}
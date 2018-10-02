using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DBContext
{
    public class ContextBuilder
    {
        #region public methods
        private static string _connectionString;

        public static Models.EPC_PolarBearSaver9001Context GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Models.EPC_PolarBearSaver9001Context>();
            optionsBuilder.UseSqlServer(_connectionString);
            var context = Activator.CreateInstance(typeof(Models.EPC_PolarBearSaver9001Context),
              new object[] { optionsBuilder.Options }) as Models.EPC_PolarBearSaver9001Context;
            return context;
        }
        public static void SetPolarBearSaver9001Context(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(name: "DefaultConnection");
        }
        #endregion
    }
}
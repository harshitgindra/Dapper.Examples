using System.Data.Common;

namespace DapperExamples.Abstraction
{
    public interface IDatabaseStrategy
    {
        /// <summary>
        /// Seeding the database with necessary schema and data changes
        /// </summary>
        void SeedDatabase();
        /// <summary>
        /// Get connection to the database
        /// </summary>
        DbConnection Connection { get;}
    }
}

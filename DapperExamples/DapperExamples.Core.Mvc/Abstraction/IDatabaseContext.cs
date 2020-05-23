using System.Data.Common;

namespace DapperExamples.Abstraction
{
    public interface IDatabaseContext
    {
        void SeedDatabase();

        DbConnection Connection { get;}
    }
}

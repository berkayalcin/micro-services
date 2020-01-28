using System.Threading.Tasks;

namespace MicroServices.Common.Mongo
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}
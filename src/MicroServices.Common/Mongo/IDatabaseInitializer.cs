using System.Threading.Tasks;

namespace MicroServices.Common.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}
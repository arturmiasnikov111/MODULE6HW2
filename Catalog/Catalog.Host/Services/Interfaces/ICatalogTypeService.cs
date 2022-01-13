using System.Threading.Tasks;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<int> CreateType(string typeName);
        Task<int> UpdateType(int id, string typeName);
        Task<int> DeleteType(int id);
    }
}
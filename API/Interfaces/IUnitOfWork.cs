using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IDeviceRepository DeviceRepository { get; }
        Task<bool> SaveChangesAsync();
        bool HasChanges();
    }
}
using CMCS.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace CMCS.Data
{
    public interface IDataContext
    {
        DbSet<Claim> Claims { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
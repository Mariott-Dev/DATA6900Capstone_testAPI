using CapAppMauiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapAppMauiClient.DataServices
{
    public interface IRestDataService
    {
        Task<List<CapApp>> GetAllCapAppsAsync();
        Task AddCapAppAsync(CapApp capApp);
        Task UpdateCapAppAsync(CapApp capApp);
        Task DeleteCapAppAsync(int id);
    }
}

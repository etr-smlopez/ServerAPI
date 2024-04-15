using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ServerAPI.Cache;
using ServerAPI.Models;
using System.Xml.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace ServerAPI.Hubs
{
    public class DataHub : Hub
    {
        private readonly IMemoryCache _dataCache;
        private readonly IHubContext<DataHub> _hubContext;

        public DataHub(IMemoryCache  dataCache, IHubContext<DataHub> hubContext)
        {
            _dataCache = dataCache;
            _hubContext = hubContext;
        }
        
        public async Task NotifySalesOrderCacheUpdated()
        {
            if (_dataCache.TryGetValue("SalesOrderCache", out List<SalesOrderModel> updatedData))
            {
                await _hubContext.Clients.All.SendAsync("SalesOrderCacheUpdated", updatedData);
            }
        }

        public async Task NotifyReturnsCacheUpdated()
        {
            if (_dataCache.TryGetValue("ReturnsCache", out List<ReturnsModel> updatedData))
            {
                await _hubContext.Clients.All.SendAsync("ReturnsCacheUpdated", updatedData);
            }
        }

        public async Task NotifyInvoiceCacheupdated()
        {
            if (_dataCache.TryGetValue("InvoiceCache", out List<InvoiceModel> updatedData))
            {
                await _hubContext.Clients.All.SendAsync("InvoiceCacheUpdated", updatedData);
            }
        }
        public async Task NotifyStockTransferCacheUpdated()
        {
            if (_dataCache.TryGetValue("StockTransferCache", out List<StockTransferModel> updatedData))
            {
                await _hubContext.Clients.All.SendAsync("StockTransferCacheUpdated", updatedData);
            }
        }
        public async Task NotifyStockWithdrawalCacheUpdated()
        {
            if (_dataCache.TryGetValue("StockWithdrawalCache", out List<StockWithdrawalModel> updatedData))
            {
                await _hubContext.Clients.All.SendAsync("StockWithdrawalCacheUpdated", updatedData);
            }
        }
    }
}

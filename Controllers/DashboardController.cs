using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ServerAPI.Cache;
using ServerAPI.Hubs;
using ServerAPI.Models;

namespace ServerAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DataCache _dataCache;
        private readonly DataHub _dataHub;

        public DashboardController(DataCache dataCache, DataHub dataHub)
        {
            _dataCache = dataCache;
            _dataHub = dataHub;
        }

        [HttpGet]
        public IActionResult GetCombine()
        {
            var salesorder = _dataCache.GetSalesOrderCachedData();
            var returns = _dataCache.GetReturnsCachedData();
            var invoice = _dataCache.GetInvoiceCachedData();
            var stocktransfer = _dataCache.GetStockTransferCachedData();
            var stockwithdrawal = _dataCache.GetStockWithdrawalCachedData();

            var result = new
            {
                SalesOrder = salesorder,
                Returns = returns,
                Invoice = invoice,
                StockTransfer = stocktransfer 
            //    ,StockWithdrawal = stockwithdrawal
            }; 
            return Ok(result);
        }
   
        [HttpPost("salerorder/{orderId}")]
        public IActionResult UpdateSalesOrderCacheEntry(int orderId, [FromBody] SalesOrderModel updatedData)
        {
            _dataCache.UpdateCache("SalesOrderCache", orderId, updatedData, _dataCache.GetSalesOrderCachedData());
            _dataHub.NotifySalesOrderCacheUpdated();
            return Ok();
        }

        [HttpPost("returns/{orderId}")]
        public IActionResult UpdateReturnCacheEntry(int orderId, [FromBody] ReturnsModel updatedData)
        {
            _dataCache.UpdateCache("ReturnsCache", orderId, updatedData, _dataCache.GetReturnsCachedData());
            _dataHub.NotifyReturnsCacheUpdated();
            return Ok();
        }

        [HttpPost("invoice/{orderId}")]
        public IActionResult UpdateInvoiceCacheEntry(int orderId, [FromBody] InvoiceModel updatedData)
        {
            _dataCache.UpdateCache("InvoiceCache", orderId, updatedData, _dataCache.GetInvoiceCachedData());
            _dataHub.NotifyInvoiceCacheupdated();
            return Ok();
        }

        [HttpPost("stocktransfer/{orderId}")]
        public IActionResult UpdateStockTransferCacheEntry(int orderId, [FromBody] StockTransferModel updatedData)
        {
            _dataCache.UpdateCache("StockTransferCache", orderId, updatedData, _dataCache.GetStockTransferCachedData());
            _dataHub.NotifyStockTransferCacheUpdated();
            return Ok();
        }

        [HttpPost("stockwithdrawal/{orderId}")]
        public IActionResult UpdateStockWithdrawalCacheEntry(int orderId, [FromBody] StockWithdrawalModel updatedData)
        {
            _dataCache.UpdateCache("StockWithdrawalCache", orderId, updatedData, _dataCache.GetStockWithdrawalCachedData());
            _dataHub.NotifyStockWithdrawalCacheUpdated();
            return Ok();
        }
         
    }
}

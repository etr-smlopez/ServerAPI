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

            var result = new Dictionary<string, object>
            {
                { "SalesOrder", salesorder },
                { "Returns", returns },
                { "Invoice", invoice },
                { "StockTransfer", stocktransfer }
               ,{ "StockWithdrawal", stockwithdrawal }
            };
            return Ok(result);
        }
        [HttpGet("salesorder")]
        public IActionResult GetSalesOrders()
        {
            var salesOrder = _dataCache.GetSalesOrderCachedData();
            return Ok(salesOrder);
        }

        [HttpGet("returns")]
        public IActionResult GetReturns()
        {
            var returns = _dataCache.GetReturnsCachedData();
            return Ok(returns);
        }

        [HttpGet("invoice")]
        public IActionResult GetInvoices()
        {
            var invoice = _dataCache.GetInvoiceCachedData();
            return Ok(invoice);
        }

        [HttpGet("stocktransfer")]
        public IActionResult GetStockTransfers()
        {
            var stockTransfer = _dataCache.GetStockTransferCachedData();
            return Ok(stockTransfer);
        }

        [HttpGet("stockwithdrawal")]
        public IActionResult GetStockWithdrawal()
        {
            var stockwithdrawal = _dataCache.GetStockWithdrawalCachedData();
            return Ok(stockwithdrawal);
        }

        [HttpPost("salerorder/{orderId}")]
        public IActionResult UpdateSalesOrderCacheEntry(int orderId, [FromBody] SalesOrderModel updatedData)
        {
            var cachedData = _dataCache.GetSalesOrderCachedData();
 
            if (cachedData.ContainsKey(orderId))
            {
                cachedData[orderId] = updatedData;

                _dataCache.UpdateCache("SalesOrderCache", orderId, updatedData, cachedData);
                _dataHub.NotifySalesOrderCacheUpdated();

                return Ok();
            }
            else
            {
                cachedData.Add(orderId, updatedData);

                _dataCache.UpdateCache("SalesOrderCache", orderId, updatedData, cachedData);
                _dataHub.NotifySalesOrderCacheUpdated();

                return Ok();
            }
        }

        [HttpPost("returns/{orderId}")]
        public IActionResult UpdateReturnCacheEntry(int orderId, [FromBody] ReturnsModel updatedData)
        {
            var cachedData = _dataCache.GetReturnsCachedData();

            if (cachedData.ContainsKey(orderId))
            {
                cachedData[orderId] = updatedData;

                _dataCache.UpdateCache("ReturnsCache", orderId, updatedData, cachedData);
                _dataHub.NotifyReturnsCacheUpdated();

                return Ok();
            }
            else
            {
                cachedData.Add(orderId, updatedData);

                _dataCache.UpdateCache("ReturnsCache", orderId, updatedData, cachedData);
                _dataHub.NotifyReturnsCacheUpdated();

                return Ok();
            }
        }

        [HttpPost("invoice/{orderId}")]
        public IActionResult UpdateInvoiceCacheEntry(int orderId, [FromBody] InvoiceModel updatedData)
        {
            var cachedData = _dataCache.GetInvoiceCachedData();

            if (cachedData.ContainsKey(orderId))
            {
                cachedData[orderId] = updatedData;

                _dataCache.UpdateCache("InvoiceCache", orderId, updatedData, cachedData);
                _dataHub.NotifyInvoiceCacheupdated();

                return Ok();
            }
            else
            {
                cachedData.Add(orderId, updatedData);

                _dataCache.UpdateCache("InvoiceCache", orderId, updatedData, cachedData);
                _dataHub.NotifyInvoiceCacheupdated();

                return Ok();
            }
        }
         
        [HttpPost("stocktransfer/{orderId}")]
        public IActionResult UpdateStockTransferCacheEntry(int orderId, [FromBody] StockTransferModel updatedData)
        {
            var cachedData = _dataCache.GetStockTransferCachedData();

            if (cachedData.ContainsKey(orderId))
            {
                cachedData[orderId] = updatedData;

                _dataCache.UpdateCache("StockTransferCache", orderId, updatedData, cachedData);
                _dataHub.NotifyStockTransferCacheUpdated();

                return Ok();
            }
            else
            {
                cachedData.Add(orderId, updatedData);

                _dataCache.UpdateCache("StockTransferCache", orderId, updatedData, cachedData);
                _dataHub.NotifyStockTransferCacheUpdated();

                return Ok();
            }
        }

        [HttpPost("stockwithdrawal/{orderId}")]
        public IActionResult UpdateStockWithdrawalCacheEntry(int orderId, [FromBody] StockWithdrawalModel updatedData)
        {
            var cachedData = _dataCache.GetStockWithdrawalCachedData();

            if (cachedData.ContainsKey(orderId))
            {
                cachedData[orderId] = updatedData;

                _dataCache.UpdateCache("StockWithdrawalCache", orderId, updatedData, cachedData);
                _dataHub.NotifyStockWithdrawalCacheUpdated();

                return Ok();
            }
            else
            {
                cachedData.Add(orderId, updatedData);

                _dataCache.UpdateCache("StockWithdrawalCache", orderId, updatedData, cachedData);
                _dataHub.NotifyStockWithdrawalCacheUpdated();

                return Ok();
            }
        }
    }
}

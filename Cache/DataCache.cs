using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ServerAPI.Models;
using ServerAPI.SQLAccess;
  
namespace ServerAPI.Cache
{
    public class DataCache
    {
        private readonly IMemoryCache _cache;
        private readonly ApplicationDbContext _dbcontext;

        public DataCache(IMemoryCache cache, ApplicationDbContext dbcontext)
        {
            _cache = cache;
            _dbcontext = dbcontext;
            _dbcontext.Database.EnsureCreated();
        }

        public Dictionary<int, SalesOrderModel> GetSalesOrderCachedData()
        {
            return GetOrCreateCache("SalesOrderCache", () => _dbcontext.SalesOrder.ToDictionary(o => o.SalesOrderKey));
        }
        public Dictionary<int, ReturnsModel> GetReturnsCachedData()
        {
            return GetOrCreateCache("ReturnsCache", () => _dbcontext.Returns.ToDictionary(o => o.ReturnsKey));
        }

        public Dictionary<int, InvoiceModel> GetInvoiceCachedData()
        {
            return GetOrCreateCache("InvoiceCache", () => _dbcontext.Invoice.ToDictionary(o => o.InvoiceKey));
        }

        public Dictionary<int, StockTransferModel> GetStockTransferCachedData()
        {
            return GetOrCreateCache("StockTransferCache", () => _dbcontext.StockTransfer.ToDictionary(o => o.StockTransferKey));
        }

        public Dictionary<int, StockWithdrawalModel> GetStockWithdrawalCachedData()
        {
            return GetOrCreateCache("StockWithdrawalCache", () => _dbcontext.StockWithdrawal.ToDictionary(o => o.StockWithdrawalID));
        }
 
        public void UpdateCache<T>(string cacheKey, int orderId, T updatedData, Dictionary<int, T> existingData) where T : class
        {
            if (existingData.ContainsKey(orderId))
            {
                UpdateExistingData(existingData[orderId], updatedData);
            }
            else
            {
                existingData.Add(orderId, updatedData);
            }

            _cache.Set(cacheKey, existingData, new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.NeverRemove
            });
        }
       
        private Dictionary<int, T> GetOrCreateCache<T>(string cacheKey, Func<Dictionary<int, T>> dataGetter) where T : class
        {
            return _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.SetPriority(CacheItemPriority.NeverRemove);
                return dataGetter();
            });
        }

        private int GetOrderId(object data)
        {
            if (data is SalesOrderModel salesOrder)
            {
                return salesOrder.SalesOrderKey;
            }
            else if (data is ReturnsModel returns)
            {
                return returns.ReturnsKey;
            }
            else if (data is InvoiceModel invoice)
            {
                return invoice.InvoiceKey;
            }
            else if (data is StockTransferModel stockTransfer)
            {
                return stockTransfer.StockTransferKey;
            }
            else if (data is StockWithdrawalModel stockWithdrawal)
            {
                return stockWithdrawal.StockWithdrawalID;
            }
            return -1;
        }

        private void UpdateExistingData<T>(T existingData, T updatedData) where T : class
        {
            if (existingData is SalesOrderModel existingSalesOrder && updatedData is SalesOrderModel updatedSalesOrder)
            {
                existingSalesOrder.OrderStatus = updatedSalesOrder.OrderStatus;
                existingSalesOrder.SO_SONumber = updatedSalesOrder.SO_SONumber;
                existingSalesOrder.SO_SODate = updatedSalesOrder.SO_SODate;
                existingSalesOrder.SO_CreatedBy = updatedSalesOrder.SO_CreatedBy;
            }
            else if (existingData is ReturnsModel existingReturns && updatedData is ReturnsModel updatedReturns)
            {
                existingReturns.Status = updatedReturns.Status;
                existingReturns.RSNumber = updatedReturns.RSNumber;
                existingReturns.ReturnDate = updatedReturns.ReturnDate;
                existingReturns.EmployeeName = updatedReturns.EmployeeName;
            }
            else if (existingData is InvoiceModel existingInvoice && updatedData is InvoiceModel updatedInvoice)
            {
                existingInvoice.Description = updatedInvoice.Description;
                existingInvoice.InvoiceNumber = updatedInvoice.InvoiceNumber;
                existingInvoice.InvoiceDate = updatedInvoice.InvoiceDate;
                existingInvoice.cust_storename = updatedInvoice.cust_storename;
            }
            else if (existingData is StockTransferModel existingStockTransfer && updatedData is StockTransferModel updatedStockTransfer)
            {
                existingStockTransfer.Status = updatedStockTransfer.Status;
                existingStockTransfer.TransferSlipNo = updatedStockTransfer.TransferSlipNo;
                existingStockTransfer.TransferDate = updatedStockTransfer.TransferDate;
                existingStockTransfer.CreatedBy = updatedStockTransfer.CreatedBy;
            }
            else if (existingData is StockWithdrawalModel existingStockWithdrawal && updatedData is StockWithdrawalModel updatedStockWithdrawal)
            {
                existingStockWithdrawal.Status = updatedStockWithdrawal.Status;
                existingStockWithdrawal.TransNo = updatedStockWithdrawal.TransNo;
                existingStockWithdrawal.TransDate = updatedStockWithdrawal.TransDate;
                existingStockWithdrawal.CreatedBy = updatedStockWithdrawal.CreatedBy;
            }
        }
    }
}

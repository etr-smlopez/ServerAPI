using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ServerAPI.Models
{
    public class SalesOrderModel
    {
        [Key]
        public int SalesOrderKey { get; set; }
        public string OrderStatus { get; set; }
        public string SO_SONumber { get; set; }
        public DateTime SO_SODate { get; set; }
        public string SO_CreatedBy { get; set; } 
        
    }
}

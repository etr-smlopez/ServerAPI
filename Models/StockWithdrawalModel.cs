using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ServerAPI.Models
{
    public class StockWithdrawalModel
    {
        [Key]
        public int StockWithdrawalID { get; set; }
        public string Status { get; set; }
        public string TransNo { get; set; }
        public DateTime TransDate { get; set; }
        public string CreatedBy { get; set; }

    }
}

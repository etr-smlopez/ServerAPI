using System.ComponentModel.DataAnnotations;

namespace ServerAPI.Models
{
    public class StockTransferModel
    {
        [Key]
        public int StockTransferKey { get; set; }
        public string Status { get; set; }
        public string TransferSlipNo { get; set; }
        public DateTime TransferDate { get; set; }
        public string CreatedBy { get; set; }
    }
}

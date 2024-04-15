using System.ComponentModel.DataAnnotations;

namespace ServerAPI.Models
{
    public class InvoiceModel
    {
        [Key]
        public int InvoiceKey { get; set; }
        public string Description { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string cust_storename { get; set; }
    }
}

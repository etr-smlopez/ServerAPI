using System.ComponentModel.DataAnnotations;

namespace ServerAPI.Models
{
    public class ReturnsModel
    {
        [Key]
        public int ReturnsKey { get; set; }
        public string Status { get; set; }
        public string RSNumber { get; set; }
        public DateTime ReturnDate { get; set; }
        public string EmployeeName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bulk_Email_Sending_Groupwise.Models
{
    public class EmpMenuMapping
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("EmpId")]
        public int EmpId { get; set; }
        public Employee Employee { get; set; }
        [ForeignKey("MenuID")]
        public int MenuID { get; set; }
        public Menus Menus { get; set; }
    }
}

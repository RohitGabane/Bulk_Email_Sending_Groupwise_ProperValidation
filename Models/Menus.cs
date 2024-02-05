using System.ComponentModel.DataAnnotations;

namespace Bulk_Email_Sending_Groupwise.Models
{
    public class Menus
    {
        [Key]
        public int MenuID { get; set; }
        [Required(ErrorMessage = "MenuName is required")]
        [StringLength(15, ErrorMessage = "MenuName cannot exceed 20 characters.")]
        public string? MenuName { get; set; }
        [Required(ErrorMessage = "ParentID is required")]
        [StringLength(15, ErrorMessage = "ParentID cannot exceed 5 Int")]

        public int? ParentID { get; set; }
        public ICollection<EmpMenuMapping> EmpMenuMapping { get; set; } = new List<EmpMenuMapping>();
    }
}

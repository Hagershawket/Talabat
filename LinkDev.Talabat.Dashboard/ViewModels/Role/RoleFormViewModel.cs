using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Dashboard.ViewModels.Role
{
    public class RoleFormViewModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;
    }
}

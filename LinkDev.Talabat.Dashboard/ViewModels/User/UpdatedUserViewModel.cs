using LinkDev.Talabat.Dashboard.ViewModels.Role;
using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Dashboard.ViewModels.User
{
    public class UpdatedUserViewModel
    {
        [Display(Name = "User Name")]
        public string? UserName { get; set; } = null!;
        public List<RoleViewModel> Roles { get; set; } = null!;
    }
}

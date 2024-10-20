using LinkDev.Talabat.Dashboard.ViewModels.Role;

namespace LinkDev.Talabat.Dashboard.ViewModels.User
{
    public class UpdatedUserViewModel
    {
        public string? UserName { get; set; } = null!;
        public List<RoleViewModel> Roles { get; set; } = null!;
    }
}

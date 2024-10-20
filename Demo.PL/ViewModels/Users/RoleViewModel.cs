using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.Users
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }

        public RoleViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

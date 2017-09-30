using System.ComponentModel.DataAnnotations;

namespace PaniDomu.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
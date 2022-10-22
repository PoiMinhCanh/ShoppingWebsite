using ShoppingWebsite.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace ShoppingWebsite.Model.DTO;

public class CreateAccountDTO
{
    [Required]
    [StringLength(20, MinimumLength = 6)]
    public string UserName { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 6)]
    public string Password { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 6)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string FullName { get; set; }

    [StringLength(50, MinimumLength = 1)]
    [Display(Name = "Secret Key to become admin")]
    public string SecretKey { get; set; }

}

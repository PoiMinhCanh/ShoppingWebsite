using ShoppingWebsite.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace ShoppingWebsite.Model.DTO;

public class LoginDTO
{
    [Required]
    [StringLength(20, MinimumLength = 6)]
    public string UserName { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 6)]
    public string Password { get; set; }

    public bool RememberMe { get; set; }

}

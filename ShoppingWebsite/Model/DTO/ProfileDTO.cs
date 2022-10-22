using ShoppingWebsite.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace ShoppingWebsite.Model.DTO;

public class ProfileDTO
{
    [Required]
    public int AccountID { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 6)]
    public string UserName { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string FullName { get; set; }

    public AccountType Type { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string ContactName { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Address { get; set; }

    [Required]
    [Phone]
    public string Phone { get; set; }

}



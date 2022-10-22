using Microsoft.EntityFrameworkCore;
using ShoppingWebsite.Model.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingWebsite.Model;

/* The Entity Framework Core IndexAttribute was introduced in .NET 5 
 * and is used to create a database index on the column mapped to the specified entity property
 */
[Table("Account")]
[Index(nameof(UserName), IsUnique = true, Name = "Ix_Account_UserName")]
public class Account
{
    [Key]
    public int AccountID { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 6)]
    public string UserName { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 6)]
    public string Password { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string FullName { get; set; }

    public AccountType Type { get; set; }
}



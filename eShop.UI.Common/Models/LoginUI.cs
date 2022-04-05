using System.ComponentModel.DataAnnotations;

namespace eShop.UI.Common.Models;

public class LoginUI
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}

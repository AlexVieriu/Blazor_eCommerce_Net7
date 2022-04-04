using System.ComponentModel.DataAnnotations;

namespace eShop.UI.CustomerPortal.Models;

public class CustomerUI
{
    [Required]
    [Display(Name = "Name")]
    [StringLength(50, ErrorMessage = "Name must be between {1} and {2} characters", MinimumLength = 5)]
    public string CustomerName { get; set; }

    [Required]
    [Display(Name = "Address")]
    [StringLength(50, ErrorMessage = "Address must be between {1} and {2} characters", MinimumLength = 5)]
    public string CustomerAddress { get; set; }

    [Required]
    [Display(Name = "City")]
    [StringLength(50, ErrorMessage = "City must be between {1} and {2} characters", MinimumLength = 5)]
    public string CustomerCity { get; set; }

    [Required]
    [Display(Name = "StateProvince")]
    [StringLength(50, ErrorMessage = "StateProvince must be between {1} and {2} characters", MinimumLength = 5)]
    public string CustomerStateProvince { get; set; }

    [Required]
    [Display(Name = "Country ")]
    [StringLength(50, ErrorMessage = "Country must be between {1} and {2} characters", MinimumLength = 5)]
    public string CustomerCountry { get; set; }
}

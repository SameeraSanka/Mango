using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponAPI.Models;

public class Coupon
{
    [Key]
    public required int CouponID { get; set; }

    [Required]
    public  string? CouponCode { get; set; }

    public double DiscountAmount { get; set; }

    public int MinAmount { get; set; }
}

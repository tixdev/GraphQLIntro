using System.ComponentModel.DataAnnotations;

namespace Balance.API.Models;

public class Balance
{
    [Key]
    public int Id { get; set; }

    public int AssetId { get; set; }

    public decimal Amount { get; set; }
    public required string Currency { get; set; }
}

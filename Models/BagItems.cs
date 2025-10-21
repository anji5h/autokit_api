using System.ComponentModel.DataAnnotations;

namespace AutoKitApi.Models;

public class BagItem
{
    [Key] public int BagItemId { get; set; }

    [Required] public int ProductId { get; set; }

    [Required] public int Compartment { get; set; }

    [Required] [Range(1, int.MaxValue)] public int Quantity { get; set; }

    public Product Product { get; set; } = null!;
}
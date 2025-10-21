using System.ComponentModel.DataAnnotations;

namespace AutoKitApi.Models;

public class Product
{
    [Key] public int ProductId { get; set; }

    [Required] [StringLength(100)] public string Name { get; set; } = string.Empty;

    [StringLength(1000)] public string? Description { get; set; }

    [Required] public int Location { get; set; }

    [Required] [Range(0, int.MaxValue)] public int Quantity { get; set; }

    [Required] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<BagItem> BagItems { get; set; } = new List<BagItem>();
    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();
}
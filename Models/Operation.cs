using System.ComponentModel.DataAnnotations;

namespace AutoKitApi.Models;

public class Operation
{
    [Key] public int OperationId { get; set; }

    [Required] public int BagId { get; set; }

    [Required] public int ProductId { get; set; }

    [Required] [Range(1, int.MaxValue)] public int Quantity { get; set; }

    [Required] public DateTime PackagedAt { get; set; } = DateTime.UtcNow;

    public virtual Bag Bag { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
}
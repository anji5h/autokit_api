using System.ComponentModel.DataAnnotations;
using AutoKitApi.Enums;

namespace AutoKitApi.Models;

public class Bag
{
    [Key] public int BagId { get; set; }

    [Required] public BagStatus Status { get; set; } = BagStatus.Created;

    [StringLength(1000)]
    public string? ShippingAddress { get; set; }

    [Required] public DateTime CreatedAt { get; } = DateTime.UtcNow;

    [Required] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();
}
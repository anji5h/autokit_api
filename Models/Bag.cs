using System.ComponentModel.DataAnnotations;
using AutoKitApi.Enums;

namespace AutoKitApi.Models;

public class Bag
{
    [Key] public int BoxId { get; set; }

    [Required] public BagStatus Status { get; set; } = BagStatus.Created;

    public string? ShippingAddress { get; set; }

    [Required] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Operation> Operations { get; set; } = new List<Operation>();
}
using System.ComponentModel.DataAnnotations;

namespace AutoKitApi.DTOs;

public class ProductCreateDto
{
    [Required] [StringLength(100)] public string Name { get; set; } = string.Empty;

    [StringLength(1000)] public string? Description { get; set; }

    [Required] public int Location { get; set; }

    [Required] [Range(0, int.MaxValue)] public int Quantity { get; set; }
}
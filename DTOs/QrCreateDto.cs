using System.ComponentModel.DataAnnotations;

namespace AutoKitApi.DTOs;

public class QrCreateDto
{
    [Required]
    public int ProductId { get; set; }
    
    [Required]
    public int BagId { get; set; }
    
    [Required]
    public int Quantity { get; set; }
}
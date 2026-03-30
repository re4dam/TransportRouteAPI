using System;
using System.ComponentModel.DataAnnotations; // Add this namespace

namespace TransportRouteApi.DTOs;

public class CreateTransitRouteDto
{
    [Required(ErrorMessage = "Route Name is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Route Name must be between 3 and 100 characters.")]
    public required string RouteName { get; set; }

    [Required(ErrorMessage = "Starting Point is required.")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "Starting Point must be between 2 and 150 characters.")]
    public required string StartingPoint { get; set; }

    [Required(ErrorMessage = "Destination is required.")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "Destination must be between 2 and 150 characters.")]
    public required string Destination { get; set; }
    
    [Required(ErrorMessage = "Starting Hour is required in the payload.")]
    public TimeOnly StartingHour { get; set; }
    
    [Required(ErrorMessage = "Ending Hour is required in the payload.")]
    public TimeOnly EndingHour { get; set; }
}
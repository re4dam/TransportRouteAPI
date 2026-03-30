using System;
using System.ComponentModel.DataAnnotations; // Add this namespace

namespace TransportRouteApi.DTOs;

public class CreateVehicleDto
{
    [Required(ErrorMessage = "Vehicle Name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Vehicle Name must be between 2 and 100 characters.")]
    public required string VehicleName { get; set; }

    // Prevents 0 or negative numbers from hitting the database
    [Range(1, long.MaxValue, ErrorMessage = "A valid, positive Category ID must be selected.")]
    public required long CategoryId { get; set; }

    [Range(1, long.MaxValue, ErrorMessage = "A valid, positive Transit Route ID must be selected.")]
    public required long TransitRouteId { get; set; }
}
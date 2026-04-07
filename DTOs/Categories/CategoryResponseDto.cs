using System;

namespace TransportRouteApi.DTOs;

public class CategoryResponseDto
{
    public long Id { get; set; }
    public required string CategoryName { get; set; }
    public required string Description { get; set; }
    public required string DisplayColor { get; set; }
    
    public List<VehicleResponseDto> Vehicles { get; set; } = new List<VehicleResponseDto>();
}
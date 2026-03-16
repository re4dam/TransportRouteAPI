using System;
namespace TransportRouteApi.DTOs;

public class CreateTransitRouteDto
{
    public required string RouteName { get; set; }
    public required string StartingPoint { get; set; }
    public required string Destination { get; set; }
    
    // Thanks to your custom JsonConverter, these can stay as TimeOnly!
    public TimeOnly StartingHour { get; set; }
    public TimeOnly EndingHour { get; set; }
}
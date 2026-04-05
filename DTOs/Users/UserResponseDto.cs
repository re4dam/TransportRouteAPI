namespace TransportRouteApi.DTOs;

public class UserResponseDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = "User"; // Default role is "User"
    public bool IsBanned { get; set; } // Include ban status in the response
}
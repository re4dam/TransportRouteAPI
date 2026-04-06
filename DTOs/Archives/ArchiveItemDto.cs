namespace TransportRouteApi.DTOs; // Use your actual namespace

public class ArchiveItemDto
{
    public long Id { get; set; }
    public string PrimaryText { get; set; } = string.Empty;
    public string SecondaryText { get; set; } = string.Empty;
    public DateTime? ArchivedAt { get; set; }
    public string? ArchivedBy { get; set; }
}
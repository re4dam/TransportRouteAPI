using System;
using System.Collections.Generic;
namespace TransportRouteApi.DTOs;

// The <T> makes this a generic class so it can wrap any other DTO!
public class PaginatedResponseDto<T>
{
    public required IEnumerable<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}
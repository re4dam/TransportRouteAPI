using System;
using System.ComponentModel.DataAnnotations; // Add this namespace

namespace TransportRouteApi.DTOs;

public class CreateCategoryDto
{
    [Required(ErrorMessage = "Category Name is absolutely required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Category Name must be between 2 and 100 characters.")]
    public required string CategoryName { get; set; }
}
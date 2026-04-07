using System;
using System.ComponentModel.DataAnnotations; // Add this namespace

namespace TransportRouteApi.DTOs;

public class CreateCategoryDto
{
    [Required(ErrorMessage = "Category Name is absolutely required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Category Name must be between 2 and 100 characters.")]
    public required string CategoryName { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 500 characters.")]
    public required string Description { get; set; }

    [Required(ErrorMessage = "Display Color is required.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Display Color must be between 3 and 20 characters.")]
    public required string DisplayColor { get; set; }
}
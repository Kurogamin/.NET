using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet.Models;

public class Game
{
	[Key]
	public int Id { get; set; }

	[Required]
	public string Title { get; set; } = string.Empty;

	[DisplayName("Genre")]
	public int GenreId { get; set; }

	[ForeignKey("GenreId")]
	[ValidateNever]
	public Genre Genre { get; set; }

	public string? Description { get; set; }

	[DisplayName("Studio")]
	public int StudioId { get; set; }

	[ForeignKey("StudioId")]
	[ValidateNever]
	public Studio Studio { get; set; }

	[DisplayName("Image URL")]
	[ValidateNever]
	public string ImageUrl { get; set; } = string.Empty;
}
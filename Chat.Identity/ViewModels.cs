using System.ComponentModel.DataAnnotations;

namespace Chat.Identity;

public record LoginViewModel
{
    [Required]
    public required string Name { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}

public record RegisterViewModel
{
    [Required]
    public required string Name { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public required string ConfirmPassword { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Chat.Identity;

public record LoginViewModel()
{
    [Required]
    public string Name { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}

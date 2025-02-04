using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class StatusTypeRegistrationForm
{
    [Required]
    public string StatusName { get; set; } = null!;
}

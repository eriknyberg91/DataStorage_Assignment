using Data.Entities;

namespace Business.Dtos;

public class ProjectUpdateForm
{
    public string? Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? CustomerId { get; set; }
    public CustomerEntity? Customer { get; set; } = null!;
    public int? StatusId { get; set; }
    public StatusTypeEntity? Status { get; set; } = null!;
    public int? UserId { get; set; }
    public UserEntity? User { get; set; } = null!;
    public int? ProductId { get; set; }
    public ProductEntity? Product { get; set; } = null!;
    public string? ProjectNumber { get; set; } = null!;

}

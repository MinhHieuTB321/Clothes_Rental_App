using UserService.Domain.Enums;

namespace UserService.Application.ViewModels.Users
{
    public class OwnerPublishedModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Role {  get; set; } = RoleEnums.Owner.ToString();
    }
}

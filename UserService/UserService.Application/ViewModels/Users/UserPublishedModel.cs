using UserService.Domain.Enums;

namespace UserService.Application.ViewModels.Users
{
    public class UserPublishedModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Gender{get;set;}=default!;
        public string Address{get;set;}=default!;
        public string Password{get;set;}=default!;
        public string Status{get;set;}="Active";
        public string? Event { get; set; }="Owner_Published";

    }
}

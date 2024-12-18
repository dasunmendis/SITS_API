
namespace SITS_API.Domain.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string?  Gender { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Remark { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}

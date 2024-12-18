namespace SITS_API.Domain.Entities
{
    public class UserDetail
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? UserImage { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}

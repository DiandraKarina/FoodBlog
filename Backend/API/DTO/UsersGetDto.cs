using Domain.Models.Enums;

namespace API.DTO
{
    public class UsersGetDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }
        public int Password { get; set; }
        public Gender Gender { get; set; }
        public DateTime DOB { get; set; }
        public string EmailAddress { get; set; }
        public BlogsGetDto Blog { get; set; }
    }
}

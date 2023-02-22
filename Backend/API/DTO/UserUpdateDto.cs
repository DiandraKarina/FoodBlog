using Domain.Models.Enums;

namespace API.DTO
{
    public class UserUpdateDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DOB { get; private set; }
        public string EmailAddress { get; set; }
    }
}

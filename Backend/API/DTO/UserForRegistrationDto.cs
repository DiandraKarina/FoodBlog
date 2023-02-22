using Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class UserForRegistrationDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime DOB { get; set; }
        public Gender Gender { get; set; }

    }
}

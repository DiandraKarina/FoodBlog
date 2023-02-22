using Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class UserPostDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get; set;
        }
        public DateTime DOB { get; private set; }

        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }
    }
}

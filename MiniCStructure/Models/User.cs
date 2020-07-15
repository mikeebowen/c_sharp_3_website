using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiniCStructureRepository.Models;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MiniCStructure.Models
{
    public class User
    {
        public User()
        {
            this.Classes = new List<ClassDTO>();
        }

        public int UserId { get; set; }
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        [MinLength(2, ErrorMessage = "Email must be at least 2 characters")]
        public string UserEmail { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(2, ErrorMessage = "Password must be at least 2 characters")]
        public string UserPassword { get; set; }
        public bool UserIsAdmin { get; set; }
        public virtual ICollection<ClassDTO> Classes { get; set; }

        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<UserDTO, User>().ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        private static User convertToUser(UserDTO userDTO)
        {
            return mapper.Map<UserDTO, User>(userDTO);
        }
        private static UserDTO convertoToUserDTO(User user)
        {
            return mapper.Map<User, UserDTO>(user);
        }
        public static async Task<User> GetByEmail(string userEmail)
        {
            UserDTO user = await UserDTO.GetByEmail(userEmail);
            return convertToUser(user);
        }
        public static async Task<int> Create(User user)
        {
            int id = await UserDTO.Create(convertoToUserDTO(user));
            return id;
        }
        public static async Task<User> CheckPassword(string pwToCheck, string emailToCheck)
        {
            UserDTO userDTO = await UserDTO.CheckPassword(pwToCheck, emailToCheck);
            return convertToUser(userDTO);
        }
    }
}
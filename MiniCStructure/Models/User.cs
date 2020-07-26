using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiniCStructureRepository.Models;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.Ajax.Utilities;

namespace MiniCStructure.Models
{
    public class User
    {
        public User()
        {
            this.Classes = new List<Class>();
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
        public virtual ICollection<Class> Classes { get; set; }

        private static MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.CreateMap<UserDTO, User>().ReverseMap();
            cfg.CreateMap<ClassDTO, Class>().ReverseMap();
        });
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
        public static async Task<User> AddClass(int classId, int userId)
        {
            UserDTO userDTO = await UserDTO.AddClass(classId, userId);
            return convertToUser(userDTO);
        }
    }
}
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiniCStructureRepository.Models;
using System.Threading.Tasks;

namespace MiniCStructure.Models
{
    public class User
    {
        public User()
        {
            this.Classes = new List<ClassDTO>();
        }

        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public bool UserIsAdmin { get; set; }
        public virtual ICollection<ClassDTO> Classes { get; set; }

        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<UserDTO, User>().ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        private static User convertToUser(UserDTO user)
        {
            return mapper.Map<UserDTO, User>(user);
        }
        public static async Task<User> GetByEmail(string userEmail)
        {
            UserDTO user = await UserDTO.GetByEmail(userEmail);
            return convertToUser(user);
        }
    }
}
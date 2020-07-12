using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MiniCStructureDB;

namespace MiniCStructureRepository.Models
{
    public class UserDTO
    {
        public UserDTO()
        {
            this.Classes = new List<ClassDTO>();
        }

        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public bool UserIsAdmin { get; set; }
        public virtual ICollection<ClassDTO> Classes { get; set; }

        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<User, UserDTO>().ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        private static UserDTO convertToUserDTO(User user)
        {
            return mapper.Map<User, UserDTO>(user);
        }
        private static User convertToUser(UserDTO userDTO)
        {
            return mapper.Map<UserDTO, User>(userDTO);
        }
        public static async Task<UserDTO> GetByEmail(string userEmail)
        {            
            User user = await DatabaseManager.Instance.Users.FirstAsync(u => u.UserEmail == userEmail);
            return convertToUserDTO(user);        
        }
        public static async Task<int> Create(UserDTO userDTO)
        {
            User user = DatabaseManager.Instance.Users.Add(convertToUser(userDTO));
            await DatabaseManager.Instance.SaveChangesAsync();
            return user.UserId;
        }
    }
}

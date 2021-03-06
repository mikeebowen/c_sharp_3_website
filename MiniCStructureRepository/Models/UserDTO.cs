﻿using System;
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

        private static MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.CreateMap<User, UserDTO>().ReverseMap();
            cfg.CreateMap<Class, ClassDTO>().ReverseMap();
        });
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
            if (!DatabaseManager.Instance.Users.Any(u => u.UserEmail == userDTO.UserEmail))
            {
                User user = DatabaseManager.Instance.Users.Add(convertToUser(userDTO));
                await DatabaseManager.Instance.SaveChangesAsync();
                return user.UserId;
            }
            return 0;
        }
        public static async Task<UserDTO> CheckPassword(string pwToCheck, string emailToCheck)
        {
            User user = await DatabaseManager.Instance.Users.FirstOrDefaultAsync(u => u.UserEmail == emailToCheck);
            if (user != null && user.UserPassword == pwToCheck)
            {
                return convertToUserDTO(user);
            }
            else
            {
                return null;
            }
        }
        public static async Task<UserDTO> AddClass(int classId, int userId)
        {
            User user = await DatabaseManager.Instance.Users.FirstAsync(u => u.UserId == userId);
            Class cls = await DatabaseManager.Instance.Classes.FirstAsync(c => c.ClassId == classId);
            user.Classes.Add(cls);
            await DatabaseManager.Instance.SaveChangesAsync();
            return convertToUserDTO(user);
        }
    }
}

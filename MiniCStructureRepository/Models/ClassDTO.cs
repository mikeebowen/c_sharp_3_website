using AutoMapper;
using MiniCStructureDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCStructureRepository.Models
{
    public class ClassDTO
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<Class, ClassDTO>().ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        private static ClassDTO convertToClassDTO(Class clss)
        {
            return mapper.Map<Class, ClassDTO>(clss);
        }
        private static Class convertToClass(ClassDTO classDTO)
        {
            return mapper.Map<ClassDTO, Class>(classDTO);
        }
        public static async Task<List<ClassDTO>> GetAll()
        {
            List<Class> classes = await DatabaseManager.Instance.Classes.ToListAsync();
            return classes.ConvertAll(c => convertToClassDTO(c));
        }
    }
}

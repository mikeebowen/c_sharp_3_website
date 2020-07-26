using AutoMapper;
using MiniCStructureRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MiniCStructure.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        //public string ClassName { get; set; }
        //public string ClassDescription { get; set; }
        //public decimal ClassPrice { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<ClassDTO, Class>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ClassName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ClassDescription))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ClassPrice))
            .ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        private static Class convertToClass(ClassDTO classDTO)
        {
            return mapper.Map<ClassDTO, Class>(classDTO);
        }
        private static ClassDTO convertToClassDTO(Class cls)
        {
            return mapper.Map<Class, ClassDTO>(cls);
        }
        public static async Task<List<Class>> GetAll()
        {
            List<ClassDTO> classDTOs = await ClassDTO.GetAll();
            return classDTOs.ConvertAll(c => convertToClass(c));
        }
        public static async Task<Class> GetById(int id)
        {
            ClassDTO classDTO = await ClassDTO.GetByID(id);
            return convertToClass(classDTO);
        }
    }
}
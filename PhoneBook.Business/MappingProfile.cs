using AutoMapper;
using PhoneBook.Core.Dtos;
using PhoneBook.Core.Entity;

namespace PhoneBook.Business
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //Entity den Dto ya dönüşüm
            CreateMap<Personel, PersonelDto>().ForMember(dest => dest.TamAd, opt => opt.MapFrom(src => $"{src.Ad} {src.Soyad}")); // İki alanı birleştir [cite: 113, 114]

            //Dto dan Entity ye dönüşüm(Kayıt işlemi için)
            CreateMap<PersonelDto, Personel>();

        }
    }
}

using FluentValidation;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Core.Entity;

namespace PhoneBook.Business.Validators
{
    public class PersonelValidator:AbstractValidator<Personel>
    {
        public PersonelValidator()
        {
                RuleFor(p => p.Ad).NotEmpty().WithMessage("Ad alanı boş bırakılamaz.");
                RuleFor(p => p.Soyad).NotEmpty().WithMessage("Soyad alanı boş bırakılamaz.");
                RuleFor(p => p.DahiliNo).NotEmpty().WithMessage("Dahili No alanı boş bırakılamaz.")
                    .Matches(@"^\d{4}$").WithMessage("Dahili No sadece 4 haneli sayı olmalıdır.");
        }
    }
}

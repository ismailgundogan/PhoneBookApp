using FluentValidation;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Core.Dtos;
using PhoneBook.Core.Entity;

namespace PhoneBook.Business.Validators
{
    public class PersonelValidator:AbstractValidator<Personel>
    {
        public PersonelValidator()
        {
            RuleFor(x => x.Ad)
                .NotEmpty().WithMessage("Personel adı boş olamaz.")
                .MaximumLength(50).WithMessage("Tam Ad 50 karakterden fazla olamaz.");

            RuleFor(x => x.Soyad)
                .NotEmpty().WithMessage("Personel soyadı boş olamaz.")
                .MaximumLength(50).WithMessage("Soyad 50 karakterden fazla olamaz.");

            RuleFor(x => x.DahiliNo)
                .NotEmpty().WithMessage("Dahili numara zorunludur.")
                .Matches(@"^[0-9]{4}$").WithMessage("Dahili numara tam olarak 4 haneli bir sayı olmalıdır.");
            // Regex kullanarak sadece rakam ve 4 hane kontrolü yaptık.
        }
    }
}

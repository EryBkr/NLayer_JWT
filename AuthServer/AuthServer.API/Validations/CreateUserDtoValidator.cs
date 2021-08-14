using AuthServer.Core.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.API.Validations
{
    //Validasyon kurallarımı yazıyorum
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(i => i.Email).NotEmpty().WithMessage("Email boş olamaz").EmailAddress().WithMessage("Email formatınız yanlıştır");

            RuleFor(i => i.Password).NotEmpty().WithMessage("Parola alanı boş geçilemez");

            RuleFor(i => i.UserName).NotEmpty().WithMessage("Kullanıcı adı boş olamaz");
        }
    }
}

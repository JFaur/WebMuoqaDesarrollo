using FluentValidation;
using MuoqaIdentidades;

namespace MuoqaBackend.Validations
{
    public class ValidateAccount : AbstractValidator<Account>
    {
        public ValidateAccount() 
        {
            //Contraseña
            RuleFor(x => x.UserPassword).NotEmpty().WithMessage("Porfavor ponga una contraseña")
                .Equal(x => x.UserPasswordR)
                .WithMessage("Las contraseñas deben ser iguales")
                .MaximumLength(30).WithMessage("Son 30 caracteres maximo para la contraseña")
                .MinimumLength(8).WithMessage("Son 8 caracteres minimo para la contraseña");
            //Mail
            RuleFor(x => x.Email).NotEmpty().WithMessage("El mail no puede estar vacio")
                .MaximumLength(50).WithMessage("El mail no puede superar los 50 caracteres");
            //UserName

        }
    }
}

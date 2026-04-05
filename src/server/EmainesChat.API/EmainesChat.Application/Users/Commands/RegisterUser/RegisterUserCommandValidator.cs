using FluentValidation;

namespace EmainesChat.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Nome de usuário é obrigatório.")
            .MaximumLength(50).WithMessage("Nome de usuário não pode ter mais de 50 caracteres.");

        RuleFor(x => x.Name)
            .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.")
            .Matches(@"^[\p{L} ]+$").WithMessage("O nome não pode conter números ou caracteres especiais.")
            .When(x => !string.IsNullOrWhiteSpace(x.Name));

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório.")
            .EmailAddress().WithMessage("Email inválido.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A senha não pode ser vazia.")
            .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.")
            .Matches(@"[a-zA-Z]").WithMessage("A senha deve conter pelo menos uma letra e um número.")
            .Matches(@"[0-9]").WithMessage("A senha deve conter pelo menos uma letra e um número.");
    }
}

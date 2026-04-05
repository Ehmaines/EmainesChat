using FluentValidation;

namespace EmainesChat.Application.Users.Commands.UpdateUserProfile;

public class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
{
    public UpdateUserProfileCommandValidator()
    {
        RuleFor(x => x)
            .Must(x => x.Name is not null || x.Email is not null || x.NewPassword is not null)
            .WithMessage("Nenhum campo para atualizar foi informado.");

        When(x => x.Name is not null, () =>
            RuleFor(x => x.Name!)
                .NotEmpty().WithMessage("O nome não pode ser vazio.")
                .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.")
                .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres."));

        When(x => x.Email is not null, () =>
            RuleFor(x => x.Email!)
                .NotEmpty().WithMessage("Email não pode ser vazio.")
                .EmailAddress().WithMessage("Email inválido."));

        When(x => x.NewPassword is not null, () =>
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("É necessário informar a senha atual para alterá-la.");
            RuleFor(x => x.NewPassword!)
                .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.")
                .Matches(@"[a-zA-Z]").WithMessage("A senha deve conter pelo menos uma letra e um número.")
                .Matches(@"[0-9]").WithMessage("A senha deve conter pelo menos uma letra e um número.");
        });
    }
}

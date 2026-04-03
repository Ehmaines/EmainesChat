using FluentValidation;

namespace EmainesChat.Application.Rooms.Commands.UpdateRoom;

public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
{
    public UpdateRoomCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id da sala inválido.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome da sala é obrigatório.")
            .MaximumLength(100).WithMessage("Nome da sala não pode ter mais de 100 caracteres.");
    }
}

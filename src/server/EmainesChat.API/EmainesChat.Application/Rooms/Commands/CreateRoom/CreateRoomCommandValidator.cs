using FluentValidation;

namespace EmainesChat.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome da sala é obrigatório.")
            .MaximumLength(100).WithMessage("Nome da sala não pode ter mais de 100 caracteres.");
    }
}

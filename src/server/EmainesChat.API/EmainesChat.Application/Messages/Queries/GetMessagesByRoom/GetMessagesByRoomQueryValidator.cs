using FluentValidation;

namespace EmainesChat.Application.Messages.Queries.GetMessagesByRoom;

public class GetMessagesByRoomQueryValidator : AbstractValidator<GetMessagesByRoomQuery>
{
    public GetMessagesByRoomQueryValidator()
    {
        RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("RoomId não pode ser vazio.");
    }
}

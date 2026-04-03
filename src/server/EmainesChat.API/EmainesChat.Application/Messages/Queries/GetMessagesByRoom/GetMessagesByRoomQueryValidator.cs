using FluentValidation;

namespace EmainesChat.Application.Messages.Queries.GetMessagesByRoom;

public class GetMessagesByRoomQueryValidator : AbstractValidator<GetMessagesByRoomQuery>
{
    public GetMessagesByRoomQueryValidator()
    {
        RuleFor(x => x.RoomId)
            .GreaterThan(0).WithMessage("RoomId deve ser maior que zero.");
    }
}

using FluentValidation;

namespace EmainesChat.Application.Rooms.Queries.GetRoomById;

public class GetRoomByIdQueryValidator : AbstractValidator<GetRoomByIdQuery>
{
    public GetRoomByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id da sala deve ser maior que zero.");
    }
}

using FluentValidation;

namespace EmainesChat.Application.Rooms.Queries.GetRoomById;

public class GetRoomByIdQueryValidator : AbstractValidator<GetRoomByIdQuery>
{
    public GetRoomByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id da sala não pode ser vazio.");
    }
}

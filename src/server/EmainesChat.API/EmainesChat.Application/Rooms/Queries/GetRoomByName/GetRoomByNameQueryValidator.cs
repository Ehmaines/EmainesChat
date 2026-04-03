using FluentValidation;

namespace EmainesChat.Application.Rooms.Queries.GetRoomByName;

public class GetRoomByNameQueryValidator : AbstractValidator<GetRoomByNameQuery>
{
    public GetRoomByNameQueryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome da sala é obrigatório.");
    }
}

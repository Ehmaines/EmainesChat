using FluentValidation;

namespace EmainesChat.Application.Messages.Commands.SendMessage;

public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Conteúdo da mensagem é obrigatório.")
            .MaximumLength(2000).WithMessage("Mensagem não pode ter mais de 2000 caracteres.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId inválido.");

        RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("RoomId inválido.");
    }
}

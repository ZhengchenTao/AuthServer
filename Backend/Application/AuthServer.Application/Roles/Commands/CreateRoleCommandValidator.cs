using FluentValidation;

namespace AuthServer.Application.Roles.Commands
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
            RuleFor(x => x.DisplayName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(200);
        }
    }
}

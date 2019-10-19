using FluentValidation;

namespace Furmanov.MVP.Login
{
	public class LoginValidator : AbstractValidator<LoginViewModel>
	{
		public LoginValidator()
		{
			RuleFor(vm => vm.Login)
				.NotEmpty()
				.WithMessage("Логин не может быть пустым.");

			RuleFor(vm => vm.Password)
				.NotEmpty()
				.WithMessage("Пароль не может быть пустым.");
		}
	}
}

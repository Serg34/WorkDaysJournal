using Furmanov.Data.Data;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Furmanov.Services
{
	public static class Email
	{
		private const string Login = "123@yandex.ru";
		private const string Password = "123";
		private const string Head = "<div style=\"font-family: Segoe UI Light, Tahoma, Geneva, Verdana, sans-serif; font-size: 16px\">";

		private const string Signature = @"<br />
			<p>Письмо отправлено автоматически, пожалуйста, не отвечайте на него.</p>
			<br />
			<p>С уважением,</p>
			<p>Служба технической поддержки</p>
			</div>";

		public static async Task SendPass(User toUser)
		{
			var from = new MailAddress(Login, "Служба поддержки");
			var to = new MailAddress(toUser.Email);
			var mail = new MailMessage(from, to)
			{
				Subject = "Авторизация",
				IsBodyHtml = true,
				Body = Head + "<p>Добрый день.</p>" +
					   $"<p>Данные для входа в систему:</p>" +
					   $"<p>Логин: {toUser.Login}</p>" +
					   $"<p>Пароль: {toUser.Password}</p>" +
					   $"<p>Роль: {toUser.RoleName}</p>" +
					   Signature
			};

			using (var smtp = new SmtpClient("smtp.yandex.ru", 25))
			{
				smtp.Credentials = new NetworkCredential(Login, Password);
				smtp.EnableSsl = true;
				await smtp.SendMailAsync(mail);
			}
		}
	}
}

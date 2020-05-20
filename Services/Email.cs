using SC.Common.Model;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SC.Common.Services
{
	public static class Email
	{
		private const string Login = "infomsu13@yandex.ru";
		private const string Password = "roxkun90";
		private const string Head = "<div style=\"font-family: Segoe UI Light, Tahoma, Geneva, Verdana, sans-serif; font-size: 16px\">";

		private const string Signature = @"<br />
			<p>Письмо отправлено автоматически, пожалуйста, не отвечайте на него.</p>
			<br />
			<p>С уважением,</p>
			<p>Служба технической поддержки SCAS</p>
			</div>";

		public static async Task SendRequestOp(string toLogin, CUserDto fromUser, CRequestOpDto requestOp)
		{
			var from = new MailAddress(Login, "Служба поддержки SCAS");
			var to = new MailAddress(toLogin);
			var mail = new MailMessage(from, to)
			{
				Subject = "Запрос на получение денег",
				IsBodyHtml = true,
				Body = Head + "<p>Добрый день.</p>" +
					   $"<p>Вам пришёл запрос на получение денег:</p>" +
					   $"<p>Отправитель: {fromUser.Name} ({fromUser.Login})</p>" +
					   $"<p>Дата: {requestOp.DateTime:f}</p>" +
					   $"<p>Сумма: {requestOp.Amount:c2}</p>" +
					   $"<p>Комментарий: {requestOp.Comment}</p>" +
					   Signature
			};

			using (var smtp = new SmtpClient("smtp.yandex.ru", 25))
			{
				smtp.Credentials = new NetworkCredential(Login, Password);
				smtp.EnableSsl = true;
				await smtp.SendMailAsync(mail);
			}
		}

		public static async Task SendPass(CUser toUser)
		{
			var from = new MailAddress(Login, "Служба поддержки SCAS");
			var to = new MailAddress(toUser.Email);
			var mail = new MailMessage(from, to)
			{
				Subject = "Авторизация",
				IsBodyHtml = true,
				Body = Head + "<p>Добрый день.</p>" +
				       $"<p>Данные для входа в систему SCAS:</p>" +
				       $"<p>Логин: {toUser.Login}</p>" +
				       $"<p>Пароль: {toUser.Pass}</p>" +
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

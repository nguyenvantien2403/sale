using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Sale.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Sale.Domain;

namespace Sale.Service.EmailService
{
	public class EmailService : IEmailService
	{
		private const string templatePath = @"Template/{0}.html";
		private readonly SaleContext _context;
		private readonly SMTP _smtp;
		private readonly UserManager<IdentityUser> _userManager;


		public EmailService(SaleContext context, IOptions<SMTP> options, UserManager<IdentityUser> userManager)
		{
			_context = context;
			_smtp = options.Value;
			_userManager = userManager;
		}
		// doc noi dung gui email
		public string GetEmailBody(string templatename)
		{
			var bodyEmail = File.ReadAllText(string.Format(templatePath, templatename));
			return bodyEmail;
		}
		//dynamic data in tempalte
		public string UpdatePlaceHolder(string text, List<KeyValuePair<string, string>> keyValuePairs)
		{
			if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
			{
				foreach (var placeholder in keyValuePairs)
				{
					if (text.Contains(placeholder.Key))
					{
						text = text.Replace(placeholder.Key, placeholder.Value.ToString());
					}
				}
			}
			return text;
		}
		// tao model to send 1 mail
		public async Task SendToEmail(UserEmailOption userEmailOption)
		{
			// tao 1 mail message;
			MailMessage mail = new MailMessage
			{
				Subject = userEmailOption.subject,
				Body = userEmailOption.body,
				From = new MailAddress(_smtp.SenderAddress, _smtp.SenderDisplayName),
				IsBodyHtml = _smtp.IsBodyHTML
			};
			// gui den nhieu email cung 1 luc
			// foreach (var toEmail in userEmailOption.toEmails) 
			// {
			//     mail.To.Add(toEmail);
			// }

			//tao networkcreden
			mail.To.Add(userEmailOption.toEmails);

			NetworkCredential networkCredential = new NetworkCredential(_smtp.UserName, _smtp.Password);

			//tao smtopclient
			SmtpClient smtpClient = new SmtpClient()
			{
				Host = _smtp.Host,
				Port = _smtp.Port,
				EnableSsl = _smtp.EnableSSL,
				UseDefaultCredentials = _smtp.UseDefaultCredentials,
				Credentials = networkCredential
			};
			await smtpClient.SendMailAsync(mail);
		}

		public async Task<bool> GereratePasswordEmailToken(IdentityUser user)
		{
			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			if (token != null)
			{
				UserEmailOption sendEmail = new UserEmailOption();
				sendEmail.toEmails = user.Email;
				var text = GetEmailBody("SendEmail");
				sendEmail.subject = "This is email to verify email to reset password";
				sendEmail.keyValuePairs = new List<KeyValuePair<string, string>>()
				{
					new KeyValuePair<string, string>("{{user_name}}", user.UserName),
					new KeyValuePair<string, string>("{{link}}", token)

				};
				sendEmail.body = UpdatePlaceHolder(text, sendEmail.keyValuePairs);
				await SendToEmail(sendEmail);
				return true;
			}
			return false;
		}

		public async Task<bool> SendMailToReset(string email)
		{
			try
			{
				IdentityUser user = await _userManager.FindByEmailAsync(email);
				if (user == null)
				{
					return false;
				}
				else
				{
					var res = await GereratePasswordEmailToken(user);
					return res;
				}
			}
			catch
			{
				return false;
			}
		}
		public async Task<IdentityResult> ChangePassWord(ResetPassword resetmodel)
		{
			try
			{
				IdentityUser user = await _userManager.FindByIdAsync(resetmodel.username);
				if (user != null)
				{

					var tokenUser = await _userManager.GeneratePasswordResetTokenAsync(user);
					var rs = await _userManager.ResetPasswordAsync(user, tokenUser, resetmodel.newPassword);
					return rs;
				}
				return null;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}

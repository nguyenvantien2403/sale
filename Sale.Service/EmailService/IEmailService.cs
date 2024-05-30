using Microsoft.AspNetCore.Identity;
using Sale.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.EmailService
{
	public interface IEmailService
	{
		//doc noi dung email
		string GetEmailBody(string templatename);
		//sendemail
		Task<bool> SendMailToReset(string email);
		Task SendToEmail(UserEmailOption userEmailOption);
		string UpdatePlaceHolder(string text, List<KeyValuePair<string, string>> keyValuePairs);
		Task<IdentityResult> ChangePassWord(ResetPassword resetmodel);
	}
}

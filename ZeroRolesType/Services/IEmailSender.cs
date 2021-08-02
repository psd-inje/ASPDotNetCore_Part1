using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeroRolesType.Services
{
    //public class IEmailSender
    //{

    //}

    public class EmailSender : IEmailSender
    {
        //public EmailSender()
        //{

        //}
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
            //throw new NotImplementedException();
        }
    }
}

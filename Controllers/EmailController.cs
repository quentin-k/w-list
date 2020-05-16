using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using w_list.ViewModels;
using System.Net.Mail;
using MimeKit;
using System.Net;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;

namespace w_list.Controllers
{
    public class EmailConroller : Controller
    {
        public string SendEmailConfirmation() 
        {
            return "Temp";
        }
        public string GeneratePasswordRecoveryToken()
        {
            return  "82215-AKZDW-qkeep-8Yi";
        }
        public string SendEmailToken()
        {
            return "12345";
        }
    }
}
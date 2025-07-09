using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mailing.Dto;

public class ToEmail
{
    public string Email { get; set; }
    public string FullName { get; set; }

    public ToEmail()
    {
        Email = string.Empty;
        FullName = string.Empty;
    }

    public ToEmail(string email, string fullName)
    {
        Email = email;
        FullName = fullName;
    }
}
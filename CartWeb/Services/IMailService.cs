﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartWeb.Services
{
    public interface IMailService
    {
        void SendMessage(string to, string subject, string body);
    }
}

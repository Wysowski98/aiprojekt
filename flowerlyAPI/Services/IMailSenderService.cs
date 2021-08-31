using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IMailSenderService
    {
        void SendMail(int flower, string jobId);
    }
}

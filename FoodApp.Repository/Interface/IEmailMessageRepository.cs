using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Repository.Interface
{
    public interface IEmailMessageRepository
    {
        void Update(EmailMessage entity);
    }
}

using FoodApp.Models.Models;
using FoodApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Service.Events
{
    public class OrderCompletionNotifier
    {
        public event OrderCompletedEventHandler OrderCompleted;

        private readonly IBackgroundEmailSender _backgroundEmailSender;

        public OrderCompletionNotifier(IBackgroundEmailSender backgroundEmailSender)
        {
            _backgroundEmailSender = backgroundEmailSender;
        }

        public async void NotifyOrderCompleted(Order completedOrder)
        {
            // Raise the OrderCompleted event
            OrderCompleted?.Invoke(completedOrder);

            // Start the background worker to send emails
            await _backgroundEmailSender.DoWork();


        }
    }
}

using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Service.Events
{
    public delegate void OrderCompletedEventHandler(Order completedOrder);

}

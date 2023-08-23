using System;
using System.Collections.Generic;
using System.Text;

namespace FundooSub.Interfaces
{
    public interface IRabbitMQSubscriber
    {
        void ConsumeMessages();
    }
}

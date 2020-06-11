using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Policies
{
    public class BuyersRemorseState : ContainSagaData
    {
        public string OrderId { get; set; }
    }
}

using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Commands
{
    public class CancelOrder : ICommand
    {
        public string OrderId { get; set; }
    }
}

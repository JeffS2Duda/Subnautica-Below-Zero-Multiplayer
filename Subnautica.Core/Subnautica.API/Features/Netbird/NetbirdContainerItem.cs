using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Subnautica.API.Extensions;

namespace Subnautica.API.Features.Netbird
{
    public class NetbirdContainerItem
    {
        public bool IsConnected { get; private set; }

        public string ErrorMessage { get; private set; }

        public void SetConnected(bool isConnected)
        {
            this.IsConnected = isConnected;
        }

        public void SetErrorMessage(string errorMessage)
        {
            if (errorMessage.IsNotNull())
            {
                this.ErrorMessage = errorMessage;
            }
        }

        public bool IsAnyError()
        {
            return this.ErrorMessage.IsNotNull();
        }
    }
}

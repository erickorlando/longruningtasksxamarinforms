using System;

namespace MyGPSLogic.Messages
{
    // Mensaje que informa que el servicio se detuvo
    public class CancelledMessage
    {
        public DateTime DateTime { get; set; }

        public override string ToString()
        {
            return $"Servicio detenido a las {DateTime:G}";
        }
    }
}
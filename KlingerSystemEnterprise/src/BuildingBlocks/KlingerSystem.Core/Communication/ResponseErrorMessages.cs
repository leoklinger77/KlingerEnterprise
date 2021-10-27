using System.Collections.Generic;

namespace KlingerSystem.Core.Communication
{
    public class ResponseErrorMessages
    {
        public List<string> Messagens { get; set; }

        public ResponseErrorMessages()
        {
            Messagens = new List<string>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Common.Messages
{
    public class CitiesRequest : Request
    {
        public CitiesRequest(Guid requestId) : base(requestId)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Common.Messages
{
    public class DepartureCitiesRequest : Request
    {
        public DepartureCitiesRequest(Guid requestId) : base(requestId)
        {
        }
    }
}

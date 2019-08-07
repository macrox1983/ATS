using ATS.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Common.Messages
{
    public class HotelsResponse : Response<Hotel>
    {
        public HotelsResponse(Guid requestId, string providerName) : base(requestId, providerName)
        {
        }
    }
}

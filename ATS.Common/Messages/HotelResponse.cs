using ATS.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Common.Messages
{
    public class HotelResponse : Response<Hotel>
    {
        public HotelResponse(Guid requestId, string providerName) : base(requestId, providerName)
        {
        }
    }
}

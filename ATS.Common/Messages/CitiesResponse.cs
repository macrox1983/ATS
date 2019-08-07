using ATS.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Common.Messages
{
    public class CitiesResponse : Response<City>
    {
        public CitiesResponse(Guid requestId, string providerName) : base(requestId, providerName)
        {
        }
    }
}

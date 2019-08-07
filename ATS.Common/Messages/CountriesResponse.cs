using ATS.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Common.Messages
{
    public class CountriesResponse : Response<Country>
    {
        public CountriesResponse(Guid requestId, string providerName) : base(requestId, providerName)
        {
        }
    }
}

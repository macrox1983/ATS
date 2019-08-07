using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Common.Messages
{
    public class CountriesRequest : Request
    {
        public CountriesRequest(Guid requestId) : base(requestId)
        {
        }
    }
}

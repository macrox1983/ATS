using ATS.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Common.Messages
{
    public class ToursResponse: Response<Tour>
    {
        public ToursResponse(Guid requestId, string providerName) : base(requestId, providerName)
        {

        } 
    }
}

using ATS.Common.Model;
using System;

namespace ATS.Common.Messages
{
    public class DepartureCitiesResponse : Response<City>
    {
        public DepartureCitiesResponse(Guid requestId, string providerName) : base(requestId, providerName)
        {
        }
    }
}

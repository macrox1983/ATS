using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Common.Messages
{
    public class Response<T> : IResponse
    {
        public Response(Guid requestId, string providerName)
        {
            RequestId = requestId;
            ProviderName = providerName;
        }

        public Guid RequestId { get; private set; }

        public List<T> Data { get; set; }

        public string ProviderName { get; private set; }
    }
}

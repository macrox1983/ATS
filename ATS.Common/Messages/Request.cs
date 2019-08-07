using System;

namespace ATS.Common.Messages
{
    public class Request:IRequest
    {
        public Request(Guid requestId)
        {
            RequestId = requestId;
        }

        public Guid RequestId { get; private set; }
    }
}

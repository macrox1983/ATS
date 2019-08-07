using System;

namespace ATS.Common.Messages
{
    public interface IRequest:IMessage
    {
        Guid RequestId { get; }
    }
}

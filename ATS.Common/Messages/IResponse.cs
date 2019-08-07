using System;

namespace ATS.Common.Messages
{
    public interface IResponse : IMessage
    {
        Guid RequestId { get; }

        string ProviderName { get; }
    }
}

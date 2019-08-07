using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Common.Messages
{
    public class ToursRequest:Request
    {
        public ToursRequest(Guid requestId):base(requestId)
        {

        }

        public SearchPattern SearchPattern { get; set; }
    }
}

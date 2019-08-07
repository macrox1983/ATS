using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Common.Messages
{
    public class HotelRequest : Request
    {
        public HotelRequest(Guid requestId, int hotelId) : base(requestId)
        {
            HotelId = hotelId;
        }

        public int HotelId { get; }
    }
}

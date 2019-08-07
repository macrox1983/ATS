using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ATS.Common.DataModel;

namespace ATS.OtherProvider.Abstraction
{

    public interface IOtherDataContext
    {
        Dictionary<int, Airline> Airlines { get; }

        Dictionary<int, City> Cities { get; }

        Dictionary<int, Country> Countries { get; }

        Dictionary<int, Hotel> Hotels { get; }

        Dictionary<int, Provider> Providers { get; }

        Dictionary<int, RoomType> RoomTypes { get; }

        Dictionary<int, Tour> Tours { get; }

        Task InitializeDataContext(int cityCount, int hotelCount, int tourCount, int airlineCount);
    }
}

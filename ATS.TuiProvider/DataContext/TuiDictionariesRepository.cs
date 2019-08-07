using ATS.Common.DataModel;
using ATS.TuiProvider.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.TuiProvider.DataContext
{
    public class TuiDictionariesRepository : Repository, ITuiDictionariesRepository
    {
        public TuiDictionariesRepository(ITuiDataContext dataContext):base(dataContext)
        {
        }

        public async Task<List<City>> GetAllCities()
        {
            return await Task.FromResult(_dataContext.Cities.Values.ToList());
        }

        public async Task<List<Country>> GetAllCountries()
        {
            return await Task.FromResult(_dataContext.Countries.Values.ToList());
        }

        public async Task<List<City>> GetAllDepartureCities()
        {
            return await Task.FromResult(_dataContext.Tours.Values.Select(tour=>tour.DepartureCity).Distinct().ToList());
        }

        public async Task<List<Hotel>> GetAllHotels()
        {
            return await Task.FromResult(_dataContext.Hotels.Values.ToList());
        }

        public async Task<Hotel> GetHotel(int id)
        {
            if(_dataContext.Hotels.ContainsKey(id))
                return await Task.FromResult(_dataContext.Hotels[id]);
            return default(Hotel);
        }
    }
}

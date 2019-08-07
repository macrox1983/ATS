using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATS.Common;
using ATS.Common.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Controllers
{    
    public class DictionariesController : ControllerBase
    {
        private readonly IAggregateDictionariesService _dictionariesService;

        public DictionariesController(IAggregateDictionariesService dictionariesService)
        {
            _dictionariesService = dictionariesService ?? throw new ArgumentNullException(nameof(dictionariesService));
        }

        [HttpGet]
        public async Task<List<City>> DepartureCities()
        {
            return await _dictionariesService.GetAllDepartureCities();
        }

        [HttpGet]
        public async Task<List<Country>> Countries()
        {
            return await _dictionariesService.GetAllCountries();
        }

        [HttpGet]
        public async Task<List<City>> Cities()
        {
            return await _dictionariesService.GetAllCities();
        }

        [HttpGet]
        public async Task<List<Hotel>> Hotels()
        {
            return await _dictionariesService.GetAllHotels();
        }

        [HttpGet]
        public async Task<Hotel> Hotel([FromRoute]int id)
        {
            return await _dictionariesService.GetHotel(id);
        }
    }
}
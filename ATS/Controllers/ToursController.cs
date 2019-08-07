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
    public class ToursController : ControllerBase
    {
        private readonly IAggregateSearchService _searchService;

        public ToursController(IAggregateSearchService searchService)
        {
            _searchService = searchService??throw new ArgumentNullException(nameof(searchService));
        }

        [HttpGet]
        public async Task<List<Tour>> Search([FromQuery] SearchPattern pattern)
        {
            return await _searchService.Search(pattern);
        }
    }
}
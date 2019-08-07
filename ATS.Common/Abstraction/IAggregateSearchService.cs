using ATS.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Common
{
    public interface IAggregateSearchService
    {
        Task<List<Tour>> Search(SearchPattern searchPattern);
    }
}

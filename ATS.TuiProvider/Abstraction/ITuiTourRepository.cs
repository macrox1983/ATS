using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ATS.Common;
using ATS.Common.DataModel;
using ATS.Common.Specification;

namespace ATS.TuiProvider.Abstraction
{    
    public interface ITuiTourRepository:IRepository
    {
        Task<List<Tour>> Search(Specification<Tour> specification);        
    }
}

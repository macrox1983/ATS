﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Common;
using ATS.Common.DataModel;
using ATS.Common.Specification;
using ATS.OtherProvider.Abstraction;
using ATS.OtherProvider.DataContext;

namespace ATS.OtherProvider
{
    public class OtherTourRepository : Repository, IOtherTourRepository
    {
        public OtherTourRepository(IOtherDataContext dataContext):base(dataContext)
        {
        }

        public async Task<List<Tour>> Search(Specification<Tour> specification)
        {
            return await Task.FromResult(_dataContext.Tours.Values.Where(specification.IsSatisfied).ToList());
        }
    }
}

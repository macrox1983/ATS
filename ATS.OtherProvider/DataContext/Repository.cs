using ATS.OtherProvider.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATS.OtherProvider.DataContext
{
    public class Repository: IRepository
    {
        protected readonly IOtherDataContext _dataContext;
        public Repository(IOtherDataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task InitializeDataContext()
        {
            await _dataContext.InitializeDataContext(1000, 10000, 1000000, 100);
        }
    }
}

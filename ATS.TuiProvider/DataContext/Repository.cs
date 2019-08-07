using ATS.TuiProvider.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATS.TuiProvider.DataContext
{
    public class Repository: IRepository
    {
        protected readonly ITuiDataContext _dataContext;
        public Repository(ITuiDataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task InitializeDataContext()
        {
            await _dataContext.InitializeDataContext(1000, 10000, 1000000, 100);
        }
    }
}

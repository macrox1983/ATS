using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATS.OtherProvider.Abstraction
{
    public interface IRepository
    {
        Task InitializeDataContext();
    }
}

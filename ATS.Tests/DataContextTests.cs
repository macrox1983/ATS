using ATS.TuiProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.Common;
using ATS.TuiProvider.Abstraction;
using ATS.TuiProvider.DataContext;

namespace ATS.Tests
{
    [TestClass]
    public class DataContextTests
    {
        [TestMethod]
        public void TestDataContextInitializing()
        {
            ITuiDataContext tuiContext = new TuiDataContext();

            tuiContext.InitializeDataContext(1000, 10000, 1000000, 100);

            Assert.IsTrue(tuiContext.Airlines.Count > 0);
            Assert.IsTrue(tuiContext.Countries.Count > 0);
            Assert.IsTrue(tuiContext.Cities.Count > 0);
            Assert.IsTrue(tuiContext.Hotels.Count > 0);
            Assert.IsTrue(tuiContext.Providers.Count > 0);
            Assert.IsTrue(tuiContext.RoomTypes.Count > 0);
            Assert.IsTrue(tuiContext.Tours.Count > 0);
            Assert.IsTrue(tuiContext.Tours.Where(t=>t.Value.ArrivalDate > t.Value.DepartureDate).Count()>0);
        }
    }
}

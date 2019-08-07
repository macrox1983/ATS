using ATS.AggregateDictionaryService;
using ATS.Common;
using ATS.MessageBus;
using ATS.OtherProvider;
using ATS.OtherProvider.DI;
using ATS.TuiProvider;
using ATS.TuiProvider.DI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATS.Tests
{
    [TestClass]
    public class DictionariesTests
    {
        class Options : IOptions<AppSettings>
        {
            public AppSettings Value => new AppSettings
            {
                DifferenceBetweenProviderPrice = 5,
                PreferredProvider = "tuiprovider",
                RequestTimeout = 15000
            };
        }

        [TestMethod]
        public async Task TestDictionaries()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IOptions<AppSettings>, Options>()
                .AddMessageBusClient()
                .AddAggregateDictionariesService()
                .AddTuiProvider()
                .AddOtherProvider();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var tuiService = serviceProvider.GetService<TuiSearchService>();
            await tuiService.StartAsync(new CancellationTokenSource().Token);

            var otherService = serviceProvider.GetService<OtherSearchService>();
            await otherService.StartAsync(new CancellationTokenSource().Token);

            var dictionaryService = serviceProvider.GetService<AggregateDictionariesService>();

            await dictionaryService.StartAsync(new CancellationTokenSource().Token);

            var cities = await dictionaryService.GetAllCities();
            var hotels = await dictionaryService.GetAllHotels();
            var countries = await dictionaryService.GetAllCountries();
            var departureCities = await dictionaryService.GetAllDepartureCities();
            var hotel = await dictionaryService.GetHotel(5);

            Assert.IsNotNull(cities);
            Assert.IsTrue(cities.Count > 0);

            Assert.IsNotNull(hotels);
            Assert.IsTrue(hotels.Count > 0);

            Assert.IsNotNull(countries);
            Assert.IsTrue(countries.Count > 0);

            Assert.IsNotNull(departureCities);
            Assert.IsTrue(departureCities.Count > 0);

            Assert.IsNotNull(hotel);
        }
    }
}

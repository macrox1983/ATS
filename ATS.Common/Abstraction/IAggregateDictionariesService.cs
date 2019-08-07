using ATS.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Common
{
    public interface IAggregateDictionariesService
    {
        /// <summary>
        /// Метод возвращает все города вылета из списка туров
        /// </summary>
        /// <returns>Список городов вылета</returns>
        Task<List<City>> GetAllDepartureCities();

        /// <summary>
        /// Метод возвращает все страны из справочника "Страны"
        /// </summary>
        /// <returns>Список стран</returns>
        Task<List<Country>> GetAllCountries();

        /// <summary>
        /// Метод возвращает все города из справочника "Города"
        /// </summary>
        /// <returns></returns>
        Task<List<City>> GetAllCities();

        /// <summary>
        /// Метод возвращает все отели
        /// </summary>
        /// <returns></returns>
        Task<List<Hotel>> GetAllHotels();

        /// <summary>
        /// Метод возвращает отель по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Hotel> GetHotel(int id);

    }
}

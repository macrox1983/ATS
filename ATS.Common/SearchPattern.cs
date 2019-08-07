using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Common
{
    // Класс шаблон для поиска с доступными полями поиска.
    // Используем такой подход т.к. в задании явно не указано
    // по скольким параметрам будет осуществляться поиск туров.
    // Дополнительно получим выгоду при реализации
    // для разных провайдеров, т.к. на основе данного шаблона будем
    // генерировать спецификации для запросов туров
    public class SearchPattern
    {
        //Город вылета
        public string DepartureCity { get; set; }

        //Город тура
        public string TourCity { get; set; }

        //Дата начала тура
        //не понятно, что есть начало: вылет или заселение, предположим что вылет
        public DateTime? DepartureDate { get; set; }

        //Количество ночей от
        public int? HotelNightCountFrom { get; set; }

        //Количество ночей до
        public int? HotelNightCountTo { get; set; }

        //Количество человек
        public int? PersonCount { get; set; }

        //Порядок сортировки результата
        public int ToursOrder { get; set; }
    }
}

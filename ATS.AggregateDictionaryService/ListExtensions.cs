using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.AggregateDictionaryService
{
    public static class ListAggregateDictionaryServiceExtensions
    {
        /// <summary>
        /// Метод позволяющий получить уникальные элементы из списка по ключевому полю
        /// </summary>
        /// <typeparam name="T">тип элемента</typeparam>
        /// <typeparam name="TKey">тип ключевого поля</typeparam>
        /// <param name="list">список элементов</param>
        /// <param name="keySelector">функция возвращающая ключевое поле</param>
        /// <returns></returns>
        public static List<T> GetUniqueElements<T, TKey>(this List<T> list, Func<T, TKey> keySelector)
        {
            return list.GroupBy(keySelector).Select(g => g.First()).ToList();
        }
    }
}

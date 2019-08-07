using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Common
{
    /// <summary>
    /// Класс для десериализации настроек из appsettings.json
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Адрес, по которому крутится сервис
        /// </summary>
        public string ApiUri { get; set; }

        /// <summary>
        /// Текущая версия api
        /// </summary>
        public string ApiVersion { get; set; }

        /// <summary>
        /// Язык
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Таймаут ожидания ответа от провайдеров туров, миллисекунды
        /// </summary>
        public int RequestTimeout { get; set; }

        /// <summary>
        /// Любимая жена
        /// </summary>
        public string PreferredProvider { get; set; }

        /// <summary>
        /// Разница в цене между предпочтительным провайдером и всеми отстальными, в процентах
        /// </summary>
        public decimal DifferenceBetweenProviderPrice { get; set; }
    }
}

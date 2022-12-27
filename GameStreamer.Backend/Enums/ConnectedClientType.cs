namespace GameStreamer.Backend.Enums
{

    /// <summary>
    /// Перечисление типов подключаемых к игре клиентов
    /// </summary>
    public enum ConnectedClientType
    {
        /// <summary>
        /// Неизвестный клиент
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Веб-сайт на Angular
        /// </summary>
        AngularWeb = 1,

        /// <summary>
        /// Десктопное приложение на WPF
        /// </summary>
        WpfDesktop = 2,

        /// <summary>
        /// Мобильное приложение на Xamarin
        /// </summary>
        XamarinMobile = 3
    }
}

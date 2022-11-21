namespace GameStreamer.Backend.Storage.GameStreamerDbase.Entities
{

    /// <summary>
    /// Сущность подключенного игрока в БД 
    /// </summary>
    public class ConnectedPlayerEntity
    {
        /// <summary>
        /// Id записи в БД
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id клиента, выдаваемое SignalR хабом
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// Id клиента, для хранения Id без привязки к SignalR
        /// </summary>
        public string ConnectionGuid { get; set; }

        /// <summary>
        /// Тип подключенного клиента
        /// </summary>
        public TypeOfConnectedClient ClientType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid RoomGuid { get; set; }

        /// <summary>
        /// Флаг активности клиента
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Ник игрока
        /// </summary>
        public string NickName { get; set; }

    }

    /// <summary>
    /// Перечисление типов подключаемых к игре клиентов
    /// </summary>
    public enum TypeOfConnectedClient : int
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
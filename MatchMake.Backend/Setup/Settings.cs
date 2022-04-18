namespace MatchMake.Backend.Setup
{
    public class Settings
    {

        public ConnectionStrings ConnectionStrings { get; set; }
        public MessageBrokerConfig MessageBrokerConfig { get; set; }

    }
    
    public class ConnectionStrings
    {
        public string JobManagement { get; set; }
        public string MatchMakeContext { get; set; }
    }

    public class MessageBrokerConfig
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Virtualhost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

}

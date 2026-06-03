using Exiled.API.Interfaces;


namespace AntiSpamReportSystem
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = true;
        public int SymbolToWarnPlayer { get; set; } = 120;
        public int SymbolTobanPlayer { get; set; } = 200;
        public string Discord { get; set; } = "https://discord.gg/hSynuedaga";


    }
}

using Exiled.API.Interfaces;


namespace AntiSpamReportSystem
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = true;
        public int SymbolInReportToWarnPlayer { get; set; } = 120;
        public int SymbolInReportToBanPlayer { get; set; } = 200;
        public int SymbolInConsoleToWarnPlayer { get; set; } = 350;
        public int SymbolInConsoleToBanPlayer { get; set; } = 500;
        public string Discord { get; set; } = "https://discord.gg/hSynuedaga";
        public string ShowWarn { get; set; } = "Репорт/Команда не должен превышать лимит символов!";
        public string BanReason { get; set; } = "Автомодерация: Спам в репорт/консоль. Обратитесь в наш дискорд сервер, если вас забанили по ошибке. Количество: ";

    }
}

using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;

namespace AntiSpamReportSystem
{
    public class Plugin : Plugin<Config>
    {
        public override string Author => "kardanchik";
        public override string Name => "AntiReportSpamSystem";
        public override string Prefix => "AntiReportSpamSystem";

        public static Plugin Instance { get; private set; }
        private Config _config => Plugin.Instance.Config;

        public override void OnEnabled()
            
        {
            Instance = this;
            Exiled.Events.Handlers.Server.LocalReporting += OnLocalReporting;
            Exiled.Events.Handlers.Player.SendingValidCommand += OnSendingValidCommand;
            base.OnEnabled();
        }
        public void OnDisabled()
        {
            Instance = null;
            Exiled.Events.Handlers.Server.LocalReporting -= OnLocalReporting;
            Exiled.Events.Handlers.Player.SendingValidCommand -= OnSendingValidCommand;
            base.OnDisabled();
        }

        private void OnLocalReporting(LocalReportingEventArgs ev)
        {
            if (ev.Reason.Length >= _config.SymbolInReportToWarnPlayer)
            {
                ev.IsAllowed = false;
                ev.Player.ShowHint(_config.ShowWarn + $"({_config.SymbolInReportToWarnPlayer})", 3);
                if (ev.Reason.Length >= _config.SymbolInReportToBanPlayer)
                {
                    ev.Player.Ban(999999999, _config.BanReason + $"{ev.Reason.Length}. Discord: {_config.Discord}");
                    Log.Warn($"Игрок {ev.Player} отправил репорт! Число символов в репорте {ev.Reason.Length}");
                }
                return;
            }
        }

        private void OnSendingValidCommand(SendingValidCommandEventArgs ev)
        {
            if (ev.Type == LabApi.Features.Enums.CommandType.Client)
            {
                if (ev.Query.Length >= _config.SymbolInConsoleToWarnPlayer)
                {
                    ev.IsAllowed = false;
                    ev.Player.ShowHint(_config.ShowWarn + $"({_config.SymbolInConsoleToWarnPlayer})", 3);
                    ev.Response = _config.ShowWarn + $"({_config.SymbolInConsoleToWarnPlayer})";
                    if (ev.Query.Length >= _config.SymbolInConsoleToBanPlayer)
                    {
                        ev.Player.Ban(999999999, _config.BanReason + $"{ev.Query.Length}. Discord: {_config.Discord}");
                        Log.Warn($"Игрок {ev.Player} отправил Репорт/Команду! Число символов в Репорте/Команде {ev.Query.Length}");
                    }
                    return;
                }
            }
        }
    }
}
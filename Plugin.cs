using Exiled.API.Features;
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
            Exiled.Events.Handlers.Server.LocalReporting += reported;
            base.OnEnabled();
        }
        public void OnDisabled()
        {
            Instance = null;
            Exiled.Events.Handlers.Server.LocalReporting -= reported;
            base.OnDisabled();
        }

        private void reported(LocalReportingEventArgs ev)
        {
            if (ev.Reason.Length >= _config.SymbolToWarnPlayer)
            {
                ev.IsAllowed = false;
                ev.Player.ShowHint($"The report must not exceed the character <b><color=red>limit!</color></b>({_config.SymbolToWarnPlayer})", 3);
                if (ev.Reason.Length >= _config.SymbolTobanPlayer)
                {
                    ev.Player.Ban(999999999, $"Auto-moderation: Spam in the report. Contact our Discord server if you were banned by mistake. Number: {ev.Reason.Length}. Discord: {_config.Discord} ");
                    Log.Warn($"Player {ev.Player} has sent a report! The number of characters in the report: {ev.Reason.Length}");
                }
                return;
            }
        }
    }
}
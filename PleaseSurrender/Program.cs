using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace PleaseSurrender
{
    internal class Program
    {
        private static Menu config;
        private static void Main()
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }
        private static void Game_OnGameLoad(EventArgs args)
        {
            config = new Menu("Auto Surrender", "menu", true);

            config.AddItem(new MenuItem("toggle", "Auto Surrender On/Off").SetValue(true));
            config.AddToMainMenu();

            Game.PrintChat("<font color='#01f841'>Auto Surrender by xaxixeo</font>");
            Game.OnNotify += Game_OnNotify;
        }

        private static void Game_OnNotify(GameNotifyEventArgs args)
        {
            if (string.Equals(args.EventId.ToString(), "OnSurrenderVote") && config.Item("toggle").GetValue<bool>() || args.EventId == GameEventId.OnSurrenderVote) 
            {
                Game.Say("/ff");
            }
        }
    }
}
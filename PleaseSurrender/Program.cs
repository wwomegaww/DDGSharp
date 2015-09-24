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
        private static float lastSurrenderTime;
        private static bool Surrender(float gameTime)
        {
            return (gameTime + 30) >= lastSurrenderTime;
        }
        private static void Main()
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }
        private static void Game_OnGameLoad(EventArgs args)
        {
            config = new Menu("Please Surrender", "menu", true);

            config.AddItem(new MenuItem("toggle", "Auto Surrender On/Off").SetValue(true));
            config.AddItem(new MenuItem("time", "Set Time for Surrender").SetValue(new Slider(1200, 900, 1800)));
            config.AddToMainMenu();

            Game.PrintChat("<font color='#01f841'>PleaseSurrender by xaxixeo</font>");
            Game.OnUpdate += Game_OnUpdate;
            Game.OnNotify += Game_OnNotify;
        }

        private static void Game_OnNotify(GameNotifyEventArgs args)
        {
            if (string.Equals(args.EventId.ToString(), "OnSurrenderVote") || args.EventId == GameEventId.OnSurrenderVote)
            {
                Game.Say("/ff");
            }
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            var time = config.Item("time").GetValue<Slider>().Value;

            if (Game.Time >= time * 60 && config.Item("toggle").GetValue<bool>() && Surrender(Game.Time))
            {
                Game.Say("/ff");
                lastSurrenderTime = Game.ClockTime;
            }
        }
    }
}
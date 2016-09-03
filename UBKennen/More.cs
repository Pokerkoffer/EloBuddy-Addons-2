﻿using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Events;
using SharpDX;


namespace UBKennen
{
    class More
    {       
        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Kennen") return;

            Config.Dattenosa();
            Spells.InitSpells();
            InitEvents();
        }

        private static void InitEvents()
        {
            Gapcloser.OnGapcloser += Mode.Gapcloser_OnGapcloser;
            Orbwalker.OnUnkillableMinion += Mode.Orbwalker_OnUnkillableMinion;
            Game.OnTick += GameOnTick;
            if (Config.DrawMenu["draw"].Cast<CheckBox>().CurrentValue)
            {
                Drawing.OnDraw += OnDraw;
                Drawing.OnEndScene += Damages.Damage_Indicator;
            }
            Game.OnUpdate += OnUpdate;
        }
        private static void OnUpdate(EventArgs args)
        {
            Mode.Killsteal();
        }
        private static void GameOnTick(EventArgs args)
        {
            if (Player.Instance.IsDead) return;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            { Mode.Combo(); }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            { Mode.Harass(); }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            { Mode.LaneClear(); }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            { Mode.JungleClear(); }

        }
        private static void OnDraw(EventArgs args)
        {
            if (Config.DrawMenu["drawQ"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.Q.IsLearned ? Color.HotPink : Color.Zero , Spells.Q.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu["drawW"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.W.IsLearned ? Color.Cyan : Color.Zero, Spells.W.Range, Player.Instance.Position);
            }
            if (Config.DrawMenu["drawR"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Spells.R.IsLearned ? Color.Yellow : Color.Zero, Spells.R.Range, Player.Instance.Position);
            }
        }
    }
}

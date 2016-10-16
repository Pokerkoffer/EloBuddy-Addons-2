﻿using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System;
using System.Linq;

namespace UBAzir
{
    public class ObjManager
    {
        public static Vector3 LastMyPos;
        public static bool All_Basic_Is_Ready = false;
        public static float Time;

        public static int CountAzirSoldier
        {
            get
            {
                return Orbwalker.AzirSoldiers.Count(s => s.IsAlly);
            }
        }
        public static Vector3 Soldier_Can_Cast_E { get; set; }
        public static Vector3 Soldier_Nearest_Enemy
        {
            get
            {
                var target = TargetSelector.GetTarget(1300, DamageType.Magical);
                var Soldier = Orbwalker.ValidAzirSoldiers.OrderBy(s => s.Distance(target)).FirstOrDefault();
                if (Soldier != null) return Soldier.Position;
                else return Vector3.Zero;
            }
        }
        public static Vector3 Soldier_Nearest_Azir
        {
            get
            {
                var Soldier = Orbwalker.AzirSoldiers.OrderBy(s => s.Distance(Player.Instance.Position)).FirstOrDefault();
                if (Soldier != null && Player.Instance.Distance(Soldier.Position) <= 1050) return Soldier.Position;
                else return Vector3.Zero;
            }
        }
        public static Vector3 SoldierPos
        {
            get
            {
                if (CountAzirSoldier < 1)
                    return Vector3.Zero;
                else return Orbwalker.AzirSoldiers.LastOrDefault().Position;
            }
        }
        public static void ManyThingInHere(EventArgs args)
        {
            if (Game.Time > Time + 1f)
            {
                LastMyPos = Player.Instance.Position;
                if (Spells.Q.IsReady() && Spells.W.IsReady() && Spells.E.IsReady())
                    All_Basic_Is_Ready = true;
                else All_Basic_Is_Ready = false;
                Time = Game.Time;
            }
            if (Game.Time > Insec.LastSetTime + 14f)
            {
                Insec.PositionSelected = new Vector3();
                Insec.AllySelected = null;
                Insec.PositionGotoSelected = new Vector3();
            }
            if (Config.Insec.Checked("normalInsec", false) || Config.Insec.Checked("godInsec", false))
            {
                var Value = Config.Insec.Checked("normalInsec", false) ? Config.Insec.GetValue("normal.1", false) : Config.Insec.GetValue("god.1", false);
                var Target = TargetSelector.GetTarget(Spells.R.Range, DamageType.Mixed);
                if (Target != null)
                {
                    switch (Value)
                    {
                        case 0:
                            {
                                SpecialVector.WhereCastR(Target, SpecialVector.I_want.Cursor);
                            }
                            break;
                        case 1:
                            {
                                SpecialVector.WhereCastR(Target, SpecialVector.I_want.Turret);
                            }
                            break;
                        case 2:
                            {
                                SpecialVector.WhereCastR(Target, SpecialVector.I_want.Ally);
                            }
                            break;
                        case 3:
                            {
                                if (Insec.PositionSelected != new Vector3())
                                {
                                    SpecialVector.WhereCastR(Target, Insec.PositionSelected);
                                }
                                if (Insec.AllySelected != null)
                                {
                                    SpecialVector.WhereCastR(Target, Insec.AllySelected.Position);
                                }
                            }
                            break;
                    }
                }
            }
        }
    }
}

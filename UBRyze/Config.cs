﻿using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Notifications;
using System.Drawing;

namespace UBRyze
{
    class Config
    {
        public static Menu Menu;
        public static Menu ComboMenu;
        public static Menu HarassMenu;
        public static Menu LaneClear;
        public static Menu JungleClear;
        public static Menu LasthitMenu;
        public static Menu AutoMenu;
        public static Menu MiscMenu;
        public static Menu DrawMenu;
        public static ComboBox AutoBox;

        public static void Dattenosa()
        {
            // Menu
            Menu = MainMenu.AddMenu("UB Ryze", "UBRyze");
            Menu.AddGroupLabel("Made by Uzeumaki Boruto");
            Menu.AddLabel("Dattenosa");
            Menu.Add("human", new CheckBox("Humanizer?", false));
            var sty = Menu.Add("style", new ComboBox("Delay", 1, "Exact Value", "Random Value"));
            var delay1 = Menu.Add("delay1", new Slider(Menu.GetValue("style", false) == 1 ? "Min Delay {0}0" : "Delay {0}0", 0, 0, 100));
            var delay2 = Menu.Add("delay2", new Slider("Max delay {0}0", 1, 0, 100));
            delay2.IsVisible = Menu.GetValue("style", false) == 1;
            sty.OnValueChange += delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
            {
                if (args.NewValue == 0)
                {
                    delay1.DisplayName = "Delay {0}0";
                    delay2.IsVisible = false;
                }
                if (args.NewValue == 1)
                {
                    delay1.DisplayName = "Min Delay {0}0";
                    delay2.IsVisible = true;
                }
            };

 
            ComboMenu = Menu.AddSubMenu("Combo");
            {
                ComboMenu.Add("Q", new CheckBox("Use Q"));
                ComboMenu.Add("W", new CheckBox("Use W"));
                ComboMenu.Add("E", new CheckBox("Use E"));
                ComboMenu.AddSeparator();
                ComboMenu.Add("combostyle", new ComboBox("Combo Style", 2, "Full Damage", "Shield & Movement speed", "Smart"));
                ComboMenu.Add("hpcbsmart", new Slider("If my HP below {0}% use Q to get Ms & shield (Smart)", 30));
                ComboMenu.AddSeparator();
                ComboMenu.Add("useflee", new CheckBox("Allow use Flee Combo"));
                ComboMenu.AddLabel("Flee combo is always use Q to get Shield & MS");
                ComboMenu.AddLabel("Active this by press your flee key");

            }

            HarassMenu = Menu.AddSubMenu("Harass");
            {
                HarassMenu.Add("Q", new CheckBox("Use Q"));
                HarassMenu.Add("W", new CheckBox("Use W"));
                HarassMenu.Add("E", new CheckBox("Use E"));
                //HarassMenu.Add("useQEhr", new CheckBox("Use E on minion that colision in Q Width"));
                HarassMenu.Add("hr", new Slider("If my MP below {0}% stop use spell to harass", 50));
                var HarassKey = HarassMenu.Add("keyharass", new KeyBind("Auto Harass", false, KeyBind.BindTypes.PressToggle, 'Z'));
                HarassKey.OnValueChange += delegate
                {
                    var On = new SimpleNotification("Auto Harass status:", "Activated. ");
                    var Off = new SimpleNotification("Auto Harass status:", "Disable. ");
                    if (DrawMenu.Checked("draw") && DrawMenu.Checked("notif"))
                    {
                        Notifications.Show(HarassKey.CurrentValue ? On : Off, 2000);
                    }
                };
                HarassMenu.Add("autohrmng", new Slider("Stop auto harass if my MP  below {0}%", 80));
            }

            LaneClear = Menu.AddSubMenu("LaneClear");
            {
                LaneClear.Add("Q", new CheckBox("Use Q", false));
                LaneClear.Add("W", new CheckBox("Use W", false));
                LaneClear.Add("E", new CheckBox("Use E", false));
                LaneClear.Add("lc", new Slider("If my MP below {0}% stop use spell to Lanclear", 50));
                LaneClear.Add("logiclc", new CheckBox("Enable Smart Lanclear[BETA]"));
                LaneClear.AddGroupLabel("Smart Laneclear Settings");
                LaneClear.Add("Qlc", new Slider("Use Q only {0} minion has buff of E", 5, 1, 15));
                LaneClear.Add("Elc", new CheckBox("Enable E Logic cast"));
            }

            JungleClear = Menu.AddSubMenu("JungClear");
            {
                JungleClear.Add("Q", new CheckBox("Use Q"));
                JungleClear.Add("W", new CheckBox("Use W"));
                JungleClear.Add("E", new CheckBox("Use E"));
                JungleClear.Add("jc", new Slider("If my MP below {0}% stop use spell to Jungclear", 50));
            }

            LasthitMenu = Menu.AddSubMenu("Lasthit");
            {
                LasthitMenu.AddLabel("This only work if you can kill minion wwith spell");
                LasthitMenu.Add("Q", new CheckBox("Use Q"));
                LasthitMenu.Add("W", new CheckBox("Use W"));
                LasthitMenu.Add("E", new CheckBox("Use E"));
                LasthitMenu.Add("lh", new Slider("If my MP below {0}% stop use spell to Lasthit", 50));
                LasthitMenu.AddGroupLabel("Use spell on Unkillable minnion");
                LasthitMenu.Add("Qlh", new CheckBox("Use Q"));
                LasthitMenu.Add("Wlh", new CheckBox("Use W"));
                LasthitMenu.Add("Elh", new CheckBox("Use E"));
                LasthitMenu.Add("unkillmanage", new Slider("Stop this if my MP below {0}%", 15));
            }

            AutoMenu = Menu.AddSubMenu("AutoMenu");
            {
                AutoBox = AutoMenu.Add("autofl", new ComboBox("Auto W when flash", 1, "None", "W", "E + W"));
                AutoBox.OnValueChange += AutoBox_OnValueChange;
                AutoMenu.Add("R", new KeyBind("R & Zhonya to nearest turret", false, KeyBind.BindTypes.HoldActive));
                //AutoMenu.AddSeparator();
                //AutoMenu.Add("Rzh", new Slider("Auto use Zhonya & R to your nearesr Nexus if around you >= {0}", 5, 1, 6));
                //AutoMenu.Add("Rzhe", new Slider("Get enemy around you {0}0 distance", 50, 0, 150));
                //AutoMenu.Add("Rzha", new Slider("Get ally around you {0}0 distance", 100, 0, 150));
                //AutoMenu.AddLabel("This mean if x enemy around you and no ally around, you will R into nearest Turret");
            }

            MiscMenu = Menu.AddSubMenu("Misc");
            {
                MiscMenu.AddGroupLabel("Killsteal");
                MiscMenu.Add("Qks", new CheckBox("Use Q to Killsteal"));
                MiscMenu.Add("Wks", new CheckBox("Use W to Killsteal"));
                MiscMenu.Add("Eks", new CheckBox("Use E to Killsteal"));
                MiscMenu.AddGroupLabel("Damage calculator");
                MiscMenu.Add("dmg", new ComboBox("How to damage cal?", 0, "Basic Combo(QWE)", "Highest Damage you can take"));
                MiscMenu.Add("gapcloser", new CheckBox("Use W on Gapcloser"));

            }

            DrawMenu = Menu.AddSubMenu("Drawing");
            {
                DrawMenu.Add("draw", new CheckBox("Enable Drawing"));
                DrawMenu.Add("notif", new CheckBox("Enable Notification"));
                DrawMenu.Add("drQ", new CheckBox("Draw Q"));
                DrawMenu.Add("drW", new CheckBox("Draw W + E"));
                DrawMenu.Add("drR", new CheckBox("Draw R"));
                DrawMenu.Add("drdamage", new CheckBox("Damage Indicator"));
                var ColorPick = DrawMenu.Add("color", new ColorPicker("Damage Indicator Color", SaveColor.Load()));
                ColorPick.OnLeftMouseUp += delegate(Control sender, System.EventArgs args)
                {
                    SaveColor.Save(ColorPick.CurrentValue);
                };
            }
        }
        static void AutoBox_OnValueChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
        {
            switch (args.NewValue)
            {
                case 0:
                    {
                        AutoBox.DisplayName = "You turn off flash auto";
                    }
                    break;
                case 1:
                    {
                        AutoBox.DisplayName = "Auto use W after flash";
                    }
                    break;
                case 2:
                    {
                        AutoBox.DisplayName = "Auto use E + W after flash";
                    }
                    break;
            }
        }

    }
}

﻿using System;
using System.Linq;
using Assets.Scripts.Unity.UI_New.Popups;
using Assets.Scripts.Utils;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Components;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.ModOptions;
using BTD_Mod_Helper.Extensions;
using MelonLoader;
using UltimateCrosspathing;
using UltimateCrosspathing.Merging;
using UnityEngine;

[assembly: MelonInfo(typeof(UltimateCrosspathingMod), "Ultimate Crosspathing", "1.3.0", "doombubbles")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace UltimateCrosspathing
{
    public class UltimateCrosspathingMod : BloonsTD6Mod
    {
        public static readonly ModSettingInt MaxTiers = new(7)
        {
            min = 0,
            max = 15,
            slider = true,
            description = "Controls the maximum number of upgrades that any one tower can have at once. " +
                          "Setting this all the way to 15 will enable the complete Ultimate Crosspathing experience!"
        };

        internal static readonly ModSettingBool AffectModdedTowers = new(false)
        {
            description = "Note: This setting doesn't cause algorithmic merging of modded towers. " +
                          "It just activates any existing Ultimate Crosspathing integration that mod authors added themselves."
        };

        private static readonly ModSettingCategory IndividualTowers = new("Individual Towers")
        {
            modifyCategory = category =>
            {
                var enableAll = category.AddButton(
                    new Info("Enable All", 1000, -100, 562, 200, anchor: new Vector2(0.5f, 1)),
                    VanillaSprites.GreenBtnLong, new Action(() =>
                    {
                        foreach (var loadInfo in ModContent.GetContent<LoadInfo>())
                        {
                            loadInfo.Enabled.SetValue(true);
                        }
                    }));
                enableAll.LayoutElement.ignoreLayout = true;
                enableAll.AddText(new Info("Text", InfoPreset.FillParent), "Enable All", 80f);

                var disableAll = category.AddButton(
                    new Info("Disable All", -1000, -100, 562, 200, anchor: new Vector2(0.5f, 1)),
                    VanillaSprites.RedBtnLong, new Action(() =>
                    {
                        foreach (var loadInfo in ModContent.GetContent<LoadInfo>())
                        {
                            loadInfo.Enabled.SetValue(false);
                        }
                    }));
                disableAll.LayoutElement.ignoreLayout = true;
                disableAll.AddText(new Info("Text", InfoPreset.FillParent), "Disable All", 80f);
            }
        };

        public static readonly ModSettingBool DartMonkeyEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.DartMonkeyIcon
        };

        public static readonly ModSettingBool BoomerangMonkeyEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.BoomerangMonkeyIcon
        };

        public static readonly ModSettingBool BombShooterEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.BombShooterIcon
        };

        public static readonly ModSettingBool TackShooterEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.TackShooterIcon
        };

        public static readonly ModSettingBool IceMonkeyEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.IceMonkeyIcon
        };

        public static readonly ModSettingBool GlueGunnerEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.GlueGunnerIcon
        };

        public static readonly ModSettingBool SniperMonkeyEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.SniperMonkeyIcon
        };

        public static readonly ModSettingBool MonkeySubEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.MonkeySubIcon
        };

        public static readonly ModSettingBool MonkeyBuccaneerEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.MonkeyBuccaneerIcon
        };

        public static readonly ModSettingBool MonkeyAceEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.MonkeyAceIcon
        };

        public static readonly ModSettingBool HeliPilotEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.HeliPilotIcon
        };

        public static readonly ModSettingBool MortarMonkeyEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.MortarMonkeyIcon
        };

        public static readonly ModSettingBool DartlingGunnerEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.DartlingGunnerIcon
        };

        public static readonly ModSettingBool WizardMonkeyEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.WizardIcon
        };

        public static readonly ModSettingBool SuperMonkeyEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.SuperMonkeyIcon
        };

        public static readonly ModSettingBool NinjaMonkeyEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.NInjaMonkeyIcon
        };

        public static readonly ModSettingBool AlchemistEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.AlchemistIcon
        };

        public static readonly ModSettingBool DruidEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.DruidIcon
        };

        public static readonly ModSettingBool BananaFarmEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.BananaFarmIcon2
        };

        public static readonly ModSettingBool SpikeFactoryEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.SpikeFactoryIcon
        };

        public static readonly ModSettingBool MonkeyVillageEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.MonkeyVillageIcon
        };

        public static readonly ModSettingBool EngineerMonkeyEnabled = new(true)
        {
            button = true,
            category = IndividualTowers,
            icon = VanillaSprites.EngineerMonkeyicon
        };

#if DEBUG
        private static readonly ModSettingCategory DebugSettings = "Debug Settings";

        public static readonly ModSettingBool PostMergeRegenerate = new(false)
        {
            displayName = "DEBUG: PostMerge While Regenerating",
            category = DebugSettings
        };
        
        private static readonly ModSettingButton ExportTowerBytes = new(LoadInfo.ExportTowers)
        {
            displayName = "DEBUG: Export Tower Bytes",
            buttonText = "Export",
            category = DebugSettings
        };
#endif

        public const int TowerBatchSize = 8;

        /*public override void OnTitleScreen()
        {
            if (RegenerateTowers)
            {
                StarterMessage();

                AsyncMerging.GetNextTowerBatch();
                AsyncMerging.AsyncCreateCrosspaths();
            }
        }

        public override void OnUpdate()
        {
            if (AsyncMerging.pass == AsyncMerging.middlePass && AsyncMerging.pass > AsyncMerging.finishedPass)
            {
                ModHelper.Msg<UltimateCrosspathingMod>("Something is happening here");
                AsyncMerging.FinishCreatingCrosspaths();
                AsyncMerging.finishedPass++;
            }
        }

        public override void OnMainMenu()
        {
            if (AsyncMerging.pass > AsyncMerging.finishedPass && PopupScreen.instance != null)
            {
                PopupScreen.instance.ShowOkPopup(
                    "Don't go into a game just yet, Ultimate Crosspathing is still setting up in the background.");
            }
        }

        public static void ShowFinishedPopup()
        {
            if (PopupScreen.instance != null)
            {
                PopupScreen.instance.HideAllPopups();
                TaskScheduler.ScheduleTask(() =>
                    PopupScreen.instance.ShowOkPopup(
                        "Ultimate Crosspathing has now finished merging.\n" +
                        "Happy Crosspathing!"));
            }
        }
        */


        public static void StarterMessage()
        {
            var enabled = ModContent.GetContent<LoadInfo>().Where(info => info.Enabled).Select(info => info.Name)
                .ToArray();
            var disabled = ModContent.GetContent<LoadInfo>().Where(info => !info.Enabled).Select(info => info.Name)
                .ToArray();

            if (enabled.Any())
            {
                ModHelper.Msg<UltimateCrosspathingMod>("Enabled Towers: ");
                ModHelper.Msg<UltimateCrosspathingMod>(string.Join(", ", enabled));

                ModHelper.Msg<UltimateCrosspathingMod>("");

                ModHelper.Msg<UltimateCrosspathingMod>("Disabled Towers: ");
                ModHelper.Msg<UltimateCrosspathingMod>(string.Join(", ", disabled));

                ModHelper.Msg<UltimateCrosspathingMod>("");

                ModHelper.Msg<UltimateCrosspathingMod>("Beginning Crosspath Creation");
            }
        }
    }
}
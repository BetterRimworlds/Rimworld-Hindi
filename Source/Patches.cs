// ==== ./Source/Patches.cs ====
using System;
using HarmonyLib;
using UnityEngine;
using Verse;
namespace BetterRimworlds
{
    [HarmonyPatch(typeof(Text), nameof(Text.StartOfOnGUI))]
    public static class Patch_Text_StartOfOnGUI
    {
        private static string _initializedForLanguage;
        private static bool _fontApplied;
        public static void Postfix()
        {
            string activeLang = LanguageDatabase.activeLanguage?.folderName;
            if (activeLang != RimworldHindiMod.Language)
            {
                _fontApplied = false;
                return;
            }
            if (_initializedForLanguage != activeLang)
            {
                _initializedForLanguage = activeLang;
                _fontApplied = false;
                RimworldHindiMod.Bootstrap.Init(activeLang);
            }
            if (!RimworldHindiMod.Bootstrap.ShouldUseCustomFont())
                return;
            if (_fontApplied && Text.fontStyles[(int)GameFont.Small].font == RimworldHindiMod.Bootstrap.LoadedFont)
                return;
            var font = RimworldHindiMod.Bootstrap.LoadedFont;
            foreach (GameFont value in Enum.GetValues(typeof(GameFont)))
            {
                int i = (int)value;
                int fontSize = GetFallbackFontSize(value);
                ApplyFont(Text.fontStyles[i], font, fontSize);
                ApplyFont(Text.textFieldStyles[i], font, fontSize);
                ApplyFont(Text.textAreaStyles[i], font, fontSize);
                ApplyFont(Text.textAreaReadOnlyStyles[i], font, fontSize);
                Log.Message(
                    $"[BetterRimworlds:{activeLang}] " +
                    $"Applied font to GameFont.{value} (size {Text.fontStyles[i].fontSize})"
                );
            }
            _fontApplied = true;
        }
        private static void ApplyFont(GUIStyle style, Font font, int fallbackFontSize)
        {
            if (style == null)
                return;
            style.font = font;
            style.fontStyle = FontStyle.Normal;
            if (style.fontSize <= 0)
                style.fontSize = fallbackFontSize;
        }
        private static int GetFallbackFontSize(GameFont font)
        {
            switch (font)
            {
                case GameFont.Tiny:
                    return 12;
                case GameFont.Small:
                    return 14;
                case GameFont.Medium:
                    return 16;
                default:
                    return 14;
            }
        }
    }
}
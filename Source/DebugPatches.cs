// ==== ./Source/DebugPatches.cs ====
using HarmonyLib;
using RimWorld;
using Verse;
namespace BetterRimworlds
{
    [HarmonyPatch(typeof(MainMenuDrawer), "DoMainMenuControls")]
    public static class Patch_MainMenuDrawer_Debug
    {
        private static bool _logged;
        public static void Prefix()
        {
            if (_logged)
                return;
            if (LanguageDatabase.activeLanguage?.folderName != RimworldHindiMod.Language)
                return;
            _logged = true;
            if (RimworldHindiMod.Bootstrap.LoadedFont == null)
            {
                Log.Warning(
                    $"[BetterRimworlds:{RimworldHindiMod.Language}] " +
                    "DebugPatch: LoadedFont is null."
                );
                return;
            }
            Log.Message(
                $"[BetterRimworlds:{RimworldHindiMod.Language}] DebugPatch: " +
                $"ActiveLanguage={LanguageDatabase.activeLanguage?.folderName}, " +
                $"Font={RimworldHindiMod.Bootstrap.LoadedFont.name}, " +
                $"dynamic={RimworldHindiMod.Bootstrap.LoadedFont.dynamic}"
            );
        }
    }
}
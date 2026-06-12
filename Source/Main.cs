// ==== ./Source/Main.cs ====
using HarmonyLib;
using Verse;
namespace BetterRimworlds;

public class RimworldHindiMod : Mod
{
    public const string Language = "Hindi";
    public static FontBootstrap Bootstrap { get; private set; }
    public RimworldHindiMod(ModContentPack content) : base(content)
    {
        Bootstrap = new FontBootstrap();
        var harmony = new Harmony(
            $"HopeSeekr.BetterRimworlds.Rimworld{Language}"
        );
        harmony.PatchAll();
        Log.Message($"[BetterRimworlds:{Language}] Harmony patches applied.");
    }
}

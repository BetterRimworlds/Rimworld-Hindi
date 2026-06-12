// ==== ./Source/FontBootstrap.cs ====
using UnityEngine;
using Verse;

namespace BetterRimworlds;

public class FontBootstrap
{
    public Font LoadedFont;
    private FontLanguageConfig _activeConfig;
    private static bool _missingFontDialogShown;
    private static AssetBundle _fontBundle;

    private static readonly Dictionary<string, FontLanguageConfig> LanguageFonts =
        new Dictionary<string, FontLanguageConfig>
        {
            // ── PUA-PIPELINE LANGUAGES ──────────────────────────────────
            // XML contains pre-shaped PUA codepoints (U+E000+gid).
            // ONLY the mod-shipped *.pua.ttf can render these — the OS
            // Noto fonts have no PUA cmap and Unity silently drops
            // unmapped characters (text disappears). These MUST load
            // from the mod's AssetBundle, never from the OS.
            ["Hindi"] = new FontLanguageConfig(
                language: "Hindi",
                bundleAssetName: "BetterRimworldsDevanagari.pua",
                fontSearchNames: null,
                packageName: null,
                fontUrl: null,
                // PUA warm-up span: every glyph in the font (and room
                // for future composite bakes). Unmapped codepoints in
                // the span are ignored by RequestCharactersInTexture.
                puaWarmupCount: 1536
            ),
            ["Bengali"] = new FontLanguageConfig(
                language: "Bengali",
                bundleAssetName: "BetterRimworldsBengali.pua",
                fontSearchNames: null,
                packageName: null,
                fontUrl: null,
                puaWarmupCount: 1280
            ),

            // ── OS-FONT LANGUAGES ───────────────────────────────────────
            // Arabic XML uses standard Unicode Presentation Forms B,
            // which every OS Noto Sans Arabic maps — OS loading remains
            // correct here.
            ["Arabic"] = new FontLanguageConfig(
                language: "Arabic",
                bundleAssetName: null,
                fontSearchNames: new[]
                {
                    "Noto Sans Arabic",
                    "Noto Sans Arabic Regular",
                    "NotoSansArabic-Regular",
                },
                packageName: "noto-fonts-extra",
                fontUrl: "https://fonts.google.com/noto/specimen/Noto+Sans+Arabic",
                testChars: "ابتثجحخدذرزسشصضطظعغفقكلمنهويءآأإؤئ"
            ),
            ["Urdu"] = new FontLanguageConfig(
                language: "Arabic",
                bundleAssetName: null,
                fontSearchNames: new[]
                {
                    "Noto Sans Arabic",
                    "Noto Sans Arabic Regular",
                    "NotoSansArabic-Regular",
                },
                packageName: "noto-fonts-extra",
                fontUrl: "https://fonts.google.com/noto/specimen/Noto+Sans+Arabic",
                testChars: "ابتثجحخدذرزسشصضطظعغفقكلمنهويءآأإؤئ"
            ),
            // Tamil note: currently OS-loaded raw Unicode, which has the
            // same shaping breakage Hindi had. When the Tamil XML is
            // regenerated through the PUA pipeline, move it to a
            // bundleAssetName config like Hindi/Bengali.
            ["Tamil"] = new FontLanguageConfig(
                language: "Tamil",
                bundleAssetName: null,
                fontSearchNames: new[]
                {
                    "Noto Sans Tamil",
                    "Noto Sans Tamil Regular",
                    "NotoSansTamil-Regular",
                },
                packageName: "noto-fonts-extra",
                fontUrl: "https://fonts.google.com/noto/specimen/Noto+Sans+Tamil",
                testChars: "அஆஇஈஉஊஎஏஐஒஓஔகஙசஜஞடணதநனபமயரறலளழவஷஸஹாிீுூெேைொோௌ்"
            ),
        };

    public void Init(string language)
    {
        if (!LanguageFonts.TryGetValue(language, out _activeConfig))
        {
            Log.Error(
                $"[BetterRimworlds] No font config for language '{language}'. " +
                $"Supported: {string.Join(", ", LanguageFonts.Keys)}"
            );
            return;
        }

        try
        {
            LoadedFont = _activeConfig.UsesPuaPipeline
                ? LoadBundleFont(_activeConfig)
                : LoadOsFont(_activeConfig);

            if (LoadedFont == null)
                return; // error already logged by the loader

            if (!LoadedFont.dynamic)
            {
                Log.Error(
                    $"[BetterRimworlds:{_activeConfig.Language}] Font is not " +
                    "dynamic — was the TTF imported with Character mode " +
                    "'Dynamic' + 'Include Font Data' before bundling?"
                );
                LoadedFont = null;
                return;
            }

            Log.Message(
                $"[BetterRimworlds:{_activeConfig.Language}] Loaded: " +
                $"name={LoadedFont.name}, dynamic={LoadedFont.dynamic}, " +
                $"lineHeight={LoadedFont.lineHeight}"
            );

            WarmUpFont(LoadedFont, _activeConfig);
            DumpFontInfo(LoadedFont, _activeConfig);
        }
        catch (Exception ex)
        {
            Log.Error(
                $"[BetterRimworlds:{_activeConfig.Language}] Font init failed: {ex}"
            );
        }
    }

    // ────────────────────────────────────────────────────────────────────
    // PUA PATH: load the mod-shipped font from an AssetBundle.
    // RimWorld auto-loads bundles from <Mod>/<version>/AssetBundles/,
    // exposed via ModContentPack.assetBundles. We search those first,
    // then fall back to manually loading from a Fonts/ folder (which
    // RimWorld does NOT auto-load, avoiding double-load conflicts).
    // ────────────────────────────────────────────────────────────────────
    private static Font LoadBundleFont(FontLanguageConfig config)
    {
        // 1. Search bundles RimWorld already loaded for this mod
        ModContentPack ourMod = LoadedModManager.RunningModsListForReading
            .FirstOrDefault(m => m.assemblies.loadedAssemblies
                .Contains(typeof(FontBootstrap).Assembly));

        if (ourMod != null)
        {
            foreach (AssetBundle bundle in
                     ourMod.assetBundles.loadedAssetBundles)
            {
                Font f = bundle.LoadAsset<Font>(config.BundleAssetName);
                if (f != null)
                {
                    Log.Message(
                        $"[BetterRimworlds:{config.Language}] Font " +
                        $"'{config.BundleAssetName}' loaded from " +
                        $"auto-loaded bundle '{bundle.name}'."
                    );
                    return f;
                }
            }
        }

        // 2. Manual fallback: <Mod>/Fonts/brfonts.assetbundle
        if (_fontBundle == null && ourMod != null)
        {
            string path = Path.Combine(ourMod.RootDir, "Fonts", "brfonts");
            if (!File.Exists(path))
                path += ".assetbundle";
            if (File.Exists(path))
                _fontBundle = AssetBundle.LoadFromFile(path);
        }

        Font fallback = _fontBundle?.LoadAsset<Font>(config.BundleAssetName);
        if (fallback != null)
        {
            Log.Message(
                $"[BetterRimworlds:{config.Language}] Font " +
                $"'{config.BundleAssetName}' loaded from Fonts/ bundle."
            );
            return fallback;
        }

        ShowBrokenInstallDialog(config);
        Log.Error(
            $"[BetterRimworlds:{config.Language}] Font asset " +
            $"'{config.BundleAssetName}' not found in any AssetBundle. " +
            "The mod install is incomplete or the bundle was built " +
            "without the font. Re-download the mod."
        );
        return null;
    }

    // ────────────────────────────────────────────────────────────────────
    // OS PATH: unchanged behavior for Arabic/Urdu/Tamil.
    // ────────────────────────────────────────────────────────────────────
    private static Font LoadOsFont(FontLanguageConfig config)
    {
        string match = FindSystemFont(config);
        if (match == null)
        {
            ShowMissingFontDialog(config);
            Log.Error(
                $"[BetterRimworlds] ERROR: No suitable font found for " +
                $"{config.Language}.\n\nPlease install a " +
                $"{config.Language} font:\n{config.FontUrl}\n\n" +
                "Then restart RimWorld."
            );
            return null;
        }

        Log.Message(
            $"[BetterRimworlds:{config.Language}] Found system font: '{match}'"
        );
        return Font.CreateDynamicFontFromOSFont(match, 16);
    }

    public bool ShouldUseCustomFont()
    {
        return _activeConfig != null
            && LanguageDatabase.activeLanguage?.folderName == _activeConfig.Language
            && LoadedFont != null
            && LoadedFont.dynamic;
    }

    private static string FindSystemFont(FontLanguageConfig config)
    {
        string[] osFonts = Font.GetOSInstalledFontNames();
        foreach (var candidate in config.FontSearchNames)
        {
            if (osFonts.Contains(candidate))
                return candidate;
        }
        string primary = config.FontSearchNames[0];
        foreach (var osFont in osFonts)
        {
            if (osFont.IndexOf(primary, StringComparison.OrdinalIgnoreCase) >= 0)
                return osFont;
        }
        return null;
    }

    private static void ShowMissingFontDialog(FontLanguageConfig config)
    {
        if (_missingFontDialogShown) return;
        _missingFontDialogShown = true;
        string fontName = config.FontSearchNames[0];
        string message =
            $"The {config.Language} translation needs {fontName} " +
            "installed on your system before RimWorld starts.\n\n" +
            $"Install {fontName}, then restart RimWorld.\n\n" +
            $"Download it here:\n{config.FontUrl}";
        try { Find.WindowStack.Add(new Dialog_MessageBox(message)); }
        catch (Exception ex)
        {
            Log.Warning(
                $"[BetterRimworlds:{config.Language}] " +
                $"Failed to show missing font dialog: {ex}"
            );
        }
    }

    private static void ShowBrokenInstallDialog(FontLanguageConfig config)
    {
        if (_missingFontDialogShown) return;
        _missingFontDialogShown = true;
        string message =
            $"The {config.Language} translation's font files are missing " +
            "from the mod.\n\nThis usually means an incomplete download " +
            "or unzip. Please re-install the mod.\n\n(Unlike older " +
            "versions, no system font installation is needed — the font " +
            "ships inside the mod.)";
        try { Find.WindowStack.Add(new Dialog_MessageBox(message)); }
        catch (Exception ex)
        {
            Log.Warning(
                $"[BetterRimworlds:{config.Language}] " +
                $"Failed to show broken install dialog: {ex}"
            );
        }
    }

    private static void WarmUpFont(Font font, FontLanguageConfig config)
    {
        try
        {
            const string ascii =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" +
                "0123456789 !?.,:;()-_+/\\[]{}<>=*\"'|";

            string warmupChars;
            if (config.UsesPuaPipeline)
            {
                // Warm the entire PUA glyph span. Codepoints with no
                // cmap entry are ignored, so over-requesting is safe
                // and future composite bakes are covered.
                var sb = new System.Text.StringBuilder(
                    ascii, ascii.Length + config.PuaWarmupCount);
                for (int i = 0; i < config.PuaWarmupCount; i++)
                    sb.Append((char)(0xE000 + i));
                warmupChars = sb.ToString();
            }
            else
            {
                warmupChars = ascii + config.TestChars;
            }

            font.RequestCharactersInTexture(warmupChars, 18, FontStyle.Normal);
            Log.Message(
                $"[BetterRimworlds:{config.Language}] Warmed up font texture."
            );
        }
        catch (Exception ex)
        {
            Log.Warning(
                $"[BetterRimworlds:{config.Language}] WarmUpFont failed: {ex}"
            );
        }
    }

    private static void DumpFontInfo(Font font, FontLanguageConfig config)
    {
        try
        {
            var results = new List<string>();
            if (config.UsesPuaPipeline)
            {
                // PUA spot checks: first glyphs + a known composite slot
                foreach (int cp in new[] { 0xE003, 0xE006, 0xE039, 0xE45D })
                    results.Add($"U+{cp:X4}={font.HasCharacter((char)cp)}");
            }
            else
            {
                var testChars = config.TestChars;
                for (int i = 0; i < Math.Min(8, testChars.Length); i++)
                {
                    char c = testChars[i];
                    results.Add($"U+{(int)c:X4}={font.HasCharacter(c)}");
                }
            }
            Log.Message(
                $"[BetterRimworlds:{config.Language}] Glyph test: " +
                $"A={font.HasCharacter('A')}, 1={font.HasCharacter('1')}, " +
                string.Join(", ", results)
            );
        }
        catch (Exception ex)
        {
            Log.Warning(
                $"[BetterRimworlds:{config.Language}] DumpFontInfo failed: {ex}"
            );
        }
    }
}

public class FontLanguageConfig
{
    public string Language { get; }
    public string BundleAssetName { get; }   // PUA pipeline: asset name in bundle
    public string[] FontSearchNames { get; } // OS pipeline: family names
    public string PackageName { get; }
    public string FontUrl { get; }
    public string TestChars { get; }
    public int PuaWarmupCount { get; }

    public bool UsesPuaPipeline => BundleAssetName != null;

    public FontLanguageConfig(
        string language,
        string bundleAssetName,
        string[] fontSearchNames,
        string packageName,
        string fontUrl,
        string testChars = null,
        int puaWarmupCount = 0)
    {
        Language = language;
        BundleAssetName = bundleAssetName;
        FontSearchNames = fontSearchNames;
        PackageName = packageName;
        FontUrl = fontUrl;
        TestChars = testChars;
        PuaWarmupCount = puaWarmupCount;
    }
}


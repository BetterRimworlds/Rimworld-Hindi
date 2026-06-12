# RimWorld Hindi (Hindi Translation Mod)
**by [Better Rimworlds](https://github.com/BetterRimworlds)** • powered by Autonomo AI

[**हिंदी/ Hindi**](README.md)

Bring a **full Hindi localization** to RimWorld — built to be **playable, UI-safe, and consistent** across the game’s terminology.

> ✅ Designed for real gameplay: stable placeholders, consistent RimWorld vernacular, and UI-friendly strings.

---

## What you get

- **Hindi translation** for RimWorld UI + game text
- **Consistent RimWorld terminology** (custom glossary / vernacular)
- **Placeholder-safe strings** (no broken `{0}`, `[PAWN_nameDef]`, etc.)
- **UI-safe length constraints** (labels/titles kept readable where possible)

---

## Requirements

Before enabling **RimWorld Hindi**, make sure you have:

- **Harmony Lib** installed and enabled

Harmony Lib is required for this mod to work correctly.

---

## Installation

### Option A: Steam Workshop (recommended)
1. Subscribe to **Harmony Lib** on Steam Workshop
2. Subscribe to the mod on Steam Workshop
3. Launch RimWorld
4. Go to **Mods**
5. Enable **Harmony Lib**
6. Enable **RimWorld Hindi**
7. Restart RimWorld when prompted

> If you don’t see either mod in your list, restart Steam and RimWorld.

*(Workshop link: add once published.)*

---

### Option B: Manual install (GitHub download)
1. Download and install **Harmony Lib**
2. Download this repository as a ZIP:
   - Click **Code** → **Download ZIP**
3. Extract it
4. Copy the folder into your RimWorld Mods directory:

**Windows**
```text
C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\
````

**Linux**

```text
~/.steam/steam/steamapps/common/RimWorld/Mods/
```

**macOS**

```text
~/Library/Application Support/Steam/steamapps/common/RimWorld/RimWorldMac.app/Mods/
```

5. Make sure the folder structure looks like:

```text
RimWorld/Mods/RimWorld-Hindi/
About/
Languages/
...
```

6. Launch RimWorld → **Mods**
7. Enable **Harmony Lib**
8. Enable **RimWorld Hindi**
9. Restart RimWorld when prompted

---

## Enable Hindi in RimWorld

After the mod is enabled:

1. Go to **Options**
2. Find **Language**
3. Select **Hindi**
4. Restart RimWorld if asked

---

## Load order

Recommended:

* **Core**
* DLCs (if any)
* **Harmony Lib**
* Other mods
* **RimWorld Hindi**

Harmony Lib should be enabled before **RimWorld Hindi**.

If another mod includes its own translation files, it may override parts of the Hindi text depending on load order.

---

## Known behavior

* Some UI strings are deliberately kept short to avoid overflow.
* Some mod-added content may remain in English unless those mods ship Hindi translations or you add patches.
* If you use many mods, translation completeness depends on whether those mods provide keyed strings / translation keys.

---

## Troubleshooting

### “Hindi isn’t showing up in the language menu”

* Confirm **Harmony Lib** is installed and enabled
* Confirm **Harmony Lib** loads before **RimWorld Hindi**
* Confirm **RimWorld Hindi** is enabled
* Confirm the folder path is correct:

  * `Mods/RimWorld-Hindi/Languages/Hindi/`
* Restart RimWorld after enabling the mod or changing your mod list

### “Some text is still in English”

* That text likely comes from:

  * another mod with no Hindi translation available
  * newly added RimWorld content that hasn’t been updated yet
* Please open an issue with:

  * a screenshot
  * the exact English text
  * your mod list + load order, if possible

### “Text looks weird / missing characters”

* RimWorld font rendering is sensitive to:

  * font mods
  * UI scaling
* Try disabling font/UI mods to confirm compatibility.

---

## Bug reports & requests

Open a GitHub issue here:

* Include **screenshots**
* Include the **exact string**, English if possible
* Include your **RimWorld version** and **mod list**

---

## Translation Stats

```text
================ HINDI TRANSLATION ANALYSIS ================
Volume: 125,547 English words -> 188,137 Hindi words

--- LLM (ChatGPT 5.1 Equivalent) ---
Total API Calls           : 17,326
Total LLM Tokens In       : 4,325,506
Total LLM Tokens Out      : 316,306
LLM total cost            : $8.57
  ├─ Input cost           : $5.41
  └─ Output cost          : $3.16
Total runtime             : 10.27 hours

--- Human Translation Team (Dubai) ---
Project Lead Time         : 114.1 calendar days
Average Rate              : $35.00/hr

Role            | #  | Total Hrs  | Hrs/Person   | Cost
---------------------------------------------------------------------------
Translators     | 3  | 1,411.0    | 470.3        | 181,246.48 AED ($49,385.96)
Editors         | 1  | 348.1      | 348.1        | 44,707.47 AED ($12,181.87)
Proofreaders    | 1  | 122.3      | 122.3        | 15,708.03 AED ($4,280.12)
---------------------------------------------------------------------------
TOTAL BILLABLE HOURS: 1,881.4  | 241,661.98 AED ($65,847.95)

    [ VS SINGLE HUMAN ]
    Human Calendar Time   : 526.8 Days (376.3 work + 150.5 wknd)
    Autonomo Speedup      : 1230.5x FASTER

--- Human Translation Team (USA) ---
Project Lead Time         : 137.0 calendar days
Average Rate              : $75.00/hr

Role            | #  | Total Hrs  | Hrs/Person   | Cost
---------------------------------------------------------------------------
Translators     | 3  | 1,693.2    | 564.4        | $126,992.47
Editors         | 1  | 417.7      | 417.7        | $31,324.81
Proofreaders    | 1  | 146.7      | 146.7        | $11,006.01
---------------------------------------------------------------------------
TOTAL BILLABLE HOURS: 2,257.6  | $169,323.30

    [ VS SINGLE HUMAN ]
    Human Calendar Time   : 632.1 Days (451.5 work + 180.6 wknd)
    Autonomo Speedup      : 1476.6x FASTER
```

---

## Credits

Published by **[Better Rimworlds](https://github.com/BetterRimworlds)**
Built with the **Autonomo AI** localization pipeline: Automated QA Inspection & Copyediting.

---

## Disclaimer

RimWorld is the property of its respective owner(s).
This translation mod is an independent community project and is not affiliated with or endorsed by Ludeon Studios.

```
```

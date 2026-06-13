# RimWorld हिंदी (हिंदी अनुवाद मॉड)

[**ENGLISH**](README.en.md)

**द्वारा [Better Rimworlds](https://github.com/BetterRimworlds)** • Autonomo AI द्वारा संचालित

RimWorld में **पूर्ण हिंदी लोकलाइज़ेशन** जोड़ें — जिसे **खेलने योग्य, UI-सुरक्षित, और गेम की शब्दावली में सुसंगत** बनाए रखने के लिए तैयार किया गया है।

> ✅ वास्तविक गेमप्ले के लिए डिज़ाइन किया गया: स्थिर placeholders, सुसंगत RimWorld शब्दावली, और UI-अनुकूल स्ट्रिंग्स।

---

## आपको क्या मिलेगा

* RimWorld UI + गेम टेक्स्ट के लिए **हिंदी अनुवाद**
* **सुसंगत RimWorld शब्दावली** (कस्टम ग्लॉसरी / प्रचलित शब्दावली)
* **Placeholder-सुरक्षित स्ट्रिंग्स** (टूटे हुए `{0}`, `[PAWN_nameDef]`, आदि नहीं)
* **UI-सुरक्षित लंबाई सीमाएँ** (जहाँ संभव हो, labels/titles पढ़ने योग्य रखे गए हैं)

---

## आवश्यकताएँ

**RimWorld हिंदी** सक्षम करने से पहले, सुनिश्चित करें कि आपके पास:

* **Harmony Lib** इंस्टॉल और सक्षम हो

इस मॉड के सही ढंग से काम करने के लिए Harmony Lib आवश्यक है।

---

## इंस्टॉलेशन

### विकल्प A: Steam Workshop (अनुशंसित)

1. Steam Workshop पर **Harmony Lib** को Subscribe करें
2. Steam Workshop पर इस मॉड को Subscribe करें
3. RimWorld लॉन्च करें
4. **Mods** में जाएँ
5. **Harmony Lib** सक्षम करें
6. **RimWorld हिंदी** सक्षम करें
7. संकेत मिलने पर RimWorld पुनः प्रारंभ करें

> यदि आपको अपनी सूची में इनमें से कोई भी मॉड दिखाई नहीं देता, तो Steam और RimWorld को पुनः प्रारंभ करें।

[**Rimworld-Hindi on Steam Workshop**](https://steamcommunity.com/sharedfiles/filedetails/?id=3743477376)

---

### विकल्प B: मैनुअल इंस्टॉल (GitHub डाउनलोड)

1. **Harmony Lib** डाउनलोड और इंस्टॉल करें
2. इस repository को ZIP के रूप में डाउनलोड करें:

   * **Code** → **Download ZIP** पर क्लिक करें
3. इसे extract करें
4. फ़ोल्डर को अपनी RimWorld Mods directory में कॉपी करें:

**Windows**

```text
C:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\
```

**Linux**

```text
~/.steam/steam/steamapps/common/RimWorld/Mods/
```

**macOS**

```text
~/Library/Application Support/Steam/steamapps/common/RimWorld/RimWorldMac.app/Mods/
```

5. सुनिश्चित करें कि फ़ोल्डर संरचना ऐसी दिखे:

```text
RimWorld/Mods/RimWorld-Hindi/
About/
Languages/
...
```

6. RimWorld लॉन्च करें → **Mods**
7. **Harmony Lib** सक्षम करें
8. **RimWorld हिंदी** सक्षम करें
9. संकेत मिलने पर RimWorld पुनः प्रारंभ करें

---

## RimWorld में हिंदी सक्षम करें

मॉड सक्षम होने के बाद:

1. **Options** में जाएँ
2. **Language** खोजें
3. **Hindi** चुनें
4. यदि पूछा जाए, तो RimWorld पुनः प्रारंभ करें

---

## Load order

अनुशंसित क्रम:

* **Core**
* DLCs (यदि कोई हों)
* **Harmony Lib**
* अन्य मॉड्स
* **RimWorld हिंदी**

Harmony Lib को **RimWorld हिंदी** से पहले सक्षम होना चाहिए।

यदि किसी दूसरे मॉड में अपनी translation files शामिल हैं, तो load order के आधार पर वह हिंदी टेक्स्ट के कुछ हिस्सों को override कर सकता है।

---

## ज्ञात व्यवहार

* कुछ UI strings को जानबूझकर छोटा रखा गया है ताकि overflow से बचा जा सके।
* कुछ mod-added content अंग्रेज़ी में रह सकता है, जब तक वे मॉड हिंदी अनुवाद उपलब्ध न कराएँ या आप patches न जोड़ें।
* यदि आप कई मॉड्स का उपयोग करते हैं, तो translation completeness इस बात पर निर्भर करती है कि वे मॉड keyed strings / translation keys उपलब्ध कराते हैं या नहीं।

---

## समस्या निवारण

### “Hindi भाषा मेनू में दिखाई नहीं दे रही”

* पुष्टि करें कि **Harmony Lib** इंस्टॉल और सक्षम है
* पुष्टि करें कि **Harmony Lib**, **RimWorld हिंदी** से पहले load होता है
* पुष्टि करें कि **RimWorld हिंदी** सक्षम है
* पुष्टि करें कि folder path सही है:

  * `Mods/RimWorld-Hindi/Languages/Hindi/`
* मॉड सक्षम करने या mod list बदलने के बाद RimWorld पुनः प्रारंभ करें

### “कुछ टेक्स्ट अभी भी अंग्रेज़ी में है”

* वह टेक्स्ट संभवतः इनमें से किसी स्रोत से आ रहा है:

  * कोई दूसरा मॉड जिसमें हिंदी अनुवाद उपलब्ध नहीं है
  * नया जोड़ा गया RimWorld content जिसे अभी update नहीं किया गया है
* कृपया एक issue खोलें और उसमें शामिल करें:

  * screenshot
  * सटीक अंग्रेज़ी टेक्स्ट
  * आपका mod list + load order, यदि संभव हो

### “टेक्स्ट अजीब दिख रहा है / characters गायब हैं”

* RimWorld font rendering इन चीज़ों के प्रति संवेदनशील है:

  * font mods
  * UI scaling
* compatibility की पुष्टि करने के लिए font/UI mods को disable करके देखें।

---

## Bug reports और requests

यहाँ GitHub issue खोलें:

* **screenshots** शामिल करें
* **सटीक string** शामिल करें, संभव हो तो अंग्रेज़ी में
* अपना **RimWorld version** और **mod list** शामिल करें

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

**[Better Rimworlds](https://github.com/BetterRimworlds)** द्वारा प्रकाशित
**Autonomo AI** localization pipeline के साथ निर्मित: Automated QA Inspection & Copyediting.

---

## Disclaimer

RimWorld अपने संबंधित मालिकों की संपत्ति है।
यह translation mod एक स्वतंत्र community project है और Ludeon Studios से संबद्ध या अनुमोदित नहीं है।

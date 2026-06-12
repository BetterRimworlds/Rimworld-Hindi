// ==== Source/LanguageWorker_Hindi.cs ====
using System;
using System.Collections.Generic;
using System.Text;
using Verse;

namespace BetterRimworlds
{
    public class LanguageWorker_Hindi : LanguageWorker
    {
        // Hindi grammar notes:
        // - Articles: Like Urdu/Sanskrit, Hindi has no definite or indefinite articles.
        // - Plurals: Two-case system (singular for 1, plural for everything else).
        // - Ordinals: Gender-sensitive (Masc: वाँ / Fem: वीं).

        public override string WithIndefiniteArticle(string str, Gender gender, bool plural = false, bool name = false)
        {
            if (str.NullOrEmpty())
                return "";

            // Names or Hindi nouns do not require an article prefix in this context.
            return str;
        }

        public override string WithDefiniteArticle(string str, Gender gender, bool plural = false, bool name = false)
        {
            if (str.NullOrEmpty()) return "";
            return str;
        }

        public override string PostProcessed(string str)
        {
            if (str.NullOrEmpty()) 
                return base.PostProcessed(str);

            // Run standard space merging first
            string processed = base.PostProcessed(str);

            // Apply a manual font-shaping pass to fix RimWorld's LTR vowel rendering bug
            return ShapeDevanagariForUnity(processed);
        }

        private string ShapeDevanagariForUnity(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            StringBuilder sb = new StringBuilder();
            char[] chars = input.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                // Fix the short-i vowel bug (chhoti ee matra: \u093f)
                // If a consonant is followed by a short-i, RimWorld displays it as: [Consonant][Vowel]
                // We need to look ahead, grab it, and place it BEFORE the consonant/conjunct group.
                if (i < chars.Length - 1 && chars[i + 1] == '\u093F')
                {
                    // Check if it's a half-letter conjunct group (e.g., 'स्' + 'त' in सिस्टम)
                    // Devanagari Virama (halant) is \u094D
                    if (i > 1 && chars[i - 1] == '\u094D')
                    {
                        // Trace backward to find the absolute beginning of the consonant cluster
                        int startCluster = i - 2;
                        while (startCluster > 0 && chars[startCluster] == '\u094D')
                        {
                            startCluster -= 2; // Jump back past previous half-letters
                        }
                        
                        // Safely pull the vowel to the front of the entire cluster
                        sb.Insert(sb.Length - (i - startCluster), '\u093F');
                        sb.Append(chars[i]);
                        i++; // Skip the vowel on the next cycle
                        continue;
                    }

                    // Standard single consonant swap (e.g., 'न' + 'ि' -> 'िन')
                    sb.Append('\u093F');
                    sb.Append(chars[i]);
                    i++; // Skip the vowel on next cycle
                    continue;
                }

                sb.Append(chars[i]);
            }

            return sb.ToString();
        }

        public override string ResolveNumCase(float number, List<string> args)
        {
            if (args == null || args.Count == 0)
                return null;

            List<string> forms = new List<string>(args.Count);
            for (int i = 0; i < args.Count; i++)
                forms.Add(args[i]?.Trim('\'') ?? "");

            // Handle decimal numbers (usually treated as plural/other)
            if (number - (float)Math.Floor(number) > float.Epsilon)
            {
                string form = forms.Count > 1 ? forms[1] : forms[0];
                return $"{number} {form}";
            }

            int n = (int)number;

            // Hindi Plural Rule: 1 is singular, everything else is plural.
            if (forms.Count == 1)
                return $"{n} {forms[0]}";

            // If 3 forms are provided (one, several, many), map them logically.
            if (forms.Count >= 3)
            {
                string result = GetFormForNumber(n, forms[0], forms[1], forms[2]);
                return $"{n} {result}";
            }

            // Standard 2-form fallback (one, other)
            string chosen = (n == 1) ? forms[0] : forms[1];
            return $"{n} {chosen}";
        }

        protected override string GetFormForNumber(int num, string formOne, string formSeveral, string formMany)
        {
            // Hindi mapping for RimWorld's internal 3-form logic:
            if (num == 1)
                return formOne;
            
            // RimWorld often uses "several" for small counts (2-10).
            if (num >= 2 && num <= 10)
                return formSeveral;

            return formMany;
        }

        public override string OrdinalNumber(int number, Gender gender = Gender.None)
        {
            // Hindi ordinals are highly gender-dependent.
            if (gender == Gender.Female)
            {
                return number switch
                {
                    1 => "पहली",
                    2 => "दूसरी",
                    3 => "तीसरी",
                    4 => "चौथी",
                    5 => "पाँचवीं",
                    6 => "छठी",
                    7 => "सातवीं",
                    8 => "आठवीं",
                    9 => "नौवीं",
                    10 => "दसवीं",
                    _ => number + "वीं"
                };
            }

            // Default/Masculine
            return number switch
            {
                1 => "पहला",
                2 => "दूसरा",
                3 => "तीसरा",
                4 => "चौथा",
                5 => "पाँचवाँ",
                6 => "छठा",
                7 => "सातवाँ",
                8 => "आठवाँ",
                9 => "नौवाँ",
                10 => "दसवाँ",
                _ => number + "वाँ"
            };
        }
    }
}


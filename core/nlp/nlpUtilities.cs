/*
    CuteAGI
    Copyright (C) 2007 Bob Mottram
    fuzzgun@gmail.com

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using cuteagi.core;

namespace cuteagi.nlp
{
	public class nlpUtilities
	{
	    /// <summary>
	    /// creates some common ranges
	    /// </summary>
	    public static atomCollection CreateRanges()
	    {
	        atomCollection ranges = new atomCollection();
	        ranges.SetName("Ranges");

	        /// phrases associated with a range of positive emotions
	        atomCollection emotions_positive = new atomCollection();
	        emotions_positive.SetName("RangeEmotionsPositive");
            String[] emotion_positive_str = {
                "better",
                "tidy",
                "neat",
                "welcome",
                "amusing",
                "initiative",
                "like",
                "invest",
                "investment",
                "growth",
                "lucky",
                "correct",
                "hired",
                "replenish",
                "respect",
                "respects",
                "respected",
                "encouraging",
                "enjoy",
                "pleasurable",
                "pleasure",
                "easy",
                "glad",
                "enjoys",
                "amused",
                "amusing",
                "entertained",
                "entertaining",
                "enjoyed",
                "treasure",
                "treasured",
                "pleasant",
                "joy",
                "happy",
                "good",
                "good at",
                "happiness",
                "popular",
                "esteem",
                "worthy",
                "worthwhile",
                "loving",
                "positive",
                "protect",
                "encouraged",
                "encourage",
                "working",
                "fascinate",
                "fascinated",
                "fascinating",
                "poetic",
                "interested",
                "interesting",
                "genuis",
                "freedom",                
                "enjoyable",
                "exciting",
                "pleasing",
                "pleased",
                "increase",
                "increased",
                "joyous",
                "confident",
                "confidence",
                "peace",
                "peacefull",
                "calm",
                "bright",
                "rosy",
                "kindness",
                "goodness",
                "divine",
                "divinely",
                "fantastic",
                "fabulous",
                "funny",
                "devoted",
                "refreshing",
                "refreshed",
                "enthusiast",
                "enthusiastic",
                "enthusiastically",
                "elation",
                "enthused",
                "enthusiasm",
                "love",
                "adore",
                "loved"
            };
	        emotions_positive.CreateRange(emotion_positive_str);
	        ranges.Add(emotions_positive);

	        /// phrases associated with a range of negative emotions
	        atomCollection emotions_negative = new atomCollection();
	        emotions_negative.SetName("RangeEmotionsNegative");
            String[] emotion_negative_str = {
                "misslead",
                "surrender",
                "hoax",
                "never",
                "dumb",
                "unnecessary",
                "unsophisticated",
                "messy",
                "worse",
                "worse than",
                "stumble",
                "stumbling",
                "frantic",
                "untidy",
                "difficult",
                "hard",
                "trouble",
                "troublesome",
                "stole",
                "steal",
                "stolen",
                "disslike",
                "frustrate",
                "frustration",
                "exclusion",
                "fired",
                "sacked",
                "redundant",
                "criticised",
                "jealous",
                "fear",
                "feared",
                "fearfull",
                "nowhere",
                "unpopular",
                "angry",
                "deluded",
                "delusion",
                "deserted",
                "worthless",
                "criticising",
                "bicker",
                "argue",
                "decline",
                "reduce",
                "cut",
                "unlucky",
                "empty",
                "scary",
                "wrong",
                "go wrong",
                "gone wrong",
                "going wrong",
                "not working",
                "scared",
                "scare",
                "sad",
                "crooked",
                "scam",
                "criticism",
                "criticise",
                "imposed",
                "violent",
                "violence",
                "slave",
                "fighting",
                "paranoia",
                "paranoid",
                "propaganda",
                "fraud",
                "horror",
                "horrible",
                "disturbing",
                "robbed",
                "hate",
                "hated",
                "hates",
                "harm",
                "unhappy",
                "kill",
                "kills",
                "killed",
                "killing",
                "death",
                "grave",
                "victim",
                "killing",
                "offensive",
                "evil",
                "hysterical",
                "hysteria",
                "shot",
                "genocide",
                "liar",
                "lies",
                "lied",
                "frightening",
                "frightened",
                "spam",
                "damn",
                "darn",
                "bad",
                "too bad",
                "scream",
                "screamed",
                "screaming",
                "arrested",
                "virus",
                "bomb",
                "boring",
                "fuck",
                "fucked",
                "shoot",
                "incorrect",
                "shot",
                "shooter",
                "shooting",
                "fucker",
                "fucking",
                "shit",
                "bitch",
                "dont like",
                "bitchy",
                "bitching",
                "gun",
                "guns",
                "weapon",
                "weapons",
                "war",
                "rifle",
                "lonely",
                "loser",
                "invade",
                "invasion",
                "disease",
                "sick",
                "ill",
                "illness",
                "sickness",
                "damaging",
                "damaged",
                "damage",
                "unloved",
                "devastating",
                "devastated",
                "devastate"
            };
	        emotions_negative.CreateRange(emotion_negative_str);
	        ranges.Add(emotions_negative);

	        /// phrases associated with a range of proximities
	        atomCollection proximities = new atomCollection();
	        proximities.SetName("RangeProximity");
	        String[] proximity_str = {	            
	            "very close", "very near", "short", "nearby", "close", "near",
	            "parted", "distant", "far apart", "long way", "far away", 
	            "far apart", "far from", "far out", "distal"
	        };
	        proximities.CreateRange(proximity_str);
	        ranges.Add(proximities);

	        /// phrases associated with a range of amusement
	        atomCollection amusement = new atomCollection();
	        amusement.SetName("RangeAmusement");
	        String[] amusement_str = {	            
	            "very boring", "boring", "boredom", "bores", "tedious", "tedium", "irksome",
	            "tiring", "tiresome", "uninteresting", "unexciting", "unexcited", "unimaginitive", 
	            "amusing", "amused", "amuses", "amuse", "entertain", "entertains",
	            "entertaining", "entertained",
	            "enjoy", "enjoying", "enjoyed", "enjoyable",
	            "enjoys", "excitement", "excite", "excites", "exciting", "excited",
	            "fun", "funny", "humor", "humour", "humourous", "humorous", "thrill",
	            "thrilling", "thriller", "thrilled"
	        };
	        amusement.CreateRange(amusement_str);
	        ranges.Add(amusement);


	        /// phrases associated with a range of cognitive ability
	        atomCollection cognitive = new atomCollection();
	        cognitive.SetName("RangeCognitive");
	        String[] cognitive_str = {	            
	            "mad", "madness", "insane", "insanity", "idiot", "crazy", "loony",
	            "lunatic", "craziness", "crazed", "idiotic", "retarded", 
	            "unintelligent", "not very smart", "not as smart", "not as clever",
	            "average intelligence", "clever", "smart", "very smart", "very clever",
	            "super smart", "gifted", "genius", "prodigy", "enlightened", 
	            "ingenius", "superintelligence"
	        };
	        cognitive.CreateRange(cognitive_str);
	        ranges.Add(cognitive);

	        /// phrases associated with a range of realness
	        atomCollection realness = new atomCollection();
	        realness.SetName("RangeRealness");
	        String[] realness_str = {
	            "dissbelief", "dream", "beggars belief", "ridiculous", "absurd", "absurdity",
	            "absurdness", "dreaming", "dreamy", "dreamer",
	            "hallucination", "hallucinatory", "hallucinations", "hallucinating",
	            "fantasies", "fantastical", "fantasy", "imagined", 
	            "imaginary", "imagination", "imaginitive", "imagining",
	            "unbelievable", "don't believe", "doesn't believe",
	            "dont believe", "doesnt believe", "unrealistic",
	            "fiction", "fictional", "unreal", "make believe", "pretend",
	            "credible", "believable", "reality", "realist", "real", 
	            "real life", "realistic", "realism"
	        };
	        realness.CreateRange(realness_str);
	        ranges.Add(realness);

	        /// phrases associated with a range of strengths
	        atomCollection strengths = new atomCollection();
	        strengths.SetName("RangeStrength");
	        String[] strength_str = {
	            "frail", "frailness", "frailty", "frailer", "feeble", "feebleness",
	            "fragile", "fragility", "fragileness",
	            "weak", "weaker", "weakness", "weaknesses", 
	            "soft", "softness", "softened", "softer",
	            "flimsy", "flimsiness", "robust", "robustness", "robustly",
	            "strong", "strength", "strengthen", "strengthened", "stronger", "very strong",
	            "tough", "toughness", "hard", "hardness", "harden", "hardened"
	        };
	        strengths.CreateRange(strength_str);
	        ranges.Add(strengths);

	        /// phrases associated with a range of heights
	        atomCollection heights = new atomCollection();
	        heights.SetName("RangeHeight");
	        String[] height_str = {
	            "low", "diminutive", "tiny", "petite", 
	            "dwarf", "dwarfed", "midget", "very short", "short", "shortened", "shortens", "shorter", "shortness",
	            "below average height", "average height", "above average height",
	            "tall", "taller", "tallness", "high", "higher", "very tall", "lofty", "loftier", "oversized", "oversize", 
	            "sky high", "titanic", "king sized", "king size", "giant", "gargantuan", "gigantic"
	        };
	        strengths.CreateRange(strength_str);
	        ranges.Add(strengths);

	        /// phrases associated with a range of widths
	        atomCollection widths = new atomCollection();
	        widths.SetName("RangeWidth");
	        String[] width_str = {
	            "extremely narrow", "very narrow", "incredibly narrow", "narrow", "narrowness", "narrower", 
	            "slim", "slimmer", "slimness", "slimming",
	            "wide", "fat", "fatter", "broader", "broaden", "broadens", 
	            "wider", "widen", "widened", "width"
	        };
	        widths.CreateRange(width_str);
	        ranges.Add(widths);

	        /// phrases associated with a range of weights
	        atomCollection weights = new atomCollection();
	        weights.SetName("RangeWeight");
	        String[] weight_str = {
	            "weightless", "light weight", "featherweight", 
	            "light", "heavy", "heavily", "very heavy", "weighty", "massive", "massively",
	            "supermassive", "colossal"
	        };
	        weights.CreateRange(weight_str);
	        ranges.Add(weights);

	        /// phrases associated with a range of heat
	        atomCollection heats = new atomCollection();
	        heats.SetName("RangeHeat");
	        String[] heat_str = {
	            "extremely cold", "very cold", "ice cold", "freezing", "freeze", "freezer",
	            "cold", "colder", "cooler", "cools", "cooling", "cool",
	            "tepid", "lukewarm", "warm", "warmer", "warmed", "warms", "warming",
	            "hot", "hotter", "boils", "boil", "boiling", "boiled", 
	            "scorched", "scorching", "scorch", "roast", "roasting", "roasted"
	        };
	        heats.CreateRange(heat_str);
	        ranges.Add(heats);

	        /// phrases associated with a range of aggressiveness
	        atomCollection aggressions = new atomCollection();
	        aggressions.SetName("RangeAggression");
	        String[] aggression_str = {
	            "chordial", "hospitable", "hospitableness", "hospitality",
	            "peaceful", "peace", "non violent", "non voilence", "non violently",
	            "aggressive", "aggressively", "aggressiveness",
	            "violent", "violently", "violence",
	            "hostile", "hostility", "hostilities",
	            "war", "warlike", "waring", "warlords", "combat", "fight",
	        };
	        aggressions.CreateRange(aggression_str);
	        ranges.Add(aggressions);

	        /// phrases associated with a range of liquidity
	        atomCollection liquidities = new atomCollection();
	        liquidities.SetName("RangeLiquidity");
	        String[] liquidity_str = {
	            "desert", "dry", "dryness", "arid", "parched", "dust", "dusty",
	            "damp", "dampened", "moist", "moistened", "watery", "flood", "flooded",
	            "rain", "rainy", "raining", "rained",
	            "sea", "river", "seas", "rivers"
	        };
	        liquidities.CreateRange(liquidity_str);
	        ranges.Add(liquidities);

	        /// phrases associated with a range of visibility
	        atomCollection visibility = new atomCollection();
	        visibility.SetName("RangeVisibility");
	        String[] visibility_str = {
	            "invisible", "poor visibility", "hidden", "hide", "unclear", "shadowy", 
	            "murky", "dingy", "opaque", "visible",
	            "clearly visible", "clearly seen", "clear to see", "clarity", "clear"
	        };
	        visibility.CreateRange(visibility_str);
	        ranges.Add(visibility);

	        /// phrases associated with a range of temporal events
	        atomCollection temporal = new atomCollection();
	        temporal.SetName("RangeTemporal");
	        String[] temporal_str = {
	            "not much time", "little time", "hardly any time",
	            "million years ago", "prehistoric", "long ago", "long time ago", "ancient",
	            "centuries ago", "century ago", "last century", "last centurys", "last century's", "previous century",
	            "decade ago", "decades ago", "last decade", "last decades", "last decade's", "previous decade",
	            "years ago", "past",
	            "last year", "last years", "last year's", "past year", "past years", "past year's", "previous year", "previous years", "previous year's",  
	            "last month", "last month's", "last months", "past month", "past month's", "past months", "previous month", "previous month's", "previous months", 
	            "last week", "last weeks", "last week's", "past week", "past weeks", "past week's",
	            "previously", "previous", "before",
	            "this month's", "this month", "this months", "current month", "current months", "current_month's",
	            "this week", "this weeks", "this week's", "current week", "current weeks", "current week's",	            
	            "last few days", "last days", "previous day", "recent", "recently", "earlier", 
	            "yesterday", "today", "currently", "now",
	            "tomorrow", "tomorrows", "tomorrow's", "future plans", "a while", 
	            "next day", "next days", "next day's", "following day", "following days", "following day's",
	            "next week", "next weeks", "next week's", "following weeks", "following week",
	            "next month", "next month's", "next months", "following months", "following month",
	            "next year", "next years", "years", "next year's", "following years", "following year", "many years",
	            "next decade", "next decades", "next decade's", "future",
	            "years ahead", "years to come", "years from now", "into the future", "in the future", "in future",
	            "centuries ahead", "next century", "next centurys", "next century's", "next century",
	            "centuries to come", "centuries in the future", "centuries into the future", "far future", "distant future",
	            "long time"
	        };
	        temporal.CreateRange(temporal_str);
	        ranges.Add(temporal);

	        /// phrases associated with a range of frequencies
	        atomCollection frequencies = new atomCollection();
	        frequencies.SetName("RangeFrequency");
	        String[] frequency_str = {
	            "far from certain", "not ever", "never", "unheard of", "exceptionally rare", "rarity", "hardly ever", "rare", "rarely", "scarce", "infrequent", "infrequently", 
	            "few occasions", "occasional", "occasionally", "periodically", "periodic", "time to time", "unreliable", "unreliably",
	            "usually", "usual", "typical", "more often than not", "multiple times", "many times", "frequent", "frequently", "often", "very often", "always", "every time",
	            "near certain", "near certainty", "certain", "certainty", "dead cert"
	        };
	        frequencies.CreateRange(frequency_str);
	        ranges.Add(frequencies);
	        
	        /// phrases associated with a range of speeds
	        atomCollection speeds = new atomCollection();
	        speeds.SetName("RangeSpeed");
	        String[] speed_str = {
	            "incredibly slow", "very slow", "slowest", "much longer", "much slower", "significantly slower",
	            "hugely slower", "sluggish", "more slowly", "more gradually", "gradually", "slowly", "slower rate", "lower speed", "slow",	            
	            "longer", "slower rate", "lower rate", "smaller rate", "lesser rate", "slower than", "more slowly",
	            "equal speed", "equal time", "same time", "same speed", "same time", "same amount of time", "same duration", "same velocity", "same rate",
	            "more quickly", "quicker than", "faster than", "more quickly", "more rapidly", 
	            "fast", "timely", "faster", "rapidly", "faster rate", "quicker rate", "higher speed", "very rapidly", "very quickly", "speedily", "speed record", 
	            "quicker", "shorter time", "less time", "rapidly", "rapid", "record time", "record speed", "fastest", "quickest time", "fastest time"
	        };
	        speeds.CreateRange(speed_str);
	        ranges.Add(speeds);

	        /// phrases associated with a range of magnitudes
	        atomCollection magnitudes = new atomCollection();
	        magnitudes.SetName("RangeMagnitude");
	        String[] magnitude_str = {
	            "vastly less", "vastly under", "vastly below", "vastly fewer", "massively less", "significantly below",
	            "hugely less", "substantially less", "substantially smaller", "significantly less", "significantly under",
	            "massively under", "hugely under", "well below", "far fewer", "much fewer", "very few", 
	            "far smaller", "much smaller", "far less", "not much", "not many", "not very much", "not very many",
	            "much less", "much lesser", "lesser", "fewer", "smaller", "below", "less sizable", "less extensive", "less extensively", "small",	            
	            "slightly smaller", "slightly less", "just under", "just below", "slightly below",
	            "marginally less", "marginally under", "marginally smaller", "just smaller", 
	            "a little smaller", "bit smaller", "a little less", "bit less", "marginally less",
	            "just less than",
	            "same size", "equivalent to", "equal to", "equalling", "equivalent", "equals", "same as",
	            "slightly larger", "a little larger", "bit more", "little more", "bit larger", "bit bigger", 
	            "a little bigger", "marginally bigger", "marginally more", "marginally greater", "little over",
	            "slightly larger", "slightly bigger", "just bigger", "just larger", "little larger", "little bigger", 
	            "more substantial", "greater than", "more than", "bigger than", "very many", "quite a few",
	            "just over", "more sizable", "more substantial", "more substantive", "huger", "more extensive", "more extensively",
	            "considerably bigger", "considerably larger", "substantially bigger", "far bigger", "far larger", "substantially larger", "much larger", "much bigger",
	            "far greater", "a lot of", "great deal", "far more", "far bigger", "multitude", "multitudes",
	            "significantly bigger", "significantly larger", "significantly more", "significantly above", "massively bigger", "massively larger",
	            "vastly bigger", "vastly larger", "vastly greater", "vastly over", "vastly more", "enormous", "huge", "gigantic",
	            "hugely bigger", "hugely larger", "hugely greater", "hugely over", "hugely more", "colossal", "colossally"
	        };
	        magnitudes.CreateRange(magnitude_str);
	        ranges.Add(magnitudes);
	        
	        return(ranges);
	    }	
	    
        /// <summary>
        /// removes any punctuation from the given text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private String removePunctuation(String text)
        {
            char[] chars = text.ToCharArray();
            String result = "";

            for (int i = 0; i < text.Length; i++)
            {
                if (((chars[i] > 96) && (chars[i] < 123)) || (chars[i] == ' '))
                    result += text.Substring(i, 1);
            }
            return (result);
        }

	    /// <summary>
	    /// does the given text contain a human relationship?
	    /// </summary>
	    public static DateTime ContainsDate(String text,
	                                        ref String day_name,
	                                        ref int day_number,
	                                        ref String month,
	                                        ref int year)
	    {
	        DateTime date = DateTime.MinValue;
	        String text_str = " " + text.ToLower() + " ";
	        
	        String[] day_str = {
	            "mon", "tue", "wed", "thu", "fri", "sat", "sun", 
	            "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday"
	        };
	        String[] month_str = {
	            "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec",
	            "january", "febuary", "march", "april", "may", "june", "july", "august",
	            "september", "october", "november", "december"
	        };
	        
	        month = "";
	        int i = 0;
	        while ((i < month_str.Length) && (month == ""))
	        {
	            if (text_str.IndexOf(month_str[i]) > -1)
	            {
	                if (i >= 12)
	                    month = month_str[i];
	                else
	                    month = month_str[i + 12];
	            }
	            i++;
	        }

	        day_name = "";
	        i = 0;
	        while ((i < day_str.Length) && (day_name == ""))
	        {
	            if (text_str.IndexOf(day_str[i]) > -1)
	                day_name = day_str[i];
	            i++;
	        }
	        
	        String[] word = text_str.Split(' ');
	        String dateformat = "";
	        day_number = -1;
	        year = -1;
	        for (i = 0; i < word.Length; i++)
	        {
	            if ((word[i].EndsWith("th")) || 
	                (word[i].EndsWith("rd")) || 
	                (word[i].EndsWith("st")))
	                word[i] = word[i].Substring(0, word[i].Length-2);
	        
	            char[] ch = word[i].ToCharArray();
	            int slash_count = 0;
	            int j = 0;
	            bool non_numeric = false;
	            while ((j < ch.Length) && (!non_numeric))
	            {
	                if ((!char.IsDigit(ch[j])) &&
	                    (ch[j] != '/')) non_numeric = true;
	                    
	                if (ch[j] == '/') slash_count++;
	                j++;
	            }
	            
	            if ((!non_numeric) && (ch.Length > 0))
	            {
	                if (slash_count == 2)
	                {
	                    dateformat = word[i];
	                }
	                else
	                {
	                    int n = Convert.ToInt32(word[i]); 
	                    if ((n <= 31) && (day_number == -1)) 
	                        day_number = n;
	                    else
	                        year = n;
	                }
	            }
	        }
	        
	        if (dateformat == "")
	        {
	            if ((day_name != "") && (day_number > -1) && (month != "") && (year > -1))
	            {
	                dateformat = day_name + " " + day_number.ToString() + " " + month + " " + year.ToString();
	            }
	            else
	            {
	                if ((day_number > -1) && (month != "") && (year > -1))
	                {
	                    dateformat = day_number.ToString() + " " + month + " " + year.ToString();
	                }
	            }
	        }
	        
	        if (dateformat != "")
	        {
	            // make sure that dates are in the format dd/mm/yyyy
	            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");
	            
	            bool date_parsed = false;
	            
	            try
	            {
	                date = DateTime.Parse(dateformat, culture);
	                date_parsed = true;
	            }
	            catch {}
	            
	            if (!date_parsed)
	            {
	                // if british format dates fail try the default american format
	                try
	                {
	                    date = DateTime.Parse(dateformat);
	                }
	                catch {}
	                }
	        }
	        
	        return(date);
	    }

	    /// <summary>
	    /// does the given text contain a human relationship?
	    /// </summary>
	    public static String ContainsRelationship(String text, ref bool plural)
	    {
	        plural = false;
	        String relationship = "";
	        String text_str = " " + text.ToLower() + " ";

            String[] RELATIONSHIP_TYPES = { 
            "Ally",
            "Apprentice",
            "Aunt",
            "Babysitter",
            "Boss",
            "Boyfriend",
            "Brother",
            "Carer",
            "Celebrity",
            "Co-worker",
            "contractor",
            "Daughter",
            "Doctor",
            "Employee",
            "Enemy",
            "Father",
            "Friend",
            "Girlfriend",
            "Grandfather",
            "Grandmother",
            "Great Grandfather",
            "Great Grandmother",
            "Husband",
            "Lover",
            "Mentor",
            "Mother",
            "Nephew",
            "Niece",
            "Nurse",
            "Opponent",
            "Partner",
            "Pet",
            "Politician",
            "Sister",
            "Son",
            "Step Brother",
            "Step Daughter",
            "Step Father",
            "Step Mother",
            "Step Sister",
            "Step Son",
            "Student",
            "Teacher",
            "Uncle",
            "Wife" };
            
            int i = 0;
            while ((i < RELATIONSHIP_TYPES.Length) && (relationship == ""))
            {
                if (text_str.IndexOf(" " + RELATIONSHIP_TYPES[i].ToLower() + " ") > -1)
                    relationship = RELATIONSHIP_TYPES[i].ToLower();
                
                if (relationship == "")
                {
                    if (text_str.IndexOf(" " + RELATIONSHIP_TYPES[i].ToLower() + "s ") > -1)
                    {
                        relationship = RELATIONSHIP_TYPES[i].ToLower();
                        plural = true;
                    }
                }
                
                if (relationship == "")
                {
                    if (text_str.IndexOf(" " + RELATIONSHIP_TYPES[i].ToLower() + "'s ") > -1)
                        relationship = RELATIONSHIP_TYPES[i].ToLower();
                }
                
                i++;
            }
	        
	        return(relationship);
	    }
	
	    /// <summary>
	    /// does the given text contain a yes or no type response
	    /// </summary>
	    public static String ContainsYesNoConfirmation(String text)
	    {
	        String confirmation = "";	        
	        String text_str = " " + text.ToLower() + " ";
	        
	        String[] yes_words = {
	            "yes", "y", "indeed", "yep", "yup", "affirmative", "afirmative", "confirmed",
	            "yeah", "aye", "true", "think so", "believe so", "it is", "it was", "true",
	            "agree"
	        };
	        String[] no_words = {
	            "no", "n", "nope", "not so", "false", "unconfirmed", "dissagree",
	            "non", "dont", "don't", "not", "nah"
	        };
	        String[] uncertain_words = {
	            "not sure", "don't know", "dont know", "maybe", "who knows", 
	            "possible", "unknown", "mystery", "mysterious", "not at all sure",
	            "not really sure", "not very sure", "not confident"
	        };
	        
	        int i = 0;
	        while ((i < yes_words.Length) && (confirmation == ""))
	        {
	            if (text_str.IndexOf(" " + yes_words[i] + " ") > -1) confirmation = "yes";
	            i++;
	        }

            bool uncertain = false;
	        i = 0;
	        while ((i < uncertain_words.Length) && (confirmation == ""))
	        {
	            if (text_str.IndexOf(" " + uncertain_words[i] + " ") > -1)
	            {
	                confirmation = "unknown";
	                uncertain = true;
	            }
	            i++;
	        }

            if (!uncertain)
            {
	            i = 0;
	            while ((i < no_words.Length) && (confirmation == ""))
	            {
	                if (text_str.IndexOf(" " + no_words[i] + " ") > -1) confirmation = "no";
	                i++;
	            }
	        }
	        
	        return(confirmation);
	    }
	    
	    /// <summary>
	    /// uses the search text to split the given text into two or three sections
	    /// </summary>
	    public static String[] SplitWithString(String text, String search_text)
	    {
	        text = text.ToLower();
	        text = text.Trim();
	        int idx = text.IndexOf(search_text);
	        if (idx > -1)
	        {
	            String[] result = null;
	            if (idx == 0)
	            {
	                result = new String[2];
	                result[0] = search_text;
	                result[1] = text.Substring(search_text.Length, text.Length - search_text.Length);
	            }
	            else
	            {
	                if (idx == text.Length - search_text.Length)
	                {
   	                    result = new String[2];
	                    result[0] = text.Substring(0, idx);
	                    result[1] = text.Substring(idx, text.Length - idx);
	                }
	                else
	                {
   	                    result = new String[3];
	                    result[0] = text.Substring(0, idx);
	                    result[1] = text.Substring(idx, search_text.Length);
	                    result[2] = text.Substring(idx + search_text.Length, text.Length - idx - search_text.Length);
	                }
	            }
	            return(result);
	        }
	        else return(null);
	    }
	}
}

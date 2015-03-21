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
using System.Collections;

namespace cuteagi.nlp
{
    /// <summary>
    /// various functions related to verbs
    /// </summary>
	public class nlpVerbs
	{
        private String[,] irregular_verbs;

	    #region "constructors"
	    
	    public nlpVerbs()
	    {
	        
	    }
	    
	    #endregion
	    
	    #region "finding verbs"
	
        /// <summary>
        /// if the given word is an irregular verb return its index
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private int isIrregularVerb(String word)
        {
            int index = -1;
            int i = 0;
            int j, k;
            bool found = false;
            String verb;
            String[] verbs;

            if (irregular_verbs == null) createIrregularVerbs();

            while ((i < irregular_verbs.GetLength(0)) && (!found))
            {
                for (j = 0; j < 3; j++)
                {
                    verb = irregular_verbs[i, j];
                    if (verb.IndexOf(",") == -1)
                    {
                        if (verb == word) { found = true; }
                    }
                    else
                    {
                        verbs = verb.Split(',');
                        for (k = 0; k < verbs.Length; k++)
                            if (verbs[k] == word) found = true;
                    }
                }
                if (!found) i++;
            }
            if (found) index = i;
            return (index);
        }
        
        /// <summary>
        /// returns a list of verbs contained within the given sentence
        /// </summary>
        /// <param name="sentence">sentence to be analysed</param>
        /// <returns>list of verbs</returns>
        public ArrayList GetVerbs(String sentence)
        {
            ArrayList verbs = new ArrayList();
            String[] words = sentence.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (isIrregularVerb(words[i]) > -1)
                {
                    verbs.Add(words[i]);
                }
                else
                {
                    if (words[i].Length > 3)
                        if ((words[i].Substring(words[i].Length - 3) == "ing") && (words[i] != "morning")) verbs.Add(words[i]);

                    if (words[i].Length > 2)
                        if (words[i].Substring(words[i].Length - 2) == "ed") verbs.Add(words[i]);
                }
            }
            return(verbs);
        }
        
        /// <summary>
        /// creates a big lookup table for irregular verbs
        /// </summary>
        private void createIrregularVerbs()
        {
            irregular_verbs = new String[260, 3];
        
            irregular_verbs[0,0] = "arise";
            irregular_verbs[0,1] = "arose";
            irregular_verbs[0,2] = "arisen";

            irregular_verbs[1,0] = "awake";
            irregular_verbs[1,1] = "awoke";
            irregular_verbs[1,2] = "awoken";

            irregular_verbs[2,0] = "be";
            irregular_verbs[2,1] = "am,is,are";
            irregular_verbs[2,2] = "was,were,been";

            irregular_verbs[3,0] = "bear";
            irregular_verbs[3,1] = "bore";
            irregular_verbs[3,2] = "born,borne";

            irregular_verbs[4,0] = "beat";
            irregular_verbs[4,1] = "beat";
            irregular_verbs[4,2] = "beaten";

            irregular_verbs[5,0] = "become";
            irregular_verbs[5,1] = "became";
            irregular_verbs[5,2] = "become";

            irregular_verbs[6,0] = "befall";
            irregular_verbs[6,1] = "befell";
            irregular_verbs[6,2] = "befallen";

            irregular_verbs[7,0] = "begin";
            irregular_verbs[7,1] = "began";
            irregular_verbs[7,2] = "begun";

            irregular_verbs[8,0] = "behold";
            irregular_verbs[8,1] = "beheld";
            irregular_verbs[8,2] = "beheld";

            irregular_verbs[9,0] = "bend";
            irregular_verbs[9,1] = "bent";
            irregular_verbs[9,2] = "bent";

            irregular_verbs[10,0] = "bereave";
            irregular_verbs[10,1] = "bereaved,bereft";
            irregular_verbs[10,2] = "bereaved,bereft";

            irregular_verbs[11,0] = "beseech";
            irregular_verbs[11,1] = "besought";
            irregular_verbs[11,2] = "besought";

            irregular_verbs[12,0] = "beset";
            irregular_verbs[12,1] = "beset";
            irregular_verbs[12,2] = "beset";

            irregular_verbs[13,0] = "bet";
            irregular_verbs[13,1] = "bet,betted";
            irregular_verbs[13,2] = "bet,betted";

            irregular_verbs[14,0] = "bid";
            irregular_verbs[14,1] = "bade, bid";
            irregular_verbs[14,2] = "bidden,bid";

            irregular_verbs[15,0] = "bind";
            irregular_verbs[15,1] = "bound";
            irregular_verbs[15,2] = "bound";

            irregular_verbs[16,0] = "bite";
            irregular_verbs[16,1] = "bit";
            irregular_verbs[16,2] = "bitten";

            irregular_verbs[17,0] = "bleed";
            irregular_verbs[17,1] = "bled";
            irregular_verbs[17,2] = "bled";

            irregular_verbs[18,0] = "blow";
            irregular_verbs[18,1] = "blew";
            irregular_verbs[18,2] = "blown";

            irregular_verbs[19,0] = "break";
            irregular_verbs[19,1] = "broke";
            irregular_verbs[19,2] = "broken";

            irregular_verbs[20,0] = "breed";
            irregular_verbs[20,1] = "bred";
            irregular_verbs[20,2] = "bred";

            irregular_verbs[21,0] = "bring";
            irregular_verbs[21,1] = "brought";
            irregular_verbs[21,2] = "brought";

            irregular_verbs[22,0] = "broadcast";
            irregular_verbs[22,1] = "broadcast";
            irregular_verbs[22,2] = "broadcast";

            irregular_verbs[23,0] = "browbeat";
            irregular_verbs[23,1] = "browbeat";
            irregular_verbs[23,2] = "browbeaten";

            irregular_verbs[24,0] = "build";
            irregular_verbs[24,1] = "built";
            irregular_verbs[24,2] = "built";

            irregular_verbs[25,0] = "burn";
            irregular_verbs[25,1] = "burned,burnt";
            irregular_verbs[25,2] = "burned,burnt";

            irregular_verbs[26,0] = "burst";
            irregular_verbs[26,1] = "burst";
            irregular_verbs[26,2] = "burst";

            irregular_verbs[27,0] = "bust";
            irregular_verbs[27,1] = "busted,bust";
            irregular_verbs[27,2] = "busted,bust";

            irregular_verbs[28,0] = "buy";
            irregular_verbs[28,1] = "bought";
            irregular_verbs[28,2] = "bought";

            irregular_verbs[29,0] = "can";
            irregular_verbs[29,1] = "could";
            irregular_verbs[29,2] = "could";

            irregular_verbs[30,0] = "cast";
            irregular_verbs[30,1] = "cast";
            irregular_verbs[30,2] = "cast";

            irregular_verbs[31,0] = "catch";
            irregular_verbs[31,1] = "caught";
            irregular_verbs[31,2] = "caught";

            irregular_verbs[32,0] = "chide";
            irregular_verbs[32,1] = "chid,chided,chode";
            irregular_verbs[32,2] = "chidden,chided";

            irregular_verbs[33,0] = "choose";
            irregular_verbs[33,1] = "chose";
            irregular_verbs[33,2] = "chosen";

            irregular_verbs[34,0] = "cleave";
            irregular_verbs[34,1] = "clove,cleft";
            irregular_verbs[34,2] = "cloven,cleft";

            irregular_verbs[35,0] = "cling";
            irregular_verbs[35,1] = "cling";
            irregular_verbs[35,2] = "cling";

            irregular_verbs[36,0] = "come";
            irregular_verbs[36,1] = "come";
            irregular_verbs[36,2] = "come";

            irregular_verbs[37,0] = "cost";
            irregular_verbs[37,1] = "cost";
            irregular_verbs[37,2] = "cost";

            irregular_verbs[38,0] = "creep";
            irregular_verbs[38,1] = "crept";
            irregular_verbs[38,2] = "crept";

            irregular_verbs[39,0] = "cut";
            irregular_verbs[39,1] = "cut";
            irregular_verbs[39,2] = "cut";

            irregular_verbs[40,0] = "deal";
            irregular_verbs[40,1] = "dealt";
            irregular_verbs[40,2] = "dealt";

            irregular_verbs[41,0] = "dig";
            irregular_verbs[41,1] = "dug";
            irregular_verbs[41,2] = "dug";

            irregular_verbs[42,0] = "dive";
            irregular_verbs[42,1] = "dived,dove";
            irregular_verbs[42,2] = "dived";

            irregular_verbs[43,0] = "do";
            irregular_verbs[43,1] = "did";
            irregular_verbs[43,2] = "done";

            irregular_verbs[44,0] = "draw";
            irregular_verbs[44,1] = "drew";
            irregular_verbs[44,2] = "drawn";

            irregular_verbs[45,0] = "dream";
            irregular_verbs[45,1] = "dreamt,dreamed";
            irregular_verbs[45,2] = "dreamt,dreamed";

            irregular_verbs[46,0] = "drink";
            irregular_verbs[46,1] = "drank";
            irregular_verbs[46,2] = "drunk";

            irregular_verbs[47,0] = "drive";
            irregular_verbs[47,1] = "drove";
            irregular_verbs[47,2] = "driven";

            irregular_verbs[48,0] = "dwell";
            irregular_verbs[48,1] = "dwelt,dwelled";
            irregular_verbs[48,2] = "dwelt,dwelled";

            irregular_verbs[49,0] = "eat";
            irregular_verbs[49,1] = "ate";
            irregular_verbs[49,2] = "eaten";

            irregular_verbs[50,0] = "fall";
            irregular_verbs[50,1] = "fell";
            irregular_verbs[50,2] = "fallen";

            irregular_verbs[51,0] = "feed";
            irregular_verbs[51,1] = "fed";
            irregular_verbs[51,2] = "fed";

            irregular_verbs[52,0] = "feel";
            irregular_verbs[52,1] = "felt";
            irregular_verbs[52,2] = "felt";

            irregular_verbs[53,0] = "fight";
            irregular_verbs[53,1] = "fought";
            irregular_verbs[53,2] = "fought";

            irregular_verbs[54,0] = "find";
            irregular_verbs[54,1] = "found";
            irregular_verbs[54,2] = "found";

            irregular_verbs[55,0] = "fit";
            irregular_verbs[55,1] = "fit,fitted";
            irregular_verbs[55,2] = "fit,fitted";

            irregular_verbs[56,0] = "flee";
            irregular_verbs[56,1] = "fled";
            irregular_verbs[56,2] = "fled";

            irregular_verbs[57,0] = "fling";
            irregular_verbs[57,1] = "flung";
            irregular_verbs[57,2] = "flung";

            irregular_verbs[58,0] = "fly";
            irregular_verbs[58,1] = "flew";
            irregular_verbs[58,2] = "flown";

            irregular_verbs[59,0] = "forbid";
            irregular_verbs[59,1] = "forbade";
            irregular_verbs[59,2] = "forbidden";

            irregular_verbs[60,0] = "forecast";
            irregular_verbs[60,1] = "forecast";
            irregular_verbs[60,2] = "forecast";

            irregular_verbs[61,0] = "forego";
            irregular_verbs[61,1] = "forewent";
            irregular_verbs[61,2] = "foregone";

            irregular_verbs[62,0] = "forgo";
            irregular_verbs[62,1] = "forwent";
            irregular_verbs[62,2] = "forgone";

            irregular_verbs[63,0] = "foresee";
            irregular_verbs[63,1] = "foresaw";
            irregular_verbs[63,2] = "foreseen";

            irregular_verbs[64,0] = "foretell";
            irregular_verbs[64,1] = "foretold";
            irregular_verbs[64,2] = "foretold";

            irregular_verbs[65,0] = "forget";
            irregular_verbs[65,1] = "forgot";
            irregular_verbs[65,2] = "forgotten";

            irregular_verbs[66,0] = "forgive";
            irregular_verbs[66,1] = "forgave";
            irregular_verbs[66,2] = "forgiven";

            irregular_verbs[67,0] = "forsake";
            irregular_verbs[67,1] = "forsook";
            irregular_verbs[67,2] = "forsaken";

            irregular_verbs[68,0] = "freeze";
            irregular_verbs[68,1] = "froze";
            irregular_verbs[68,2] = "frozen";

            irregular_verbs[69,0] = "get";
            irregular_verbs[69,1] = "got";
            irregular_verbs[69,2] = "got,gotten";

            irregular_verbs[70,0] = "give";
            irregular_verbs[70,1] = "gave";
            irregular_verbs[70,2] = "given";

            irregular_verbs[71,0] = "go";
            irregular_verbs[71,1] = "went";
            irregular_verbs[71,2] = "gone";

            irregular_verbs[72,0] = "grind";
            irregular_verbs[72,1] = "ground";
            irregular_verbs[72,2] = "ground";

            irregular_verbs[73,0] = "grow";
            irregular_verbs[73,1] = "grew";
            irregular_verbs[73,2] = "grown";

            irregular_verbs[74,0] = "hang";
            irregular_verbs[74,1] = "hung";
            irregular_verbs[74,2] = "hung";

            irregular_verbs[75,0] = "have";
            irregular_verbs[75,1] = "had";
            irregular_verbs[75,2] = "had";

            irregular_verbs[76,0] = "hear";
            irregular_verbs[76,1] = "heard";
            irregular_verbs[76,2] = "heard";

            irregular_verbs[77,0] = "heave";
            irregular_verbs[77,1] = "hove,heaved";
            irregular_verbs[77,2] = "hove,heaved";

            irregular_verbs[78,0] = "hew";
            irregular_verbs[78,1] = "hewed";
            irregular_verbs[78,2] = "hewn";

            irregular_verbs[79,0] = "hide";
            irregular_verbs[79,1] = "hid";
            irregular_verbs[79,2] = "hidden";

            irregular_verbs[80,0] = "hit";
            irregular_verbs[80,1] = "hit";
            irregular_verbs[80,2] = "hit";

            irregular_verbs[81,0] = "hold";
            irregular_verbs[81,1] = "held";
            irregular_verbs[81,2] = "held";

            irregular_verbs[82,0] = "hurt";
            irregular_verbs[82,1] = "hurt";
            irregular_verbs[82,2] = "hurt";

            irregular_verbs[83,0] = "input";
            irregular_verbs[83,1] = "input";
            irregular_verbs[83,2] = "input";

            irregular_verbs[84,0] = "inset";
            irregular_verbs[84,1] = "inset";
            irregular_verbs[84,2] = "inset";

            irregular_verbs[85,0] = "interbreed";
            irregular_verbs[85,1] = "interbred";
            irregular_verbs[85,2] = "interbred";

            irregular_verbs[86,0] = "interweave";
            irregular_verbs[86,1] = "interwove";
            irregular_verbs[86,2] = "interwoven";

            irregular_verbs[87,0] = "keep";
            irregular_verbs[87,1] = "kept";
            irregular_verbs[87,2] = "kept";

            irregular_verbs[88,0] = "kneel";
            irregular_verbs[88,1] = "knelt";
            irregular_verbs[88,2] = "knelt";

            irregular_verbs[89,0] = "knit";
            irregular_verbs[89,1] = "knit,knitted";
            irregular_verbs[89,2] = "knit,knitted";

            irregular_verbs[90,0] = "know";
            irregular_verbs[90,1] = "knew";
            irregular_verbs[90,2] = "known";

            irregular_verbs[91,0] = "lay";
            irregular_verbs[91,1] = "laid";
            irregular_verbs[91,2] = "laid";

            irregular_verbs[92,0] = "lead";
            irregular_verbs[92,1] = "led";
            irregular_verbs[92,2] = "led";

            irregular_verbs[93,0] = "lean";
            irregular_verbs[93,1] = "leaned,leant";
            irregular_verbs[93,2] = "leaned,leant";

            irregular_verbs[94,0] = "leap";
            irregular_verbs[94,1] = "leaped,leapt";
            irregular_verbs[94,2] = "leaped,leapt";

            irregular_verbs[95,0] = "learn";
            irregular_verbs[95,1] = "learned,learnt";
            irregular_verbs[95,2] = "learned,learnt";

            irregular_verbs[96,0] = "leave";
            irregular_verbs[96,1] = "left";
            irregular_verbs[96,2] = "left";

            irregular_verbs[97,0] = "lend";
            irregular_verbs[97,1] = "lent";
            irregular_verbs[97,2] = "lent";

            irregular_verbs[98,0] = "let";
            irregular_verbs[98,1] = "let";
            irregular_verbs[98,2] = "let";

            irregular_verbs[99,0] = "lie";
            irregular_verbs[99,1] = "lay";
            irregular_verbs[99,2] = "lain";

            irregular_verbs[100,0] = "light";
            irregular_verbs[100,1] = "lit,lighted";
            irregular_verbs[100,2] = "lit,lighted";

            irregular_verbs[101,0] = "lose";
            irregular_verbs[101,1] = "lost";
            irregular_verbs[101,2] = "lost";

            irregular_verbs[102,0] = "make";
            irregular_verbs[102,1] = "make";
            irregular_verbs[102,2] = "make";

            irregular_verbs[102,0] = "make";
            irregular_verbs[102,1] = "made";
            irregular_verbs[102,2] = "made";

            irregular_verbs[103,0] = "may";
            irregular_verbs[103,1] = "might";
            irregular_verbs[103,2] = "might";

            irregular_verbs[104,0] = "mean";
            irregular_verbs[104,1] = "meant";
            irregular_verbs[104,2] = "meant";

            irregular_verbs[105,0] = "meet";
            irregular_verbs[105,1] = "met";
            irregular_verbs[105,2] = "met";

            irregular_verbs[106,0] = "mishear";
            irregular_verbs[106,1] = "misheard";
            irregular_verbs[106,2] = "misheard";

            irregular_verbs[107,0] = "mislay";
            irregular_verbs[107,1] = "mislaid";
            irregular_verbs[107,2] = "mislaid";

            irregular_verbs[108,0] = "mislead";
            irregular_verbs[108,1] = "misled";
            irregular_verbs[108,2] = "misled";

            irregular_verbs[109,0] = "misread";
            irregular_verbs[109,1] = "misread";
            irregular_verbs[109,2] = "misread";

            irregular_verbs[110,0] = "misspell";
            irregular_verbs[110,1] = "misspelled,misspelt";
            irregular_verbs[110,2] = "misspelled,misspelt";

            irregular_verbs[111,0] = "mistake";
            irregular_verbs[111,1] = "mistook";
            irregular_verbs[111,2] = "mistaken";

            irregular_verbs[112,0] = "misunderstand";
            irregular_verbs[112,1] = "misunderstood";
            irregular_verbs[112,2] = "misunderstood";

            irregular_verbs[113,0] = "mow";
            irregular_verbs[113,1] = "mowed";
            irregular_verbs[113,2] = "mowed,mown";

            irregular_verbs[114,0] = "outbid";
            irregular_verbs[114,1] = "outbid";
            irregular_verbs[114,2] = "outbid";

            irregular_verbs[115,0] = "outdo";
            irregular_verbs[115,1] = "outdid";
            irregular_verbs[115,2] = "outdone";

            irregular_verbs[116,0] = "outgrow";
            irregular_verbs[116,1] = "outgrew";
            irregular_verbs[116,2] = "outgrown";

            irregular_verbs[117,0] = "outrun";
            irregular_verbs[117,1] = "outran";
            irregular_verbs[117,2] = "outrun";

            irregular_verbs[118,0] = "outsell";
            irregular_verbs[118,1] = "outsold";
            irregular_verbs[118,2] = "outsold";

            irregular_verbs[119,0] = "overcast";
            irregular_verbs[119,1] = "overcast";
            irregular_verbs[119,2] = "overcast";

            irregular_verbs[120,0] = "overcome";
            irregular_verbs[120,1] = "overcame";
            irregular_verbs[120,2] = "overcome";

            irregular_verbs[121,0] = "overdo";
            irregular_verbs[121,1] = "overdid";
            irregular_verbs[121,2] = "overdone";

            irregular_verbs[122,0] = "overdraw";
            irregular_verbs[122,1] = "overdrew";
            irregular_verbs[122,2] = "overdrawn";

            irregular_verbs[123,0] = "overeat";
            irregular_verbs[123,1] = "overate";
            irregular_verbs[123,2] = "overeaten";

            irregular_verbs[124,0] = "overhang";
            irregular_verbs[124,1] = "overhung";
            irregular_verbs[124,2] = "overhung";

            irregular_verbs[125,0] = "overhear";
            irregular_verbs[125,1] = "overheard";
            irregular_verbs[125,2] = "overheard";

            irregular_verbs[126,0] = "overlay";
            irregular_verbs[126,1] = "overlaid";
            irregular_verbs[126,2] = "overlaid";

            irregular_verbs[127,0] = "overlie";
            irregular_verbs[127,1] = "overlay";
            irregular_verbs[127,2] = "overlain";

            irregular_verbs[128,0] = "overpay";
            irregular_verbs[128,1] = "overpaid";
            irregular_verbs[128,2] = "overpaid";

            irregular_verbs[129,0] = "override";
            irregular_verbs[129,1] = "overrode";
            irregular_verbs[129,2] = "overridden";

            irregular_verbs[130,0] = "overrun";
            irregular_verbs[130,1] = "overran";
            irregular_verbs[130,2] = "overrun";

            irregular_verbs[131,0] = "oversee";
            irregular_verbs[131,1] = "oversaw";
            irregular_verbs[131,2] = "overseen";

            irregular_verbs[132,0] = "oversell";
            irregular_verbs[132,1] = "oversold";
            irregular_verbs[132,2] = "oversold";

            irregular_verbs[133,0] = "overshoot";
            irregular_verbs[133,1] = "overshot";
            irregular_verbs[133,2] = "overshot";

            irregular_verbs[134,0] = "oversleep";
            irregular_verbs[134,1] = "overslept";
            irregular_verbs[134,2] = "overslept";

            irregular_verbs[135,0] = "overtake";
            irregular_verbs[135,1] = "overtook";
            irregular_verbs[135,2] = "overtaken";

            irregular_verbs[136,0] = "overthrow";
            irregular_verbs[136,1] = "overthrew";
            irregular_verbs[136,2] = "overthrown";

            irregular_verbs[137,0] = "partake";
            irregular_verbs[137,1] = "partook";
            irregular_verbs[137,2] = "partaken";

            irregular_verbs[138,0] = "pay";
            irregular_verbs[138,1] = "paid";
            irregular_verbs[138,2] = "paid";

            irregular_verbs[139,0] = "plead";
            irregular_verbs[139,1] = "pleaded,pled";
            irregular_verbs[139,2] = "pleaded,pled";

            irregular_verbs[140,0] = "pre-set";
            irregular_verbs[140,1] = "pre-set";
            irregular_verbs[140,2] = "pre-set";

            irregular_verbs[141,0] = "proofread";
            irregular_verbs[141,1] = "proofread";
            irregular_verbs[141,2] = "proofread";

            irregular_verbs[142,0] = "prove";
            irregular_verbs[142,1] = "proved";
            irregular_verbs[142,2] = "proved,proven";

            irregular_verbs[143,0] = "put";
            irregular_verbs[143,1] = "put";
            irregular_verbs[143,2] = "put";

            irregular_verbs[144,0] = "quit";
            irregular_verbs[144,1] = "quit,quitted";
            irregular_verbs[144,2] = "quit,quitted";

            irregular_verbs[145,0] = "read";
            irregular_verbs[145,1] = "read";
            irregular_verbs[145,2] = "read";

            irregular_verbs[146,0] = "reave";
            irregular_verbs[146,1] = "reft";
            irregular_verbs[146,2] = "reft";

            irregular_verbs[147,0] = "rebind";
            irregular_verbs[147,1] = "rebound";
            irregular_verbs[147,2] = "rebound";

            irregular_verbs[148,0] = "rebuild";
            irregular_verbs[148,1] = "rebuilt";
            irregular_verbs[148,2] = "rebuilt";

            irregular_verbs[149,0] = "recast";
            irregular_verbs[149,1] = "recast";
            irregular_verbs[149,2] = "recast";

            irregular_verbs[150,0] = "rend";
            irregular_verbs[150,1] = "rent";
            irregular_verbs[150,2] = "rent";

            irregular_verbs[151,0] = "redo";
            irregular_verbs[151,1] = "redid";
            irregular_verbs[151,2] = "redone";

            irregular_verbs[152,0] = "re-lay";
            irregular_verbs[152,1] = "re-laid";
            irregular_verbs[152,2] = "re-laid";

            irregular_verbs[153,0] = "remake";
            irregular_verbs[153,1] = "remade";
            irregular_verbs[153,2] = "remade";

            irregular_verbs[154,0] = "repay";
            irregular_verbs[154,1] = "repaid";
            irregular_verbs[154,2] = "repaid";

            irregular_verbs[155,0] = "rerun";
            irregular_verbs[155,1] = "reran";
            irregular_verbs[155,2] = "rerun";

            irregular_verbs[156,0] = "resell";
            irregular_verbs[156,1] = "resold";
            irregular_verbs[156,2] = "resold";

            irregular_verbs[157,0] = "reset";
            irregular_verbs[157,1] = "reset";
            irregular_verbs[157,2] = "reset";

            irregular_verbs[158,0] = "rethink";
            irregular_verbs[158,1] = "rethought";
            irregular_verbs[158,2] = "rethought";

            irregular_verbs[159,0] = "rewind";
            irregular_verbs[159,1] = "rewound";
            irregular_verbs[159,2] = "rewound";

            irregular_verbs[160,0] = "rewrite";
            irregular_verbs[160,1] = "rewrote";
            irregular_verbs[160,2] = "rewritten";

            irregular_verbs[161,0] = "rid";
            irregular_verbs[161,1] = "rid";
            irregular_verbs[161,2] = "rid";

            irregular_verbs[162,0] = "ride";
            irregular_verbs[162,1] = "rode";
            irregular_verbs[162,2] = "ridden";

            irregular_verbs[163,0] = "ring";
            irregular_verbs[163,1] = "rang";
            irregular_verbs[163,2] = "rung";

            irregular_verbs[164,0] = "rise";
            irregular_verbs[164,1] = "rose";
            irregular_verbs[164,2] = "risen";

            irregular_verbs[165,0] = "rive";
            irregular_verbs[165,1] = "rived,rove";
            irregular_verbs[165,2] = "riven";

            irregular_verbs[166,0] = "run";
            irregular_verbs[166,1] = "ran";
            irregular_verbs[166,2] = "run";

            irregular_verbs[167,0] = "saw";
            irregular_verbs[167,1] = "sawed";
            irregular_verbs[167,2] = "sawed,sawn";

            irregular_verbs[168,0] = "say";
            irregular_verbs[168,1] = "said";
            irregular_verbs[168,2] = "said";

            irregular_verbs[169,0] = "see";
            irregular_verbs[169,1] = "saw";
            irregular_verbs[169,2] = "seen";

            irregular_verbs[170,0] = "seek";
            irregular_verbs[170,1] = "sought";
            irregular_verbs[170,2] = "sought";

            irregular_verbs[171,0] = "sell";
            irregular_verbs[171,1] = "sold";
            irregular_verbs[171,2] = "sold";

            irregular_verbs[172,0] = "send";
            irregular_verbs[172,1] = "sent";
            irregular_verbs[172,2] = "sent";

            irregular_verbs[173,0] = "set";
            irregular_verbs[173,1] = "set";
            irregular_verbs[173,2] = "set";

            irregular_verbs[174,0] = "sew";
            irregular_verbs[174,1] = "sewed";
            irregular_verbs[174,2] = "sewed,sewn";

            irregular_verbs[175,0] = "shake";
            irregular_verbs[175,1] = "shook";
            irregular_verbs[175,2] = "shaken";

            irregular_verbs[176,0] = "shave";
            irregular_verbs[176,1] = "shaved";
            irregular_verbs[176,2] = "shaved,shaven";

            irregular_verbs[177,0] = "shear";
            irregular_verbs[177,1] = "sheared";
            irregular_verbs[177,2] = "sheared,shorn";

            irregular_verbs[178,0] = "shed";
            irregular_verbs[178,1] = "shed";
            irregular_verbs[178,2] = "shed";

            irregular_verbs[179,0] = "shine";
            irregular_verbs[179,1] = "shone";
            irregular_verbs[179,2] = "shone";

            irregular_verbs[180,0] = "shit";
            irregular_verbs[180,1] = "shit,shat";
            irregular_verbs[180,2] = "shit,shat";

            irregular_verbs[181,0] = "shoe";
            irregular_verbs[181,1] = "shoed,shod";
            irregular_verbs[181,2] = "shoed,shod";

            irregular_verbs[182,0] = "shoot";
            irregular_verbs[182,1] = "shot";
            irregular_verbs[182,2] = "shot";

            irregular_verbs[183,0] = "show";
            irregular_verbs[183,1] = "showed";
            irregular_verbs[183,2] = "showed,shown";

            irregular_verbs[184,0] = "shrink";
            irregular_verbs[184,1] = "shrank";
            irregular_verbs[184,2] = "shrunk";

            irregular_verbs[185,0] = "shut";
            irregular_verbs[185,1] = "shut";
            irregular_verbs[185,2] = "shut";

            irregular_verbs[186,0] = "sing";
            irregular_verbs[186,1] = "sang";
            irregular_verbs[186,2] = "sung";

            irregular_verbs[187,0] = "sink";
            irregular_verbs[187,1] = "sank";
            irregular_verbs[187,2] = "sunk";

            irregular_verbs[188,0] = "sit";
            irregular_verbs[188,1] = "sat";
            irregular_verbs[188,2] = "sat";

            irregular_verbs[189,0] = "shall";
            irregular_verbs[189,1] = "should";
            irregular_verbs[189,2] = "should";

            irregular_verbs[190,0] = "slay";
            irregular_verbs[190,1] = "slew";
            irregular_verbs[190,2] = "slain";

            irregular_verbs[191,0] = "sleep";
            irregular_verbs[191,1] = "slept";
            irregular_verbs[191,2] = "slept";

            irregular_verbs[192,0] = "slide";
            irregular_verbs[192,1] = "slid";
            irregular_verbs[192,2] = "slid";

            irregular_verbs[193,0] = "sling";
            irregular_verbs[193,1] = "slung";
            irregular_verbs[193,2] = "slung";

            irregular_verbs[194,0] = "slink";
            irregular_verbs[194,1] = "slunk";
            irregular_verbs[194,2] = "slunk";

            irregular_verbs[195,0] = "slit";
            irregular_verbs[195,1] = "slit";
            irregular_verbs[195,2] = "slit";

            irregular_verbs[196,0] = "smell";
            irregular_verbs[196,1] = "smelled,smelt";
            irregular_verbs[196,2] = "smelled,smelt";

            irregular_verbs[197,0] = "smite";
            irregular_verbs[197,1] = "smote";
            irregular_verbs[197,2] = "smitten";

            irregular_verbs[198,0] = "sneak";
            irregular_verbs[198,1] = "sneaked,snuck";
            irregular_verbs[198,2] = "sneaked,snuck";

            irregular_verbs[199,0] = "sow";
            irregular_verbs[199,1] = "sowed";
            irregular_verbs[199,2] = "sowed,sown";

            irregular_verbs[200,0] = "speak";
            irregular_verbs[200,1] = "spoke";
            irregular_verbs[200,2] = "spoken";

            irregular_verbs[201,0] = "speed";
            irregular_verbs[201,1] = "sped,speeded";
            irregular_verbs[201,2] = "sped,speeded";

            irregular_verbs[202,0] = "spell";
            irregular_verbs[202,1] = "spelled/spelt";
            irregular_verbs[202,2] = "spelled/spelt";

            irregular_verbs[203,0] = "spend";
            irregular_verbs[203,1] = "spent";
            irregular_verbs[203,2] = "spent";

            irregular_verbs[204,0] = "spill";
            irregular_verbs[204,1] = "spilled,spilt";
            irregular_verbs[204,2] = "spilled,spilt";

            irregular_verbs[205,0] = "spin";
            irregular_verbs[205,1] = "spun";
            irregular_verbs[205,2] = "spun";

            irregular_verbs[206,0] = "spit";
            irregular_verbs[206,1] = "spit,spat";
            irregular_verbs[206,2] = "spit";

            irregular_verbs[207,0] = "split";
            irregular_verbs[207,1] = "split";
            irregular_verbs[207,2] = "split";

            irregular_verbs[208,0] = "spoil";
            irregular_verbs[208,1] = "spoiled,spoilt";
            irregular_verbs[208,2] = "spoiled,spoilt";

            irregular_verbs[209,0] = "spoon-feed";
            irregular_verbs[209,1] = "spoon-fed";
            irregular_verbs[209,2] = "spoon-fed";

            irregular_verbs[210,0] = "spread";
            irregular_verbs[210,1] = "spread";
            irregular_verbs[210,2] = "spread";

            irregular_verbs[211,0] = "spring";
            irregular_verbs[211,1] = "sprang";
            irregular_verbs[211,2] = "sprung";

            irregular_verbs[212,0] = "stand";
            irregular_verbs[212,1] = "stood";
            irregular_verbs[212,2] = "stood";

            irregular_verbs[213,0] = "steal";
            irregular_verbs[213,1] = "stole";
            irregular_verbs[213,2] = "stolen";

            irregular_verbs[214,0] = "stick";
            irregular_verbs[214,1] = "stuck";
            irregular_verbs[214,2] = "stuck";

            irregular_verbs[215,0] = "sting";
            irregular_verbs[215,1] = "stung";
            irregular_verbs[215,2] = "stung";

            irregular_verbs[216,0] = "stink";
            irregular_verbs[216,1] = "stank";
            irregular_verbs[216,2] = "stunk";

            irregular_verbs[217,0] = "strew";
            irregular_verbs[217,1] = "strewed";
            irregular_verbs[217,2] = "strewn,strewed";

            irregular_verbs[218,0] = "stride";
            irregular_verbs[218,1] = "strode";
            irregular_verbs[218,2] = "stridden";

            irregular_verbs[219,0] = "strike";
            irregular_verbs[219,1] = "struck";
            irregular_verbs[219,2] = "struck,stricken";

            irregular_verbs[220,0] = "string";
            irregular_verbs[220,1] = "strung";
            irregular_verbs[220,2] = "strung";

            irregular_verbs[221,0] = "strive";
            irregular_verbs[221,1] = "strove,strived";
            irregular_verbs[221,2] = "striven,strived";

            irregular_verbs[222,0] = "swear";
            irregular_verbs[222,1] = "swore";
            irregular_verbs[222,2] = "sworn";

            irregular_verbs[223,0] = "sweep";
            irregular_verbs[223,1] = "swept";
            irregular_verbs[223,2] = "swept";

            irregular_verbs[224,0] = "swell";
            irregular_verbs[224,1] = "swelled";
            irregular_verbs[224,2] = "swelled,swollen";

            irregular_verbs[225,0] = "swim";
            irregular_verbs[225,1] = "swam";
            irregular_verbs[225,2] = "swum";

            irregular_verbs[226,0] = "swing";
            irregular_verbs[226,1] = "swung";
            irregular_verbs[226,2] = "swung";

            irregular_verbs[227,0] = "take";
            irregular_verbs[227,1] = "took";
            irregular_verbs[227,2] = "taken";

            irregular_verbs[228,0] = "teach";
            irregular_verbs[228,1] = "taught";
            irregular_verbs[228,2] = "taught";

            irregular_verbs[229,0] = "tear";
            irregular_verbs[229,1] = "tore";
            irregular_verbs[229,2] = "torn";

            irregular_verbs[230,0] = "tell";
            irregular_verbs[230,1] = "told";
            irregular_verbs[230,2] = "told";

            irregular_verbs[231,0] = "think";
            irregular_verbs[231,1] = "thought";
            irregular_verbs[231,2] = "thought";

            irregular_verbs[232,0] = "thrive";
            irregular_verbs[232,1] = "thrived,throve";
            irregular_verbs[232,2] = "thrived,throve";

            irregular_verbs[233,0] = "throw";
            irregular_verbs[233,1] = "threw";
            irregular_verbs[233,2] = "thrown";

            irregular_verbs[234,0] = "thrust";
            irregular_verbs[234,1] = "thrust";
            irregular_verbs[234,2] = "thrust";

            irregular_verbs[235,0] = "tread";
            irregular_verbs[235,1] = "trod,treaded";
            irregular_verbs[235,2] = "trodden,trod,treaded";

            irregular_verbs[236,0] = "unbind";
            irregular_verbs[236,1] = "unbound";
            irregular_verbs[236,2] = "unbound";

            irregular_verbs[237,0] = "underlie";
            irregular_verbs[237,1] = "underlay";
            irregular_verbs[237,2] = "underlain";

            irregular_verbs[238,0] = "understand";
            irregular_verbs[238,1] = "understood";
            irregular_verbs[238,2] = "understood";

            irregular_verbs[239,0] = "undertake";
            irregular_verbs[239,1] = "undertook";
            irregular_verbs[239,2] = "undertaken";

            irregular_verbs[240,0] = "underwrite";
            irregular_verbs[240,1] = "underwrote";
            irregular_verbs[240,2] = "underwritten";

            irregular_verbs[241,0] = "undo";
            irregular_verbs[241,1] = "undid";
            irregular_verbs[241,2] = "undone";

            irregular_verbs[242,0] = "unwind";
            irregular_verbs[242,1] = "unwound";
            irregular_verbs[242,2] = "unwound";

            irregular_verbs[243,0] = "uphold";
            irregular_verbs[243,1] = "upheld";
            irregular_verbs[243,2] = "upheld";

            irregular_verbs[244,0] = "upset";
            irregular_verbs[244,1] = "upset";
            irregular_verbs[244,2] = "upset";

            irregular_verbs[245,0] = "wake";
            irregular_verbs[245,1] = "woke";
            irregular_verbs[245,2] = "woken";

            irregular_verbs[246,0] = "waylay";
            irregular_verbs[246,1] = "waylaid";
            irregular_verbs[246,2] = "waylaid";

            irregular_verbs[247,0] = "wear";
            irregular_verbs[247,1] = "wore";
            irregular_verbs[247,2] = "worn";

            irregular_verbs[248,0] = "weave";
            irregular_verbs[248,1] = "wove";
            irregular_verbs[248,2] = "woven";

            irregular_verbs[249,0] = "wed";
            irregular_verbs[249,1] = "wed,wedded";
            irregular_verbs[249,2] = "wed,wedded";

            irregular_verbs[250,0] = "weep";
            irregular_verbs[250,1] = "wept";
            irregular_verbs[250,2] = "wept";

            irregular_verbs[251,0] = "wet";
            irregular_verbs[251,1] = "wet,wetted";
            irregular_verbs[251,2] = "wet,wetted";

            irregular_verbs[252,0] = "win";
            irregular_verbs[252,1] = "won";
            irregular_verbs[252,2] = "won";

            irregular_verbs[253,0] = "wind";
            irregular_verbs[253,1] = "wound";
            irregular_verbs[253,2] = "wound";

            irregular_verbs[254,0] = "withdraw";
            irregular_verbs[254,1] = "withdrew";
            irregular_verbs[254,2] = "withdrawn";

            irregular_verbs[255,0] = "withhold";
            irregular_verbs[255,1] = "withheld";
            irregular_verbs[255,2] = "withheld";

            irregular_verbs[256,0] = "withstand";
            irregular_verbs[256,1] = "withstood";
            irregular_verbs[256,2] = "withstood";

            irregular_verbs[257,0] = "work";
            irregular_verbs[257,1] = "worked,wrought";
            irregular_verbs[257,2] = "worked,wrought";

            irregular_verbs[258,0] = "wring";
            irregular_verbs[258,1] = "wrung";
            irregular_verbs[258,2] = "wrung";

            irregular_verbs[259,0] = "write";
            irregular_verbs[259,1] = "wrote";
            irregular_verbs[259,2] = "written";
        }
        
        #endregion
        
        #region "verb/noun groups"

        private String[] common_words;    //common words to avoid
        private int no_of_common_words;
        
        private void addCommonWord(String str)
        {
            if (no_of_common_words < common_words.Length)
            {
                str = str.ToLower();
                common_words[no_of_common_words] = str;
                no_of_common_words++;
            }
        }
        
        private void createCommonWords()
        {
            common_words = new String[500];
            addCommonWord("0");
            addCommonWord("1");
            addCommonWord("2");
            addCommonWord("3");
            addCommonWord("4");
            addCommonWord("5");
            addCommonWord("6");
            addCommonWord("7");
            addCommonWord("8");
            addCommonWord("9");
            addCommonWord("10");
            addCommonWord("a");
            addCommonWord("an");
            addCommonWord("am");
            addCommonWord("at");
            addCommonWord("to");
            addCommonWord("as");
            addCommonWord("we");
            addCommonWord("i");
            addCommonWord("in");
            addCommonWord("is");
            addCommonWord("it");
            addCommonWord("if");
            addCommonWord("be");
            addCommonWord("by");
            addCommonWord("so");
            addCommonWord("no"); 
            addCommonWord("last");
            addCommonWord("first");
            addCommonWord("on");
            addCommonWord("of");
            addCommonWord("its");
            addCommonWord("all");
            addCommonWord("can");
            addCommonWord("into");
            addCommonWord("from");
            addCommonWord("just");
            addCommonWord("and");
            addCommonWord("the");
            addCommonWord("over");
            addCommonWord("under");
            addCommonWord("for");
            addCommonWord("then");
            addCommonWord("dont");
            addCommonWord("has");
            addCommonWord("get");
            addCommonWord("got");
            addCommonWord("had");
            addCommonWord("should");            
            addCommonWord("hadnt");
            addCommonWord("have");
            addCommonWord("some");
            addCommonWord("come");
            addCommonWord("this");
            addCommonWord("call");
            addCommonWord("that");
            addCommonWord("thats");
            addCommonWord("find");
            addCommonWord("these");
            addCommonWord("them");
            addCommonWord("look");
            addCommonWord("looked");
            addCommonWord("looks");
            addCommonWord("with");
            addCommonWord("but");
            addCommonWord("about");
            addCommonWord("where");
            addCommonWord("possible");
            addCommonWord("possible for");
            addCommonWord("possible to");
            addCommonWord("sometimes");
            addCommonWord("which");
            addCommonWord("they");
            addCommonWord("just");
            addCommonWord("we");
            addCommonWord("while");
            addCommonWord("whilst");
            addCommonWord("their");
            addCommonWord("perhaps");
            addCommonWord("can be");
            addCommonWord("you");
            addCommonWord("make");
            addCommonWord("any");
            addCommonWord("say");
            addCommonWord("been");
            addCommonWord("like");
            addCommonWord("form");
            addCommonWord("our");
            addCommonWord("give");
            addCommonWord("in the");
            addCommonWord("in a");
            addCommonWord("will");
            addCommonWord("object");
            addCommonWord("shall");
            addCommonWord("will not");
            addCommonWord("until");
            addCommonWord("take");
            addCommonWord("other");
            addCommonWord("now");
            addCommonWord("lead");
            addCommonWord("taken");
            addCommonWord("you can");
            addCommonWord("have to");
            addCommonWord("have some");
            addCommonWord("would");
            addCommonWord("said");
            addCommonWord("one");
            addCommonWord("how");
            addCommonWord("new");
            addCommonWord("not");
            addCommonWord("we are");
            addCommonWord("said");
            addCommonWord("it is");
            addCommonWord("was");
            addCommonWord("are");
            addCommonWord("every");
            addCommonWord("such");
            addCommonWord("more");
            addCommonWord("different");
            addCommonWord("example");
            addCommonWord("way");
            addCommonWord("only");
            addCommonWord("often");
            addCommonWord("show");
            addCommonWord("group");
            addCommonWord("itself");
            addCommonWord("part");
            addCommonWord("saw");
            addCommonWord("making");
            addCommonWord("could");
            addCommonWord("need");
            addCommonWord("out");
            addCommonWord("being");
            addCommonWord("been");
            addCommonWord("yet");
            addCommonWord("lack");
            addCommonWord("even");
            addCommonWord("are not");
            addCommonWord("own");
            addCommonWord("much");
            addCommonWord("of this");
            addCommonWord("become");
            addCommonWord("keep");
            addCommonWord("keeps");
            addCommonWord("do");
            addCommonWord("having");
            addCommonWord("normal");
            addCommonWord("this is");
            addCommonWord("after");
            addCommonWord("before");
            addCommonWord("during");
            addCommonWord("off");
            addCommonWord("use");
            addCommonWord("same");
            addCommonWord("different");
            addCommonWord("difference");
            addCommonWord("case");
            addCommonWord("there");
            addCommonWord("there is");
            addCommonWord("which is");
            addCommonWord("through");
            addCommonWord("end");
            addCommonWord("may");
            addCommonWord("made");
            addCommonWord("name");
            addCommonWord("most");
            addCommonWord("many");
            addCommonWord("well");
            addCommonWord("who");
            addCommonWord("who is");
            addCommonWord("your");
            addCommonWord("you are");
            addCommonWord("you not");
            addCommonWord("owner");
            addCommonWord("around");
            addCommonWord("about");
            addCommonWord("of it");
            addCommonWord("is necessary");
            addCommonWord("necessary");
            addCommonWord("necessary for");
            addCommonWord("process");
            addCommonWord("too");
            addCommonWord("my");
            addCommonWord("why");
            addCommonWord("tell");
            addCommonWord("he");
            addCommonWord("she");
            addCommonWord("what");
            addCommonWord("left");
            addCommonWord("him");
            addCommonWord("her");
            addCommonWord("his");
            addCommonWord("ever");
            addCommonWord("when");
            addCommonWord("because");
            addCommonWord("there are");
            addCommonWord("though");
            addCommonWord("we've");
        }
        

        /// <summary>
        /// returns true is the given word is common
        /// </summary>
        private bool isCommonWord(String word)
        {
            int i;
            bool found = false;

            if (word != "")
            {
                if (common_words == null) createCommonWords();
            
                i = 0;
                while ((i < no_of_common_words) && (!found))
                {
                    if (common_words[i] == word) found = true;
                    i++;
                }
            }
            else found = true;

            return (found);
        }

        
        private String GetSentenceSubject(String[] words, int words_before_verb)
        {
            String subject = "";
            
            if (words.Length > words_before_verb)
            {
            if (words_before_verb < 5)
            {
                if (((words[0] == "the") ||
                    (words[0] == "a") ||
                    (words[0] == "to") ||
                    (words[0] == "my") ||
                    (words[0] == "when") ||
                    (words[0] == "your") ||
                    (words[0] == "some") ||
                    (words[0] == "only") ||
                    (words[0] == "many") ||
                    (words[0] == "all") ||
                    (words[0] == "every") ||
                    (words[0] == "on"))
                    ||
                    ((words[words_before_verb-1] == "is") ||
                     (words[words_before_verb-1] == "was") ||
                     (words[words_before_verb-1] == "were") ||
                     (words[words_before_verb-1] == "be") ||
                     (words[words_before_verb-1] == "are") ||
                     (words[words_before_verb-1] == "will")
                     )
                    ||
                    (words_before_verb == 1)
                    )
                {
                    for (int i = 0; i < words_before_verb; i++)
                    {
                        if ((!isCommonWord(words[i])) || (words_before_verb == 1))
                        {
                            if (subject != "") subject += " ";
                            subject += words[i];
                        }
                    }
                    if (subject.ToUpper() == "I") subject = "me";
                    if (subject != "") subject = "subject(" + subject + ")";
                }
            }
            }
            return(subject);
        }
        
        private String MakeVerbose(String sentence)
        {
            String new_sentence = "";
            String[] words = sentence.Split(' '); 
            ArrayList new_words = new ArrayList();
            for (int i = 0; i < words.Length; i++)
            {
                bool updated = false;
                
                if (((words[i].ToLower() == "shan't") ||
                     (words[i].ToLower() == "Shan't")) && (!updated))
                {
                    new_words.Add("shall");
                    new_words.Add("not");
                    updated = true;
                }                
                if (((words[i].ToLower() == "won't") ||
                     (words[i].ToLower() == "Won't")) && (!updated))
                {
                    new_words.Add("will");
                    new_words.Add("not");
                    updated = true;
                }                
                if (((words[i].ToLower() == "can't") || 
                     (words[i].ToLower() == "Can't")) && (!updated))
                {
                    new_words.Add("can");
                    new_words.Add("not");
                    updated = true;
                }                
                if ((words[i].EndsWith("'ve")) && (!updated))
                {
                    new_words.Add(words[i].Substring(0,words[i].Length-3));
                    new_words.Add("have");
                    updated = true;
                }
                if ((words[i].EndsWith("'ll")) && (!updated))
                {
                    new_words.Add(words[i].Substring(0,words[i].Length-3));
                    new_words.Add("will");
                    updated = true;
                }
                if ((words[i].EndsWith("'s")) && (!updated))
                {
                    new_words.Add(words[i].Substring(0,words[i].Length-2));
                    new_words.Add("is");
                    updated = true;
                }
                if ((words[i].EndsWith("n't")) && (!updated))
                {
                    new_words.Add(words[i].Substring(0,words[i].Length-2));
                    new_words.Add("not");
                    updated = true;
                }
                if (!updated) new_words.Add(words[i]);
            }
            for (int i = 0; i < new_words.Count; i++)
            {
                if (i > 0) new_sentence += " ";
                new_sentence += (String)new_words[i];
            }
            return(new_sentence);
        }
        
        /// <summary>
        /// returns a list of verb/noun groups for the given sentence
        /// </summary>
        public ArrayList GetVerbNouns(String sentence, ref float past, ref float future)
        {
            String verbose_sentence = MakeVerbose(sentence);
            
            ArrayList verbnouns = new ArrayList();
            String[] words = verbose_sentence.Split(' ');
            String str;
            int i, j, k, index;
            bool[] is_a_verb = new bool[words.Length];
            bool[] is_common = new bool[words.Length];
            bool beginning;
            bool causalImplication = false;

            for (i = 0; i < words.Length; i++)
            {
                is_a_verb[i] = false;
                is_common[i] = false;

                index = isIrregularVerb(words[i]);
                if (index > -1)
                {
                    is_a_verb[i] = true;
                    if (irregular_verbs[index, 1] == words[i])
                    {
                        past++;
                        future = 0;
                    }
                    words[i] = irregular_verbs[index, 0];
                }
                else
                {
                    if ((words[i] == "want") ||
                        (words[i] == "wants") ||
                        (words[i] == "need") ||
                        (words[i] == "needs")
                        )
                    {
                        is_a_verb[i] = true;
                    }
                
                    if (words[i].Length > 5)
                        if ((words[i].Substring(words[i].Length - 3) == "ing") && (words[i].IndexOf("thing") == -1) && (words[i] != "morning") && (words[i] != "evening"))
                        {
                            is_a_verb[i] = true;
                            if (past == 0) future++;

                            if ((words[i].Substring(words[i].Length - 4, 1) != "s") && (words[i].Substring(words[i].Length - 4, 1) != "t") && (words[i].Substring(words[i].Length - 4, 1) != "z") &&
                                (words[i].Substring(words[i].Length - 5, 2) != "ar") && (words[i].Substring(words[i].Length - 4, 1) != "c") && (words[i].Substring(words[i].Length - 4, 1) != "l") && (words[i].Substring(words[i].Length - 4, 1) != "v") && (words[i].Substring(words[i].Length - 4, 1) != "g") &&
                                (words[i].Substring(words[i].Length - 4, 1) != "m") && (words[i].Substring(words[i].Length - 4, 1) != "n") && (words[i].Substring(words[i].Length - 4, 1) != "d") && (words[i].Substring(words[i].Length - 4, 1) != "k") && (words[i].Substring(words[i].Length - 4, 1) != "p"))
                                words[i] = words[i].Substring(0, words[i].Length - 3);
                            else
                            {
                                if ((words[i].Substring(words[i].Length - 4, 1)) != (words[i].Substring(words[i].Length - 5, 1)))
                                {
                                    words[i] = words[i].Substring(0, words[i].Length - 3);
                                    if ((words[i].Substring(words[i].Length - 2, 2) != "nk") && (words[i].Substring(words[i].Length - 2, 2) != "rk") && (words[i].Substring(words[i].Length - 2, 2) != "nd") && (words[i].Substring(words[i].Length - 2, 2) != "rd") && (words[i].Substring(words[i].Length - 2, 2) != "lm") && (words[i].Substring(words[i].Length - 2, 2) != "rm") &&
                                        (words[i].Substring(words[i].Length - 3, 3) != "ack") && (words[i].Substring(words[i].Length - 3, 3) != "ait") && (words[i].Substring(words[i].Length - 2, 2) != "el") && (words[i].Substring(words[i].Length - 2, 2) != "lp") &&
                                        ((words[i].Substring(words[i].Length - 2, 2) != "at") || (words[i].Substring(words[i].Length - 3, 3) == "tat") || (words[i].Substring(words[i].Length - 3, 3) == "eat")) && 
                                        (words[i].Substring(words[i].Length - 2, 2) != "et") && (words[i].Substring(words[i].Length - 2, 2) != "ct") && (words[i].Substring(words[i].Length - 2, 2) != "nt") && (words[i].Substring(words[i].Length - 2, 2) != "st") && (words[i].Substring(words[i].Length - 2, 2) != "ht") &&
                                        ((words[i].Substring(words[i].Length - 1, 1) != "n") || (words[i].Substring(words[i].Length - 2, 2) == "in"))
                                        ) //suggesting, containing
                                    {
                                        if ((words[i].Substring(words[i].Length - 1, 1) != "k") ||
                                            ((words[i].Substring(words[i].Length - 1, 1) == "k") &&
                                             (words[i].Substring(words[i].Length - 2, 1) != "s")) &&
                                             ((words[i].Substring(words[i].Length - 2, 1) != (words[i].Substring(words[i].Length - 3, 1)))
                                             )
                                            )
                                        {
                                            if ((words[i].Substring(words[i].Length - 3, 3) != "eed") &&
                                                (words[i].Substring(words[i].Length - 3, 3) != "eep") &&
                                                (words[i].Substring(words[i].Length - 3, 3) != "oop") &&
                                                (!words[i].StartsWith("train")) &&
                                                (!words[i].StartsWith("eat"))
                                                )
                                                words[i] += "e";
                                        }
                                    }
                                }
                                else
                                {
                                    if (((!((words[i].Substring(words[i].Length - 4, 1)) == "t") && ((words[i].Substring(words[i].Length - 5, 1) == "t"))) &&
                                        (!((words[i].Substring(words[i].Length - 4, 1)) == "n") && ((words[i].Substring(words[i].Length - 5, 1) == "n"))) &&
                                        (!((words[i].Substring(words[i].Length - 4, 1)) == "p") && ((words[i].Substring(words[i].Length - 5, 1) == "p")))    //popping                                     
                                        )
                                        || ((words[i].Substring(words[i].Length - 4, 1)) == "l") //pulling
                                        || ((words[i].Substring(words[i].Length - 4, 1)) == "s") //passing
                                        ) 
                                        words[i] = words[i].Substring(0, words[i].Length - 3);
                                    else
                                        words[i] = words[i].Substring(0, words[i].Length - 4);
                                }
                            }
                        }

                    if (words[i].Length > 3)
                        if (words[i].Substring(words[i].Length - 2) == "ed")
                        {
                            if (words[i].Substring(words[i].Length - 3) != "eed")
                            {
                                is_a_verb[i] = true;
                                past++;
                                if ((words[i].Substring(words[i].Length - 3, 1) != "g") && (words[i].Substring(words[i].Length - 3, 1) != "c") && (words[i].Substring(words[i].Length - 4, 2) != "ok") &&
                                    ((words[i].Substring(words[i].Length - 4, 2) != "st") || (words[i].Substring(words[i].Length - 5, 3) == "ist") || (words[i].Substring(words[i].Length - 5, 3) == "ust")) &&
                                    (words[i].Substring(words[i].Length - 4, 2) != "os") && (words[i].Substring(words[i].Length - 4, 2) != "ud") && (words[i].Substring(words[i].Length - 4, 2) != "bl") && (words[i].Substring(words[i].Length - 4, 2) != "dl") && (words[i].Substring(words[i].Length - 4, 2) != "gl") && (words[i].Substring(words[i].Length - 4, 2) != "id") &&
                                    ((words[i].Substring(words[i].Length - 3, 1) != "r") || (words[i].Substring(words[i].Length - 4, 2) == "ar") || (words[i].Substring(words[i].Length - 4, 2) == "er") || (words[i].Substring(words[i].Length - 4, 2) == "or")) &&
                                    ((words[i].Substring(words[i].Length - 3, 1) != "n") || (words[i].Substring(words[i].Length - 4, 2) == "on") || (words[i].Substring(words[i].Length - 4, 2) == "gn") || (words[i].Substring(words[i].Length - 4, 2) == "rn") || (words[i].Substring(words[i].Length - 4, 2) == "in") || (words[i].Substring(words[i].Length - 4, 2) == "en")) &&
                                    (words[i].Substring(words[i].Length - 3, 1) != "b") && (words[i].Substring(words[i].Length - 3, 1) != "z") && (words[i].Substring(words[i].Length - 3, 1) != "v") && (words[i].Substring(words[i].Length - 4, 2) != "ut") && (words[i].Substring(words[i].Length - 4, 2) != "at") 
                                    //(!((words[i].Substring(words[i].Length - 3, 1) != "s") && (words[i].Substring(words[i].Length - 4, 1) != "u")))
                                    )
                                {
                                    if (words[i] == "died")
                                        words[i] = "die";
                                    else
                                    {
                                        if (words[i] == "based")
                                            words[i] = "base";
                                        else
                                        {
                                            if (words[i] == "named")
                                                words[i] = "name";
                                            else
                                            {
                                                if (words[i] == "collapsed")
                                                    words[i] = "collapse";
                                                else
                                                {
	                                                if ((words[i].Substring(words[i].Length - 4, 4) != "tled") &&
	                                                    (words[i].Substring(words[i].Length - 4, 4) != "used") &&
	                                                    (words[i].Substring(words[i].Length - 4, 4) != "ised")                                                    
	                                                    )
	                                                {
	                                                    words[i] = words[i].Substring(0, words[i].Length - 2);

	                                                    if ((words[i].Substring(words[i].Length - 1, 1)) == (words[i].Substring(words[i].Length - 2, 1)))
	                                                    {
	                                                        if ((words[i].Substring(words[i].Length - 1, 1) != "l") &&
	                                                           (words[i].Substring(words[i].Length - 1, 1) != "s")) //killed, impressed
	                                                            words[i] = words[i].Substring(0, words[i].Length - 1);
	                                                    }

	                                                    if ((words[i].Substring(words[i].Length - 2, 2) == "in") && (words[i].Substring(words[i].Length - 3, 1) != "a")) //destined
	                                                        words[i] += "y";

	                                                    if (words[i].Substring(words[i].Length - 1, 1) == "i")
	                                                    {
	                                                        //accompanied - accompany
	                                                        words[i] = words[i].Substring(0, words[i].Length - 1) + "y";
	                                                    }
	                                                }
	                                                else
	                                                {
	                                                    //eg. rattled, caused
	                                                    words[i] = words[i].Substring(0, words[i].Length - 1);

	                                                    if (words[i].Length > 3)
	                                                    {
	                                                         if (words[i].Substring(words[i].Length - 4, 4) == "cuse")  //focused
	                                                             words[i] = words[i].Substring(0, words[i].Length - 1);
	                                                    }

	                                                }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if ((words[i].Substring(words[i].Length - 3, 1)) == (words[i].Substring(words[i].Length - 4, 1)))
                                        words[i] = words[i].Substring(0, words[i].Length - 2);

                                    //eg. sliced, encouraged, optimised, decribed, convoluted
                                    words[i] = words[i].Substring(0, words[i].Length - 1);
                                }
                            }
                        }

                    //are the last two letters the same?
                    if (words[i].Length > 1)
                    {
                        if ((words[i].Substring(words[i].Length - 1, 1)) == words[i].Substring(words[i].Length - 2, 2))
                            words[i] = words[i].Substring(0, words[i].Length - 1);
                    }
                }

                if ((is_a_verb[i]) && (i > 0))
                {
                    if ((words[i - 1] == "the") || (words[i - 1] == "a") || (words[i - 1] == "some") || (words[i - 1] == "an") || (words[i - 1] == "to"))
                        is_a_verb[i] = false;
                }


                if (isCommonWord(words[i])) is_common[i] = true;
            }
            
            bool subject_added = false;
            
            for (i = 0; i < words.Length; i++)
            {
                if (words[i] == "because") causalImplication = true;
            
                if (is_a_verb[i])
                {                
                    Console.WriteLine(words[i]);
                
                    // find the subject of the sentence
                    if (!subject_added)
                    {
                        String subject = GetSentenceSubject(words, i);
                        if (subject != "")
                        {
                            verbnouns.Add(subject);
                            subject_added = true;
                        }
                    }
                
                    

                    str = "";                    
                    if (causalImplication)
                        str = "causalimplication(";
                    
                    str += words[i];
                    beginning = true;
                    if (i < words.Length - 1)
                    {
                        //look ahead
                        //skip any common words
                        j = i + 1;
                        if (j < words.Length)
                        {
                            k = j;
                            while (k < words.Length)
                            {
                                if (is_a_verb[k] == false)
                                {
                                    if (is_common[k] == false)
                                    {
                                        if (beginning)
                                            str += " (";
                                        else
                                            str += ",";
                                        beginning = false;
                                        str += words[k];
                                    }
                                }
                                else k = words.Length;
                                k++;
                            }
                        }
                    }
                    else
                    {
                        if (i - 1 >= 0)
                        {
                            if ((is_common[i - 1] == false) && (is_a_verb[i - 1] == false))
                            {
                                if (beginning)
                                    str += " (";
                                else
                                    str += ",";
                                beginning = false;
                                str += words[i-1];
                            }
                        }
                    }

                    //if (beginning == false)
                    {
                        if (causalImplication)
                        {
                            str += ")";
                            causalImplication = false;
                        }
                        str += ")";
                        verbnouns.Add(str);
                    }
                }
            }
            return(verbnouns);
        }
        
        #endregion
	}
}

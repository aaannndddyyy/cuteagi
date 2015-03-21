/*
    personality modeling framework
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

namespace cuteagi.personalitymodel
{
    /// <summary>
    /// 
    /// </summary>
    public class informationGoal
    {
        // the type of information which we wish to extract
        public String information_type;
        
        // results
        public ArrayList results = new ArrayList();
        

        public informationGoal()
        {
        }
        
	    /// <summary>
	    /// returns a section of text which follows one of the search strings
	    /// </summary>
		public void ParsePostfix(String text, String[] search_str)
		{
		    results.Clear();
		    if (!DontKnow(text))
		    {		        
		        String s = getPostfix(text, search_str);
		    
		        if (s != "") 
		            results.Add(s);
		        else
		            results.Add(text);
		    }
		}
        
        
	    /// <summary>
	    /// returns a section of text which follows one of the search strings
	    /// </summary>
	    private String getPostfix(String text,
	                             String[] search_str)
	    {
	        String result = "";
	        int idx = -1;
	        
	        text = " " + text + " ";
	        
	        int i = 0;
	        while ((i < search_str.Length) && (idx == -1))
	        {
	            String searchstr = " " + search_str[i] + " ";
	            idx = text.IndexOf(searchstr);
	            if (idx > -1)
	            {
	                idx += searchstr.Length;
	                result = text.Substring(idx, text.Length - idx);
	            }
	            i++;
	        }
	        return(result);
	    }

	    /// <summary>
	    /// returns a section of text which precedes one of the search strings
	    /// </summary>
	    private String getPrefix(String text,
	                             String[] search_str)
	    {
	        String result = "";
	        int idx = -1;
	        
	        text = " " + text + " ";
	        
	        int i = 0;
	        while ((i < search_str.Length) && (idx == -1))
	        {
	            String searchstr = " " + search_str[i] + " ";
	            idx = text.IndexOf(searchstr);
	            if (idx > -1)
	            {
	                result = text.Substring(idx);
	            }
	            i++;
	        }
	        return(result);
	    }

        
	    /// <summary>
	    /// returns true if the given text contains one of the given search strngs
	    /// </summary>
	    private bool Contains(String text,
	                         String[] search_str)
	    {
	        bool result = false;
	        int idx = -1;
	        
	        text = " " + text + " ";
	        
	        int i = 0;
	        while ((i < search_str.Length) && (idx == -1))
	        {
	            String searchstr = " " + search_str[i] + " ";
	            idx = text.IndexOf(searchstr);
	            if (idx > -1) result = true;
	            i++;
	        }
	        return(result);
	    }
	    
	    private bool DontKnow(String text)
	    {
	        String[] search_str = {"don't know", "dont know", "no idea", 
	                               "unknown", "beats me", "who knows" };
	        
	        if (Contains(text, search_str))
	            return(true);
	        else
	            return(false);
	    }

        
        public virtual void Parse(String text)
        {
        }        
    }
}

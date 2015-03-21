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
using Gtk;
using cuteagi.core;
using cuteagi.nlp;
using cuteagi.unittests;

public partial class MainWindow: Gtk.Window
{	
    nlpVerbs verbs = new nlpVerbs();
    atomCollection ranges = nlpUtilities.CreateRanges();

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

    private void ParseEntry(String text)
    {
        agentTests.Run();
        collectionTests.Run();
    
        ArrayList r = ranges.GetPositionWithinRangeCollection(text);
        for (int i = 0; i < r.Count; i++)
        {
            txtConversation.Buffer.Text += (char)13 + (String)r[i];
        }
    
        String day_name = "";
        int day_number = -1;
        String month = "";
        int year = -1;
        DateTime d = nlpUtilities.ContainsDate(text, ref day_name, ref day_number, ref month, ref year);

        float past=0, future=0;
        ArrayList v = verbs.GetVerbNouns(text, ref past, ref future);

        text = "";
        for (int i = 0; i < v.Count; i++)
            text += (String)v[i] + ", ";
                
	    txtConversation.Buffer.Text += (char)13 + text;

        if (d != DateTime.MinValue)
            txtConversation.Buffer.Text += (char)13 + d.ToString();
    }

	protected virtual void OnCmdEntryClicked (object sender, System.EventArgs e)
	{
	    ParseEntry(txtEntry.Text);
	    txtEntry.Text = "";
	}
}
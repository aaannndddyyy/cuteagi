// /home/motters/develop/personaGUI/Main.cs created with MonoDevelop
// User: motters at 5:47 PM 7/4/2007
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//
// project created on 7/4/2007 at 5:47 PM
using System;
using Gtk;

namespace personaGUI
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}
	}
}
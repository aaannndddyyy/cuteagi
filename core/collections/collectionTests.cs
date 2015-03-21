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
using cuteagi.core;
using sluggish.utilities.timing;
using NUnit.Framework;

namespace cuteagi.unittests
{	
    [TestFixture]
	public class collectionTests
	{
		/// <summary>
		/// runs all unit tests
		/// </summary>
	    public static void Run()
	    {
	        collectionTests tests = new collectionTests();
	        
	        tests.TestAtomCollectionIndexIDs();
	        tests.TestAtomCollectionIndexNames();
	    }
	    
		/// <summary>
		/// test atomCollection index on ID
		/// </summary>
		[Test]
		public void TestAtomCollectionIndexIDs()
		{
		    Random rnd = new Random();
		
		    // make a collection
		    atomCollection collection = new atomCollection();
		    
		    // add some nodes to the collection
		    const int no_of_nodes = 5;
		    for (int i = 0; i < no_of_nodes; i++)
		    {
		        node n = new node();
		        n.atom_ID = rnd.Next(1000);
		        collection.Add(n);
		    }
		    
		    ArrayList index = collection.GetIndex("ID");
		    int j = 1;
		    bool inOrder = true;
		    while ((j < no_of_nodes) && (inOrder))
		    {
		        int idx1 = (int)index[j - 1];
		        node n1 = (node)collection.Get(idx1);
		        int idx2 = (int)index[j];
		        node n2 = (node)collection.Get(idx2);
		        if (n1.atom_ID > n2.atom_ID) inOrder = false;
		        j++;
		    }
		    Assert.IsTrue(inOrder, "atomCollection index on IDs is in order");
		    
		    //collection.ShowIndexes();
		}

		/// <summary>
		/// test atomCollection index on name
		/// </summary>
		[Test]
		public void TestAtomCollectionIndexNames()
		{
		    Random rnd = new Random();
		
		    // make a collection
		    atomCollection collection = new atomCollection();
		    
		    // add some nodes to the collection
		    const int no_of_nodes = 5;
		    for (int i = 0; i < no_of_nodes; i++)
		    {
		        node n = new node();
		        n.SetName(rnd.Next(1000).ToString());
		        collection.Add(n);
		    }
		    
		    ArrayList index = collection.GetIndex("name");
		    int j = 1;
		    bool inOrder = true;
		    while ((j < no_of_nodes) && (inOrder))
		    {
		        int idx1 = (int)index[j - 1];
		        node n1 = (node)collection.Get(idx1);
		        int idx2 = (int)index[j];
		        node n2 = (node)collection.Get(idx2);
		        if (n1.GetName().CompareTo(n2.GetName()) > 0) inOrder = false;
		        j++;
		    }
		    
		    //collection.ShowIndexes();

		    Assert.IsTrue(inOrder, "atomCollection index on names is not in sequential order");
		}

	}
}

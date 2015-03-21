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
	public class agentTests
	{
		/// <summary>
		/// runs all unit tests
		/// </summary>
	    public static void Run()
	    {
	        agentTests tests = new agentTests();
	        
	        tests.TestSearch();
	        tests.TestAssociativeLearning();
	        tests.TestActivationSpreading();
	    }
	
		/// <summary>
		/// unit test for agentSearch
		/// </summary>
		[Test]
		public void TestSearch()
		{
		    // make a collection
		    atomCollection search_collection = new atomCollection();
		    
		    // add some nodes to the collection
		    const int no_of_nodes = 10;
		    for (int i = 0; i < no_of_nodes; i++)
		    {
		        node n = new node();
		        n.SetImportance(1.0f - (i / (float)no_of_nodes), atom.SHORT_TERM_IMPORTANCE);
		        search_collection.Add(n);
		    }
		    
		    // create a search agent
		    agentSearch seeker = new agentSearch(search_collection, "importance",
		                                         atom.SHORT_TERM_IMPORTANCE);
		    
		    // create an exemplar to match against
		    node exemplar = new node();
		    exemplar.SetImportance(0.7f, atom.SHORT_TERM_IMPORTANCE);
		    
		    // link the exemplar to the the search agent
		    bool link_added = seeker.AddIncomingLink("hebbian", exemplar, new truthvalue());
		    Assert.IsTrue(link_added, "Link created");
		    
		    // run the agent until it completes
		    seeker.Run();
		    while (seeker.running) seeker.Run();
		    atomCollection search_results = seeker.GetSearchResults();		    
		    
		    // check that some results were produced
		    Assert.IsNotNull(search_results, "search results were returned");
		    if (search_results != null)
		    {
		        // check that the number of nodes is what we expect
		        Assert.AreEqual(search_results.Count(), no_of_nodes, "expected number of nodes in the search pool");
		        
		        //check that the results are sorted
		        int i = 1;
		        bool in_order = true;
		        
		        while ((i < search_results.Count()) && (in_order))
		        {
		            if (seeker.GetDifference(i) < seeker.GetDifference(i - 1)) in_order = false;
		            i++;
		        }
		        Assert.IsTrue(in_order, "search results are sorted in ascending order of difference from the exemplar");
		    }
		}

		/// <summary>
		/// unit test for agentActivationSpreading
		/// </summary>
		[Test]
		public void TestActivationSpreading()
		{
		    // make a collection
		    atomCollection collection = new atomCollection();
		    
		    // add some nodes to the collection
		    const int no_of_nodes = 4;
		    for (int i = 0; i < no_of_nodes; i++)
		    {
		        node n = new node();
		        collection.Add(n);
		    }
		    
		    // link the nodes together
		    for (int i = 0; i < no_of_nodes; i++)
		    {
		        node n1 = (node)collection.Get(i);
		        for (int j = 0; j < no_of_nodes; j++)
		        {
		            if (i != j)
		            {
		                node n2 = (node)collection.Get(j);
		                n1.AddIncomingLink("hebbian", n2, new truthvalue(0.5f));
		            }
		        }
		    }
		    
		    // set an initial activation on channel 0
		    node activated1 = (node)collection.Get(0);
		    activated1.SetActivation(1.0f, 0);

		    // set an initial activation on channel 1
		    node activated2 = (node)collection.Get(no_of_nodes-1);
		    activated2.SetActivation(1.0f, 1);
		    
		    // create an activation spreading agent
		    agentActivationSpreading spread = new agentActivationSpreading(collection);
		    		    
		    // run the agent until it completes
		    spread.Run();
		    while (spread.running) spread.Run();

            Assert.IsTrue(activated1.GetActivation(0) < 1.0f, "activation level 0 has changed");
            Assert.IsTrue(activated2.GetActivation(1) < 1.0f, "activation level 1 has changed");
            
            node activated3 = (node)collection.Get(1);
            Assert.IsTrue(activated3.GetActivation(0) > 0, "activation level 0 has spread");
            Assert.IsTrue(activated3.GetActivation(1) > 0, "activation level 1 has spread");
		}
		
		/// <summary>
		/// test associative learning
		/// </summary>
		[Test]
		public void TestAssociativeLearning()
		{
		    // make a collection
		    atomCollection collection = new atomCollection();
		    
		    // add some nodes to the collection
		    const int no_of_nodes = 5;
		    for (int i = 0; i < no_of_nodes; i++)
		    {
		        node n = new node();
		        collection.Add(n);
		    }
		    
		    // link the nodes together
		    for (int i = 0; i < no_of_nodes; i++)
		    {
		        node n1 = (node)collection.Get(i);
		        for (int j = 0; j < no_of_nodes; j++)
		        {
		            if (i != j)
		            {
		                node n2 = (node)collection.Get(j);
		                n1.AddIncomingLink("hebbian", n2, new truthvalue());
		            }
		        }
		    }
		    
		    // create a learning agent
		    agentAssociativeLearning learner = new agentAssociativeLearning(collection);
		    
		    // run the agent until it completes
		    learner.Run();
		    while (learner.running) learner.Run();		    
		}
	}
}

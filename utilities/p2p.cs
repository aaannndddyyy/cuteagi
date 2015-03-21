/*
    peer to peer stuff
    Copyright (C) 2000-2007 Bob Mottram
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
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace sluggish.utilities.p2p
{	
    public class p2pTcpListener : TcpListener
    {
        public p2pTcpListener(int port) : base(port)
        {
        }
  
        public void StopMe()
        {
            if ( this.Server != null )
            {
                this.Server.Close();
            }
        }
    }


    public class p2pTransfer
    {
        // port to use for data transfer
        public int port = 8081;
        
        protected p2pTcpListener tcpl;

        public p2pTransfer()
        {
        }
 
        public void Stop()
        {
            if (tcpl != null) tcpl.StopMe();
        }

        /// <summary>
        /// start listening for peer requests and send data
        /// </summary>
        public void Start() 
        {           
            try
            {
                this.tcpl = new p2pTcpListener(port);
            
                //Encoding ASCII = Encoding.ASCII;     
                tcpl.Start();

                while (true)
                {  
                    // Accept will block until someone connects     
                    Socket s = tcpl.AcceptSocket();      
                    NetworkStream DataStream = new NetworkStream(s);

                    String filename;
                    byte[] Buffer = new byte[256];
                    DataStream.Read(Buffer, 0, 256);
                    filename = Encoding.ASCII.GetString(Buffer);
                    StringBuilder sbFileName = new StringBuilder(filename);
                    StringBuilder sbFileName2 = sbFileName.Replace(@"\", @"\\");
                    FileStream fs = new FileStream(sbFileName2.ToString(), FileMode.Open, FileAccess.Read);     
                    BinaryReader reader = new BinaryReader(fs);
                    byte[] bytes = new byte[1024];
                    int read;
                    while((read = reader.Read(bytes, 0, bytes.Length)) != 0) 
                    {
                        DataStream.Write(bytes, 0, read);
                    }
                    reader.Close(); 
                    DataStream.Flush();
                    DataStream.Close();
                }
            }
            catch(SocketException ex)
            {    
                Console.WriteLine("ListenForPeers: " + ex.ToString());
            }
        }

        /// <summary>
        /// download a file
        /// </summary>
        public void DownloadToClient(string server, int port, 
                                     string remotefilename, 
                                     string localfilename)
        {
            try
            {
                TcpClient tcpc = new TcpClient();                
                byte[] read = new byte[1024];                    

                // Try to connect to the server 
                IPHostEntry IPHost = Dns.Resolve(server); 
                string []aliases = IPHost.Aliases; 
                IPAddress[] addr = IPHost.AddressList; 

                IPEndPoint ep = new IPEndPoint(addr[0], port);
                tcpc.Connect(ep);
    
                // Get the stream
                Stream s = tcpc.GetStream();    
                byte[] b = Encoding.ASCII.GetBytes(remotefilename.ToCharArray());
                s.Write(b, 0,  b.Length);
                int bytes;
                FileStream fs = new FileStream(localfilename, FileMode.OpenOrCreate);
                BinaryWriter w = new BinaryWriter(fs);

                // Read the stream and convert it to ASCII   
                while( (bytes = s.Read(read, 0, read.Length)) != 0) 
                {    
                    w.Write(read, 0, bytes);
                    read = new byte[1024];    
                }

                tcpc.Close();
                w.Close();
                fs.Close();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());     
            }
        } 
    }

}

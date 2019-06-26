using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace StreetsAppServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345));
            socket.Listen(100);
            Thread serverThread = new Thread(ServerThreadProcedure);
            serverThread.Start(socket);
        }

        private static void ServerThreadProcedure(object obj)
        {
            Socket serverSocket = obj as Socket;
            Socket clientSocket = serverSocket.Accept();
            while (true)
            {
                byte[] buffer = new byte[1024 * 4];
                int receiveSize = clientSocket.Receive(buffer);
                if (receiveSize == 0)
                {
                    continue;
                }
                XmlDocument document = new XmlDocument();
                document.Load("data.xml");
                XmlElement rootElemet = document.DocumentElement;
                string message = "";
                foreach (XmlNode node in rootElemet)
                {
                    if (node.Name == "street")
                    {
                        if (node.ChildNodes[1].Value == Encoding.UTF8.GetString(buffer))
                        {
                            message += node.ChildNodes[1].Value + "\n";
                        }
                    }
                }
                clientSocket.Send(Encoding.UTF8.GetBytes(message));
            }
        }
    }
}

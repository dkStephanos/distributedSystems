using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SynchronousServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Allocate a buffer to store incoming data
            byte[] bytes = new byte[1024];
            string data;
            bool gameOver = false;
            
            // 2. Establish a loal endpoint for the socket (ip and port)
            IPHostEntry IPHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // 3. Create the socket
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                // 4. Bind the socket to the local endpoint
                listener.Bind(localEndPoint);

                // 5. Listen for incoming connections
                listener.Listen(10);

                // 6. Enter a loop
                while(true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    Console.WriteLine(ipAddress.ToString());
                    Console.WriteLine(localEndPoint.ToString());

                    // 6.1. Listen for a connection (blocking call)
                    Socket handler = listener.Accept();

                    // 6.2. Process the connection to the read the incoming data
                    data = "";
                    while (true)
                    {
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }

                    // 6.3. Process the incoming data
                    Console.WriteLine("Text received: {0}", data);
                    TicTacToeGame currGame = new TicTacToeGame(data[0]);


                    byte[] msg = Encoding.ASCII.GetBytes(currGame.board.drawBoard());
                    handler.Send(msg);

                    while (!gameOver)
                    {
                        data = "";
                        while (true)
                        {
                            int bytesRec = handler.Receive(bytes);
                            data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                            if (data.IndexOf("<EOF>") > -1)
                            {
                                break;
                            }
                        }
                        Console.WriteLine("Text received: {0}", data);
                    }

                    // 6.4. Close connection
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("\nPress ENTER to exit...");
            Console.Read();
        }
    }
}

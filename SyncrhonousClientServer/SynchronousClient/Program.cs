using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SynchronousClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Allocate a buffer to store incoming data
            byte[] bytes = new byte[1024];
            bool gameOver = false;
            char token;

            try
            {
                // 2. Establish a remote endpointfor the socket
                IPHostEntry IPHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = IPHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                // 3. Create the socket
                Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // 4. Connect the socket to the remote endpoint
                try
                {
                    sender.Connect(remoteEP);
                    Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());


                    while(true)
                    {
                        //Get token selection from user
                        Console.WriteLine("Welcome to Server TicTacToe! Select X or O:  ");
                        token = Console.ReadLine()[0];
                        if(token == 'O' || token == 'X')
                        {
                            break;
                        } else
                        {
                            Console.WriteLine("Invalid selection!!!");
                        }
                    }

                    // 5. Encode the data to be sent
                    byte[] msg = Encoding.ASCII.GetBytes(token + "<EOF>");

                    // 6. Send the data through the socket
                    int bytesSent = sender.Send(msg);

                    //while(!gameOver)
                    {
                        // 7. Listen for the response (blocking call)
                        int bytesRec = sender.Receive(bytes);

                        // 8. Process the response
                        Console.WriteLine("Echoed test = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));
                    }

                    // 9. Close the socket
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }  
        }
    }
}

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
            string result;
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

                    // 6.3. Process the incoming data && create a new game with the clients selection (X or O)
                    Console.WriteLine("Text received: {0}", data);
                    TicTacToeGame currGame = new TicTacToeGame(data[0]);

                    // 6.4. Get the starting game board and encode/send it to the client
                    byte[] msg = Encoding.ASCII.GetBytes(currGame.board.drawBoard());
                    handler.Send(msg);

                    // 7. Enter the game loop
                    while (!gameOver)
                    {
                        // 7.1. Read in the next move from the client
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

                        // 7.2. Attempt the client's selected move
                        result = currGame.playTurn((int)Char.GetNumericValue(data[0]) - 1);

                        // 7.3. Process the result of the played turn.
                        if(result == "Not Finsihed")
                        {
                            // If we aren't finished, draw the current board/encode it/send it to the client
                            msg = Encoding.ASCII.GetBytes(currGame.board.drawBoard());
                            handler.Send(msg);
                        }
                        else if(result == "Invalid")
                        {
                            // If the client selected an invalid space, attach a warning to the previous game board and encode/send to client
                            msg = Encoding.ASCII.GetBytes("Invalid selection. Try again." + "\n" + currGame.board.drawBoard());
                            handler.Send(msg);

                        }
                        else
                        {
                            // Else, we have a final result from the game, so encode the result and the final game board and send to client
                            msg = Encoding.ASCII.GetBytes(result + "\n" + currGame.board.drawBoard());
                            handler.Send(msg);
                        }
                    }

                    // 6.5. Close connection
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

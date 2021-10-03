using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var socket = new UdpClient(666);
            IPEndPoint ip = null;
            while (true)
            {
                if (socket.Available <= 0)
                    continue;
                Thread.Sleep(10000);
                var bytes = socket.Receive(ref ip);
                try
                {
                    var str = Encoding.ASCII.GetString(bytes);
                    Console.WriteLine(str);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}

using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class SensorSimulator
{
    static void Main()
    {
        string host = "192.168.25.32";  // TCP 서버가 실행 중인 IP
        int port = 9999;

        Random rand = new Random();

        while (true)
        {
            double temp = Math.Round(rand.NextDouble() * 10 + 20, 2); // 예: 20~30도
            double hum = Math.Round(rand.NextDouble() * 20 + 40, 2);  // 예: 40~60%

            string message = $"{{\"temperature\":{temp},\"humidity\":{hum}}}";
            Console.WriteLine($"[Sensor] Sending: {message}");

            using (TcpClient client = new TcpClient(host, port))
            using (NetworkStream stream = client.GetStream())
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }

            Thread.Sleep(2000); // 2초마다 전송
        }
    }
}

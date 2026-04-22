using Server.Config;

namespace Server
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var dataDir = Path.Combine(AppContext.BaseDirectory, "Data");
            Directory.CreateDirectory(dataDir);

            var cfg = ServerConfig.Load();

            using var host = new ServerHost(cfg, dataDir);
            host.Start();

            Console.WriteLine("[Server] Press Enter to quit...");
            Console.ReadLine();

            host.Stop();
        }
    }
}

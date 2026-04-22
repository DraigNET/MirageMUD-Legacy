using Server.Config;
using Server.Database;
using Server.Game;
using Server.Networking;
using Server.World;

namespace Server
{
    public sealed class ServerHost : IDisposable
    {
        private readonly ServerConfig _config;
        private readonly JsonAccountRepository _accounts;
        private readonly ServerGameLogic _logic;

        private readonly WorldService _world;
        private readonly ServerTcp _tcp;
        private readonly DataHandler _handler;
        private readonly ServerLoop _loop;

        public ServerHost(ServerConfig config, string dataDir)
        {
            _config = config;

            Directory.CreateDirectory(dataDir);

            _accounts = new JsonAccountRepository(dataDir);
            _logic = new ServerGameLogic(_accounts);

            // World (definitions + runtime state)
            _world = new WorldService();

            // Networking
            _tcp = new ServerTcp(_config);
            _handler = new DataHandler(_logic, _tcp, _world);

            _tcp.OnPacket += (id, pid, payload) =>
            {
                _handler.HandlePacket(id, pid, payload.Span);
            };
            _tcp.OnClientDisconnected += (id, accountId, characterId) =>
            {
                _handler.HandleDisconnect(id, accountId, characterId);
            };

            // Loop (runs even with 0 players)
            _loop = new ServerLoop(_world);
        }

        public void Start()
        {
            // 1) Boot world
            _world.Boot();

            // 2) Start engine loop
            _loop.Start();

            // 3) Start TCP
            _tcp.Start();

            Console.WriteLine("[Server] READY");
        }

        public void Stop()
        {
            _tcp.Stop();
            _loop.Stop();
        }

        public void Dispose()
        {
            Stop();
            _tcp.Dispose();
        }
    }
}

using Server.World;
using System.Diagnostics;

namespace Server.Networking
{
    internal sealed class ServerLoop
    {
        private readonly WorldService _world;

        private CancellationTokenSource? _cts;
        private Task? _task;

        public ServerLoop(WorldService world)
        {
            _world = world;
        }

        public void Start()
        {
            if (_task != null) return;

            _cts = new CancellationTokenSource();
            _task = Task.Run(() => RunAsync(_cts.Token));
        }

        public void Stop()
        {
            try
            {
                _cts?.Cancel();
                _task?.Wait(1000);
            }
            catch { /* ignore */ }
            finally
            {
                _cts = null;
                _task = null;
            }
        }

        private async Task RunAsync(CancellationToken ct)
        {
            // Basic tick cadence:
            // - FastTick: 250ms (movement queue later, timed events)
            // - SlowTick: 1000ms (regen/cooldowns)
            // - SpawnTick: 5000ms (NPC respawns later)

            var swFast = Stopwatch.StartNew();
            var swSlow = Stopwatch.StartNew();
            var swSpawn = Stopwatch.StartNew();

            while (!ct.IsCancellationRequested)
            {
                if (swFast.ElapsedMilliseconds >= 250)
                {
                    swFast.Restart();
                    _world.FastTick();
                }

                if (swSlow.ElapsedMilliseconds >= 1000)
                {
                    swSlow.Restart();
                    _world.SlowTick();
                }

                if (swSpawn.ElapsedMilliseconds >= 5000)
                {
                    swSpawn.Restart();
                    _world.SpawnTick();
                }

                await Task.Delay(10, ct);
            }
        }
    }
}
using Server.World;
using System.Diagnostics;

namespace Server.Networking
{
    internal sealed class ServerLoop
    {
        private readonly WorldService _world;
        private readonly Action _slowTickAction;

        private CancellationTokenSource? _cts;
        private Task? _task;

        public ServerLoop(WorldService world, Action? slowTickAction = null)
        {
            _world = world;
            _slowTickAction = slowTickAction ?? (() => { });
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
            // - FastTick: 250ms (future sub-second actions)
            // - WorldTick: 1000ms (regen, NPC timers, cooldowns, respawns)

            var swFast = Stopwatch.StartNew();
            var swWorld = Stopwatch.StartNew();

            while (!ct.IsCancellationRequested)
            {
                if (swFast.ElapsedMilliseconds >= 250)
                {
                    swFast.Restart();
                    _world.FastTick();
                }

                if (swWorld.ElapsedMilliseconds >= 1000)
                {
                    swWorld.Restart();
                    _world.SlowTick();
                    _world.SpawnTick();
                    _slowTickAction();
                }

                await Task.Delay(10, ct);
            }
        }
    }
}

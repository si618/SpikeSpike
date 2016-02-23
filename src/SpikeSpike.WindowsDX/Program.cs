using System;

// Shared amongst projects
// ReSharper disable once CheckNamespace
namespace SpikeSpike
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new SpikeSpike())
            {
                game.Run();
            }
        }
    }
#endif
}

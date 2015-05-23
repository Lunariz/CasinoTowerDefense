using System;

namespace CasinoTowerDefence
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (CasinoTowerDefence game = new CasinoTowerDefence())
            {
                game.Run();
            }
        }
    }
#endif
}


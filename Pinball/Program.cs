using System;

namespace Pinball
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (PinballGame game = new PinballGame())
            {
                game.Run();
            }
        }
    }
}

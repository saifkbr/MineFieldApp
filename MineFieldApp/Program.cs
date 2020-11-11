using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using static System.Console;

namespace MineFieldApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var config = InitConfig();

                // Validate config values
                if (config.MineSettings.Rows >= 4 && config.MineSettings.Columns >= 4 && config.MineSettings.Lifeline >= 3)
                {

                StartOfTheGame:

                    var mineGrid = new MineGrid(config.MineSettings.Rows, config.MineSettings.Columns);
                    var moves = new Moves(config.MineSettings.Columns, config.MineSettings.Rows);
                    var mineGenerator = new MineGenerator(config.MineSettings.Rows, config.MineSettings.Columns, config.MineSettings.Lifeline);
                    var minesExploded = Enumerable.Repeat(new Point(-1, -1), config.MineSettings.Lifeline).ToArray();

                    IPlayMineGame playMineGame = new PlayMineGame(mineGrid, moves, mineGenerator, minesExploded);

                    playMineGame.Play();

                    WriteLine("\n\nGame over");
                    WriteLine("\n\nEnter Y to play more otherwise press any key to exit.");

                    if (ReadLine().ToLower() == "y")
                        goto StartOfTheGame;
                    else
                        Environment.Exit(0);
                }
                else
                {
                    WriteLine("Invalid configuration.");
                }
            }
            catch (Exception ex)
            {
                // Log ex here
                WriteLine($"Error:{ex.Message}");
            }
        }

        private static AppConfig InitConfig()
        {
            var builder = new ConfigurationBuilder().AddJsonFile($"appSettings.json", true, true);

            return builder.Build().Get<AppConfig>();
        }
    }
}

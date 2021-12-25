using Spectre.Console.Cli;
using System.ComponentModel;

namespace bingo
{
    internal class BingoSettings : CommandSettings
    {
        [Description("Number of bingo passes of simulations")]
        [CommandOption("-p|--numberOfPasses")]
        [DefaultValue(1)]
        public int NumberOfPasses { get; set; }

        [Description("Number of bingo games to run in the simulation")]
        [CommandOption("-n|--numberOfGames")]
        [DefaultValue(100000)]
        public int NumberOfGames { get; set; }

        [Description("Number of numbers drawn per games")]
        [CommandOption("-d|--numberOfDraws")]
        [DefaultValue(22)]
        public int NumberOfNumbersToDraw { get; set; }

        [Description("Determines if a random sheet should be generated")]
        [CommandOption("-g|--generateSheet")]
        [DefaultValue(false)]
        public bool GenerateRandomSheet { get; set; }
    }
}
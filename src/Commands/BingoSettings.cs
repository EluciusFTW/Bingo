using Bingo.Extensions;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Text;

namespace Bingo.Commands
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

        [Description("Number of rows/columns in the sheet")]
        [CommandOption("-s|--sheetSize")]
        [DefaultValue(5)]
        public int SheetSize { get; set; }

        [Description("Factor of how much more options there are for a column compared to its length")]
        [CommandOption("-f|--sheetFactor")]
        [DefaultValue(3)]
        public int SheetFactor { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            new[]
            {
                $"Sheet Size: {SheetSize}",
                $"Sheet Factor: {SheetFactor}",
                $"Numbers to Draw: {NumberOfNumbersToDraw}",
                $"Number of Games: {NumberOfGames}",
                $"Number of Passes: {NumberOfPasses}",
            }
                .ForEach(line => builder.AppendLine(line));
            
            return builder.ToString();
        }
    }
}
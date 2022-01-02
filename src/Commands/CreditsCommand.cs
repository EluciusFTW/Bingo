using Bingo.Extensions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Bingo.Commands
{
    internal class CreditsCommand : Command
    {
        public override int Execute(CommandContext context)
        {
            PrintCredits();
            return 0;
        }

        private static void PrintCredits()
        {
            AnsiConsole.Write(new Rule());
            AnsiConsole.Write(
                new FigletText("Bingo CLI")
                    .LeftAligned()
                    .Color(Color.Green));
            
            AnsiConsole.Write(new Rule());
            new[]
            {
                "Stay up to date with newest development and features by",
                " - visiting the GitHub page [purple]https://github.com/EluciusFTW/Bingo [/]",
                " - following me on Twitter [blue]@EluciusFTW[/]",
                string.Empty,
                "Special thanks to ",
                " - [blue]@HonigCaro[/] for combinatorical discussions",
                " - [blue]@Firstdrafthell[/] for the awesome Spectre.Console package.",
                string.Empty
            }
                .ForEach(line => AnsiConsole.MarkupLine($"[green]{line}[/]"));

            AnsiConsole.Write(new Rule());
        }
    }
}

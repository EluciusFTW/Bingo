using Spectre.Console.Cli;

namespace bingo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandApp();
            app.Configure(configuration => 
                configuration
                    .AddCommand<BingoCommand>("run")
                    .WithDescription("Runs a bingo simulation with a set amount of games against a fixed bingo sheet.")
            );

            app.Run(args);
        }
    }
}

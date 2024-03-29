﻿using Bingo.Commands;
using Spectre.Console.Cli;

namespace Bingo
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

            app.Configure(configuration =>
                configuration
                    .AddCommand<CreditsCommand>("credits")
                    .WithDescription("Shows the credits.")
            );

            app.Run(args);
        }
    }
}

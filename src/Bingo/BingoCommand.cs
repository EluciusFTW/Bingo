﻿using Bingo.Extensions;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bingo.Bingo
{
    internal partial class BingoCommand : Command<BingoSettings>
    {
        public override int Execute(CommandContext context, BingoSettings settings)
        {
            var randomGenerator = new Random();
            
            var sheet = GetSheet(settings, randomGenerator);
            PrintSheet(sheet);

            Enumerable
                .Repeat(false, settings.NumberOfPasses)
                .ForEach(_ => {
                    var results = RunSimulation(sheet, settings, randomGenerator); 
                    PrintResults(settings, results);
                });

            return 0;
        }

        private static void PrintSheet(BingoSheet sheet)
        {
            Console.WriteLine();
            Line("Bingo Sheet");
            Console.WriteLine();
            Console.WriteLine(sheet.ToString());
        }

        private static BingoSheet GetSheet(BingoSettings settings, Random randomGenerator)
            => settings.GenerateRandomSheet
                ? BingoSheet.CreateRandom(randomGenerator)
                : BingoSheet.CreateDefault();

        private static void Line(string line)
        {
            var rule = new Rule($"[cyan]{line}[/]")
            {
                Alignment = Justify.Left
            };
            AnsiConsole.Write(rule);
        }

        private static IReadOnlyCollection<Bingo> RunSimulation(BingoSheet sheet, BingoSettings settings, Random randomGenerator)
        {
            var allNumbers = Enumerable.Range(1, 75).ToList();
            var bingoCombinations = sheet.GetBingos();

            return Enumerable
                .Repeat(false, settings.NumberOfGames)
                .Select(_ =>
                {
                    var numbers = allNumbers.PickRandomly(settings.NumberOfNumbersToDraw, randomGenerator);
                    return bingoCombinations
                        .Where(bingo => bingo.All(num => numbers.Contains(num)))
                        .Count();
                })
                .GroupBy(score => score)
                .OrderBy(grp => grp.Key)
                .Select(grp => new Bingo(grp.Key, grp.Count()))
                .ToList();
        }

        private static void PrintResults(BingoSettings settings, IReadOnlyCollection<Bingo> bingos)
        {
            Line("Results");
            foreach (var b in bingos)
            {
                var frequency = (double)b.Number / settings.NumberOfGames;
                Console.WriteLine($"You made a '{b.Arity}'-Bingo {b.Number} times ({frequency:P2})");
            }
            Console.WriteLine();
        }
    }
}
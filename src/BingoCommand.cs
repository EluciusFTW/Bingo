using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bingo
{
    internal partial class BingoCommand : Command<BingoSettings>
    {
        public override int Execute(CommandContext context, BingoSettings settings)
        {
            var randomGenerator = new Random();
            var allNumbers = Enumerable.Range(1, 75).ToList();

            var sheet = GetSheet(settings, randomGenerator);
            var bingoCombinations = GetBingos(sheet);

            Enumerable
                .Repeat(false, settings.NumberOfPasses)
                .ForEach(_ => RunSimulation(settings, randomGenerator, bingoCombinations, allNumbers));

            return 0;
        }

        private static int[][] GetSheet(BingoSettings settings, Random randomGenerator)
        {
            var sheet = settings.GenerateRandomSheet
                ? GenerateSheet(randomGenerator)
                : DefaultSheet();

            PrintSheet(sheet);
            return sheet;
        }

        private static void Line(string line)
        {
            var rule = new Rule($"[cyan]{line}[/]")
            {
                Alignment = Justify.Left
            };
            AnsiConsole.Write(rule);
        }

        private static void RunSimulation(BingoSettings settings, Random randomGenerator, IReadOnlyCollection<IReadOnlyCollection<int>> bingoCombinations, List<int> allNumbers)
        {
            var bingos = Enumerable
                .Repeat(false, settings.NumberOfGames)
                .Select(_ =>
                {
                    var numbers = NOutOf(randomGenerator, settings.NumberOfNumbersToDraw, allNumbers);
                    return bingoCombinations
                        .Where(bingo => bingo.All(num => numbers.Contains(num)))
                        .Count();
                })
                .GroupBy(score => score)
                .OrderBy(grp => grp.Key)
                .Select(grp => new Bingo(grp.Key, grp.Count()))
                .ToList();

            PrintResults(settings, bingos);
        }

        private static void PrintResults(BingoSettings settings, IReadOnlyCollection<Bingo> bingos)
        {
            Line("Results");
            foreach (var b in bingos)
            {
                var frequency = (double)b.number / settings.NumberOfGames;
                Console.WriteLine($"You made a '{b.arity}'-Bingo {b.number} times ({frequency:P2})");
            }
            Console.WriteLine();
        }

        private static void PrintSheet(int[][] sheet)
        {
            Console.WriteLine();
            new[]
            { 
                new[] { " B", " I", " N", " G", " O" },
                new[] { "--", "--", "--", "--", "--" }
            }
                .Concat(sheet.Select(row => row.Select(cell => cell < 10 ? $" {cell}" : $"{cell}")))
                .ForEach(row => Console.WriteLine(string.Join(" | ", row)));

            Console.WriteLine();
        }

        private static IReadOnlyCollection<int> NOutOf(Random randomGenerator, int nrOfElements, IReadOnlyCollection<int> input)
        {
            var alreadyHit = new List<int>();
            var results = new List<int>();
            while (alreadyHit.Count < nrOfElements)
            {
                var element = randomGenerator.Next(input.Count);
                if (alreadyHit.Contains(element)) continue;

                alreadyHit.Add(element);
                results.Add(input.ElementAt(element));
            }
            return results;
        }

        private static int[][] GenerateSheet(Random randomGenerator)
        {
            var invertedSheet = Enumerable
                .Range(0, 5)
                .Select(index => NOutOf(randomGenerator, 5, Enumerable.Range(1, 15).Select(n => n + 15 * index).ToList()).ToArray())
                .ToArray();

            return Enumerable
                .Range(0, 5)
                .Select(index => invertedSheet.Select(row => row[index]).ToArray())
                .ToArray();
        }

        private static int[][] DefaultSheet()
            => new[]
            {
                new[] {1, 16, 31, 46, 61},
                new[] {2, 17, 32, 47, 62},
                new[] {3, 18, 33, 48, 63},
                new[] {4, 19, 34, 49, 64},
                new[] {5, 20, 35, 50, 65}
            };

        private static IReadOnlyCollection<IReadOnlyCollection<int>> GetBingos(int[][] sheet)
        {
            var rows = Enumerable
                .Range(0, 5)
                .Select(index => sheet[index]);

            var columns = Enumerable
                .Range(0, 5)
                .Select(index => sheet.Select(row => row[index]).ToArray());

            var mainDiagonal = Enumerable
                .Range(0, 5)
                .Select(index => sheet[index][index])
                .ToArray();

            var secondaryDiagonal = Enumerable
                .Range(0, 5)
                .Select(index => sheet[4 - index][index])
                .ToArray();

            return rows
                .Concat(columns)
                .Append(mainDiagonal)
                .Append(secondaryDiagonal)
                .ToList();
        }
    }
}
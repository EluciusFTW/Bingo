using Bingo.Extensions;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bingo.Bingo
{
    internal class BingoSheet
    {
        private readonly int[][] _sheet;

        private BingoSheet()
        {
            _sheet = new[]
            {
                new[] {1, 16, 31, 46, 61},
                new[] {2, 17, 32, 47, 62},
                new[] {3, 18, 33, 48, 63},
                new[] {4, 19, 34, 49, 64},
                new[] {5, 20, 35, 50, 65}
            };
        }

        private BingoSheet(int[][] sheet)
        {
            _sheet = sheet;
        }

        public static BingoSheet CreateDefault() 
            => new();

        public static BingoSheet CreateRandom(Random randomGenerator) 
            => new(GenerateSheet(randomGenerator));

        public IReadOnlyCollection<IReadOnlyCollection<int>> GetBingos()
        {
            var rows = Enumerable
                .Range(0, 5)
                .Select(index => _sheet[index]);

            var columns = Enumerable
                .Range(0, 5)
                .Select(index => _sheet.Select(row => row[index]).ToArray());

            var mainDiagonal = Enumerable
                .Range(0, 5)
                .Select(index => _sheet[index][index])
                .ToArray();

            var secondaryDiagonal = Enumerable
                .Range(0, 5)
                .Select(index => _sheet[4 - index][index])
                .ToArray();

            return rows
                .Concat(columns)
                .Append(mainDiagonal)
                .Append(secondaryDiagonal)
                .ToList();
        }

        private static int[][] GenerateSheet(Random randomGenerator)
        {
            var invertedSheet = Enumerable
                .Range(0, 5)
                .Select(index => Enumerable
                    .Range(1, 15)
                    .Select(n => n + 15 * index)
                    .ToList()
                    .NOutOf(5, randomGenerator)
                    .ToArray())
                .ToArray();

            return Enumerable
                .Range(0, 5)
                .Select(index => invertedSheet.Select(row => row[index]).ToArray())
                .ToArray();
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            new[]
            {
                new[] { " B", " I", " N", " G", " O" },
                new[] { "--", "--", "--", "--", "--" }
            }
                .Concat(_sheet.Select(row => row.Select(cell => cell < 10 ? $" {cell}" : $"{cell}")))
                .Select(row => string.Join(" | ", row))
                .ForEach(row => stringBuilder.AppendLine(row));

            return stringBuilder.ToString();
        }
    }
}
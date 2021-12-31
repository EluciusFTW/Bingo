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
        private readonly int _size;
        private readonly int _factor;

        private static readonly string[] _names = new[]
        {
            "",
            "X",
            "YO",
            "OMG",
            "DUDE",
            "BINGO",
            "HELIOS",
            "YOGHURT",
            "BORNSENF",
            "SUPERBING"
        };

        private BingoSheet(int size, int factor)
        {
            var gap = factor * size;
            var invertedSheet = Enumerable
                .Range(0, size)
                .Select(index => Enumerable
                    .Range(1, size)
                    .Select(n => n + gap * index)
                    .ToArray())
                .ToArray();
            _size = size;
            _factor = factor;
            _sheet = Transpose(invertedSheet);
        }

        private BingoSheet(int[][] sheet, int size, int factor)
        {
            _sheet = sheet;
            _size = size;
            _factor = factor;
        }

        public static BingoSheet CreateDefault(int size, int factor) 
            => new(size, factor);

        public static BingoSheet CreateRandom(int size, int factor, Random randomGenerator) 
            => new(GenerateSheet(size, factor, randomGenerator), size, factor);

        public IReadOnlyCollection<IReadOnlyCollection<int>> GetBingos()
        {
            var rows = Enumerable
                .Range(0, _size)
                .Select(index => _sheet[index]);

            var columns = Enumerable
                .Range(0, _size)
                .Select(index => _sheet.Select(row => row[index]).ToArray());

            var mainDiagonal = Enumerable
                .Range(0, _size)
                .Select(index => _sheet[index][index])
                .ToArray();

            var secondaryDiagonal = Enumerable
                .Range(0, _size)
                .Select(index => _sheet[_size - index - 1][index])
                .ToArray();

            return rows
                .Concat(columns)
                .Append(mainDiagonal)
                .Append(secondaryDiagonal)
                .ToList();
        }

        private static int[][] GenerateSheet(int size, int factor, Random randomGenerator)
        {
            var gap = factor * size;
            var invertedSheet = Enumerable
                .Range(0, size)
                .Select(index => Enumerable
                    .Range(1, gap)
                    .Select(number => number + gap * index)
                    .ToList()
                    .PickRandomly(size, randomGenerator)
                    .ToArray())
                .ToArray();

            return Transpose(invertedSheet);
        }

        private static int[][] Transpose(int[][] matrix)
            => Enumerable
                .Range(0, matrix[0].Length)
                .Select(index => matrix.Select(row => row[index]).ToArray())
                .ToArray();

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            var space = new string(' ', 2);
            var divider = new string('-', 3);
            new[]
            {
                _names[_size].Select(letter => $"{space}{letter}").ToArray(),
                Enumerable.Repeat(divider, _size).ToArray()
            }
                .Concat(_sheet.Select(row => row.Select(ToFormattedCell)))
                .Select(row => string.Join(" | ", row))
                .ForEach(row => stringBuilder.AppendLine(row));

            return stringBuilder.ToString();
        }

        private string ToFormattedCell(int cellValue) 
        {
            return cellValue < 10 
                ? $"  {cellValue}" 
                : cellValue < 100 
                    ? $" {cellValue}"
                    : $"{cellValue}";
        }
    }
}
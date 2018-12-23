using System;
using System.Collections.Generic;
using System.Text;

namespace ParserModel.Models
{
    public class DictionaryMonths
    {
        public static IDictionary<string, int> Months = new Dictionary<string, int>
        {
            {"STY", 1},
            {"LUT", 2},
            {"MAR", 3},
            {"KWI", 4},
            {"MAJ", 5},
            {"CZE", 6},
            {"LIP", 7},
            {"SIE", 8},
            {"WRZ", 9},
            {"PAZ", 10},
            {"LIS", 11},
            {"GRU", 12},
        };
    }
}
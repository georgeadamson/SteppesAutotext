using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoTextDataMigrator
{
    class Tools
    {
        public static List<string> Split(string input, int chunkSize)
        {
            List<string> output = new List<string>();

            while (input.Length > 0)
            {
                if (input.Length < chunkSize) chunkSize = input.Length;
                output.Add(input.Substring(0, chunkSize));
                input = input.Substring(chunkSize);
            }

            return output;
        } 
    }
}

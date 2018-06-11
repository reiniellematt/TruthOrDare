﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TruthOrDareUI
{
    public static class GlobalConfig
    {
        private static readonly string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Config.txt");

        public static int MinutesToCompleteChallenge = 0, SecondsBeforeReveal = 0;
        public static List<string> PlayersFromLastSession = new List<string>();

        public static void PrepareContents()
        {
            List<string> output = new List<string>();

            if (!File.Exists(filePath))
            {
                List<string> config = new List<string>
                {
                    "2",
                    "5"
                };

                MinutesToCompleteChallenge = 2;
                SecondsBeforeReveal = 5;

                File.WriteAllLines(filePath, config);
            }

            string[] contents = File.ReadAllLines(filePath);

            MinutesToCompleteChallenge = int.Parse(contents[0]);
            SecondsBeforeReveal = int.Parse(contents[1]);

            if (contents.Length > 2)
            {
                for (int i = 3; i < contents.Length; i++)
                {
                    PlayersFromLastSession.Add(contents[i]);
                }
            }
        }
    }
}
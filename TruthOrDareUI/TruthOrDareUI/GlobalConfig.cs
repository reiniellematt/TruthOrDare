using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TruthOrDareUI
{
    public static class GlobalConfig
    {
        private static readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Config.txt");
        private static readonly Random _generator = new Random();

        public static int MinutesToCompleteChallenge = 0, SecondsBeforeReveal = 0;
        public static ObservableCollection<string> PlayersFromLastSession = new ObservableCollection<string>();
        public static void PrepareGameConfig()
        {
            List<string> output = new List<string>();

            if (!File.Exists(_filePath))
            {
                List<string> config = new List<string>
                {
                    "2",
                    "5"
                };

                MinutesToCompleteChallenge = 2;
                SecondsBeforeReveal = 5;

                File.WriteAllLines(_filePath, config);
            }

            string[] contents = File.ReadAllLines(_filePath);

            MinutesToCompleteChallenge = int.Parse(contents[0]);
            SecondsBeforeReveal = int.Parse(contents[1]);

            if (contents.Length > 2)
            {
                for (int i = 2; i < contents.Length; i++)
                {
                    PlayersFromLastSession.Add(contents[i]);
                }
            }
        }
        public static async Task SaveTimeConfig(string minutes, string seconds)
        {
            List<string> contents = new List<string>();

            contents.Add(minutes);
            contents.Add(seconds);
            contents.AddRange(PlayersFromLastSession);

            using (StreamWriter sw = new StreamWriter(_filePath, false))
            {
                foreach (string content in contents)
                {
                    await sw.WriteLineAsync(content);
                }
            }
        }
        public static async Task SavePlayersFromLastSession()
        {
            List<string> contents = new List<string>();

            contents.Add(MinutesToCompleteChallenge.ToString());
            contents.Add(SecondsBeforeReveal.ToString());
            contents.AddRange(GlobalConfig.PlayersFromLastSession);

            using (StreamWriter sw = new StreamWriter(_filePath, false))
            {
                foreach (string content in contents)
                {
                    await sw.WriteLineAsync(content);
                }
            }
        }
        public static string GenerateName()
        {
            lock (_generator)
            {
                string output = string.Empty;

                try
                {
                    output = PlayersFromLastSession[_generator.Next(0, PlayersFromLastSession.Count)];
                }
                catch (ArgumentOutOfRangeException)
                {
                    output = "No players yet";
                }

                return output;
            }
        }
    }
}
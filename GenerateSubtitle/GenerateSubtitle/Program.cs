using Newtonsoft.Json;
using System;
using System.IO;

namespace GenerateSubtitle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConvertNumberToTimeOfSubtitle();

            Console.Read();
        }
        private static void ConvertNumberToTimeOfSubtitle()
        {
            //6319
            //00:00:02,958 --> 00:00:05,541
            using (StreamReader r = new StreamReader($"subtitle.json"))
            {
                string nameFile = Console.ReadLine();
                string json = r.ReadToEnd();

                var lines = JsonConvert.DeserializeObject<InitialModel>(json);
                using StreamWriter file = new($"{nameFile}.srt");

                int pos = 1;

                foreach (var line in lines.InitialSegments)
                {
                    var data = line;

                    string start = line.TranscriptSegmentRenderer.StartMs;
                    string end = line.TranscriptSegmentRenderer.EndMs;
                    string text = line.TranscriptSegmentRenderer.Snippet.Runs[0].Text;

                    if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
                    {

                        string time = String.Empty;

                        if (start.Length <= 3)
                        {
                            continue;
                        }
                        else
                        {
                            string milisecondStart = start.Substring(start.Length - 3);
                            string secondStart = start.Substring(0, start.Length - 3);

                            string milisecondEnd = end.Substring(end.Length - 3);
                            string secondEnd = end.Substring(0, end.Length - 3);

                            time = $"00:{FormatNumberToTime(secondStart)},{milisecondStart} --> 00:{FormatNumberToTime(secondEnd)},{milisecondEnd}";
                        }

                        file.WriteLine(pos);
                        file.WriteLine(time);
                        file.WriteLine(text.Replace("\n", " "));
                        file.WriteLine("");

                        pos += 1;
                    }
                }

                file.Close();
            }
        }

        private static string FormatNumberToTime(string num)
        {
            string formatCorrect = string.Empty;

            int number = Convert.ToInt32(num);
            //hour
            //var hours = number / 360;
            var minutes = number / 60;
            var seconds = number % 60;

            if (minutes == 0)
            {
                formatCorrect = $"00:{(seconds > 9 ? seconds : $"0{seconds}")}";
            }
            else
            {
                formatCorrect = $"{(minutes > 9 ? minutes : $"0{minutes}")}:{(seconds > 9 ? seconds : $"0{seconds}")}";
            }

            return formatCorrect;
        }
    }
}

using Psbds.LUIS.IntentCSVExport.Core;
using System;
using System.IO;
using System.Text;

namespace Psbds.LUIS.IntentCSVExport.Console
{
    class Program
    {
        public static string appId;
        public static string appKey;
        public static string appVersion;
        public static string filePath;

        static void Main(string[] args)
        {
            AskInformation(ref appId, "Please provide your application id or EXIT:");
            AskInformation(ref appKey, "Please provide your application key or EXIT:");
            AskInformation(ref appVersion, "Please provide your app version or EXIT:");
            AskInformation(ref filePath, "Please provide your csv file path or EXIT:");

            var exportService = new ExportService(appId, appKey, appVersion);

            var data = exportService.Export().Result;

            var sb = new StringBuilder();
            sb.AppendLine("intent;utterance");

            data.ForEach(item =>
            {
                sb.AppendLine($"{item.Item1};{item.Item2}".Replace("\n", ""));
            });

            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.Write(sb.ToString());
            }

            System.Console.WriteLine($"{data.Count} lines wrote in {filePath}");
            System.Console.ReadLine();
        }

        private static void AskInformation(ref string value, string phrase)
        {
            System.Console.WriteLine(phrase);
            value = System.Console.ReadLine();
            if (value.ToUpper() == "EXIT")
            {
                throw new ArgumentException();
            }
            if (String.IsNullOrEmpty(value))
                AskInformation(ref value, phrase);
        }

    }
}

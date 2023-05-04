using System;
using System.IO;
using System.Text;

namespace ue_config
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string configDirPath = args[0]; // "C:\\Users\\MyComputer\\Documents\\notd\\Config"
            string configFileName = args[1]; // "DefaultEngine.ini"
            string keyString = args[2]; // "ProjectVersion="
            string ValueString = args[3]; // 
            if (args.Length != 4)
            {
                Console.WriteLine("Invalid Argument. 0:ConfigDirPath, 1:ConfigFileName, 2:Key, 3:Value");
                return;
            }
            if (Directory.Exists(args[0]) == false)
            {
                Console.WriteLine("Invalid Config Directory Path");
                return;
            }

            string ConfigFilePath = Path.Combine(configDirPath, configFileName);
            string[] ConfigTexts = System.IO.File.ReadAllLines(ConfigFilePath);

            int DesiredTextLineIndex = -1;

            Console.WriteLine("Update " + configFileName);
            Console.WriteLine("Key : " + keyString);

            for (int i = 0; i < ConfigTexts.Length; i++)
            {
                if (ConfigTexts[i].Contains(keyString))
                {
                    Console.WriteLine("Old value : " + ConfigTexts[i]);
                    DesiredTextLineIndex = i;
                    break;
                }
            }

            ConfigTexts[DesiredTextLineIndex] = $"{keyString}{ValueString}";

            using (StreamWriter outputFile = new StreamWriter(ConfigFilePath))
            {
                foreach (string defaultGameTextLine in ConfigTexts)
                {
                    outputFile.WriteLine(defaultGameTextLine);
                    Console.WriteLine("Old value : " + ConfigTexts[DesiredTextLineIndex]);
                }
            }
            Console.WriteLine("Update success.");
        }
    }
}

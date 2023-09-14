using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ue_config
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string configDirPath = args[0]; // "C:\\Users\\MyComputer\\Documents\\notd\\Config"
            string configFileName = args[1]; // "DefaultEngine.ini"
            string keyString = args[2]; // "ProjectVersion"
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
            string[] lines = System.IO.File.ReadAllLines(ConfigFilePath);

            Console.WriteLine("Update " + configFileName);
            Console.WriteLine("Key : " + keyString);

            // 정규식 패턴 정의
            string pattern = $@"({keyString}="")[^""]*("")"; 
            string replacement = $"{keyString}=\"{ValueString}\"";
            
            // 텍스트에서 키에 해당하는 값을 변경
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(keyString))
                {
                    //Print log
                    Console.WriteLine("Line : " + i + "");
                    Console.WriteLine("Old Value : " + lines[i]);
                    lines[i] = Regex.Replace(lines[i], pattern, replacement);
                    Console.WriteLine("New Value : " + lines[i]);
                }
            }

            // 수정된 내용을 파일에 저장
            File.WriteAllLines(ConfigFilePath, lines);
            Console.WriteLine("Update success.");
        }
    }
}

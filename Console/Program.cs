// See https://aka.ms/new-console-template for more information
using Model;

namespace Console
{
    public static class Console
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "--clear-map")
            {
                ClearMapFiles();
            }
            else if (args[0] == "--generate-map" && args.Length > 1 && int.TryParse(args[1], out int mapCount))
            {
                GenerateMaps(mapCount);
            }
            else if (args[0] == "--print-map" && args.Length > 1)
            {
                PrintMap(args);
            }
            else if (args[0] == "--generate-char" && args.Length > 1 && Enum.TryParse(args[1], out Model.Enum.CharacterClass classType))
            {
                // 캐릭터 생성
                Model.Character character = Tool.Character.Generate(classType);
                Tool.Character.Print(character);
                Tool.Character.Save(character);
                System.Console.WriteLine(character);
            }
            else if (args[0] == "--print-char" && args.Length > 1)
            {
                // 캐릭터 파일 출력
                foreach(var character in Tool.Character.LoadAll(args[1]))
                {
                    Tool.Character.Print(character);
                }
            }
            else
            {
                System.Console.WriteLine("Invalid argument. Use --clear-map or --generate-map N.");
            }
        }

        // 여러 개의 맵을 생성하는 함수
        private static void GenerateMaps(int mapCount)
        {
            int width = 10;
            int height = 10;
            float mountainRatio = 0.1f;     // 산 비율
            float waterRatio = 0.1f;        // 물 비율
            float forestRatio = 0.2f;       // 숲 비율
            float swampRatio = 0.2f;        // 늪 비율

            for (int i = 0; i < mapCount; i++)
            {
                // 맵 생성
                Cell[,] field = Tool.Field.Generate(width, height, mountainRatio, waterRatio, forestRatio, swampRatio);

                // 맵을 파일로 저장하고 다시 로드
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string filename = Tool.Field.Save(field, timestamp, mapCount > 1 ? i + 1 : i);
                Cell[,] saveField = Tool.Field.Load(filename);

                // 저장된 맵 출력
                System.Console.WriteLine($"Generated map {i + 1}:");
                Tool.Field.Print(saveField);
                System.Console.WriteLine();
            }

            System.Console.WriteLine($"{mapCount} map(s) have been generated.");
        }

        private static void ClearMapFiles()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string[] mapFiles = Directory.GetFiles(currentDirectory, "*.map");

            if (mapFiles.Length == 0)
            {
                System.Console.WriteLine("No .map files found.");
                return;
            }

            foreach (var file in mapFiles)
            {
                try
                {
                    File.Delete(file);
                    System.Console.WriteLine($"Deleted: {file}");
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"Error deleting {file}: {ex.Message}");
                }
            }

            System.Console.WriteLine("All .map files have been deleted.");
        }

        public static void PrintMap(string[] args) {
            // 맵 파일을 읽어서 출력
            if (args[1] == "all")
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string[] mapFiles = Directory.GetFiles(currentDirectory, "*.map");

                if (mapFiles.Length == 0)
                {
                    System.Console.WriteLine("No .map files found.");
                    return;
                }

                foreach (var file in mapFiles)
                {
                    System.Console.WriteLine(file + ":");
                    Cell[,] field = Tool.Field.Load(file);
                    Tool.Field.Print(field);
                    System.Console.WriteLine();
                }
            }
            else
            {
                for (int i = 1; i < args.Length; i++)
                {
                    string filename = args[i];
                    System.Console.WriteLine(filename + ":");
                    Cell[,] field = Tool.Field.Load(filename);
                    Tool.Field.Print(field);
                    System.Console.WriteLine();
                }
            }
        }
    }
}


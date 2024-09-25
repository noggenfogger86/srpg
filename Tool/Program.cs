// See https://aka.ms/new-console-template for more information
using Model;
using Tool;

namespace Tool
{
    public static class Tool
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "--clear-map")
            {
                // --clear 매개변수가 있으면 .map 파일을 삭제
                ClearMapFiles();
            }
            else if (args[0] == "--generate-map" && args.Length > 1 && int.TryParse(args[1], out int mapCount))
            {
                GenerateMaps(mapCount);
            }
            else if (args[0] == "--print-map" && args.Length > 1)
            {
                // 맵 파일을 읽어서 출력
                if (args[1] == "all")
                {
                    string currentDirectory = Directory.GetCurrentDirectory();
                    string[] mapFiles = Directory.GetFiles(currentDirectory, "*.map");

                    if (mapFiles.Length == 0)
                    {
                        Console.WriteLine("No .map files found.");
                        return;
                    }

                    foreach (var file in mapFiles)
                    {
                        Console.WriteLine(file + ":");
                        Cell[,] field = FieldGenerator.Load(file);
                        FieldGenerator.PrintField(field);
                        Console.WriteLine();
                    }
                }
                else
                {
                    for (int i = 1; i < args.Length; i++)
                    {
                        string filename = args[i];
                        Console.WriteLine(filename + ":");
                        Cell[,] field = FieldGenerator.Load(filename);
                        FieldGenerator.PrintField(field);
                        Console.WriteLine();
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid argument. Use --clear-map or --generate-map N.");
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
                Cell[,] field = FieldGenerator.GenerateField(width, height, mountainRatio, waterRatio, forestRatio, swampRatio);

                // 맵을 파일로 저장하고 다시 로드
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string filename = FieldGenerator.Save(field, timestamp, mapCount > 1 ? i + 1 : i);
                Cell[,] saveField = FieldGenerator.Load(filename);

                // 저장된 맵 출력
                Console.WriteLine($"Generated map {i + 1}:");
                FieldGenerator.PrintField(saveField);
                Console.WriteLine();
            }

            Console.WriteLine($"{mapCount} map(s) have been generated.");
        }

        private static void ClearMapFiles()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string[] mapFiles = Directory.GetFiles(currentDirectory, "*.map");

            if (mapFiles.Length == 0)
            {
                Console.WriteLine("No .map files found.");
                return;
            }

            foreach (var file in mapFiles)
            {
                try
                {
                    File.Delete(file);
                    Console.WriteLine($"Deleted: {file}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting {file}: {ex.Message}");
                }
            }

            Console.WriteLine("All .map files have been deleted.");
        }
    }
}


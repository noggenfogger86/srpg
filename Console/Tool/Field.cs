using Model;

namespace Console.Tool
{
    public static class Field
    {
        // 필드를 생성하는 함수
        public static Cell[,] Generate(int width, int height, float mountainRatio, float waterRatio, float forestRatio, float swampRatio)
        {
            var field = new Cell[width, height];
            var random = new Random();

            // 전체 셀의 수
            int totalCells = width * height;

            // 각 특수 터레인에 배정할 셀의 수
            int mountainCells = (int)(totalCells * mountainRatio);
            int waterCells = (int)(totalCells * waterRatio);
            int forestCells = (int)(totalCells * forestRatio);
            int swampCells = (int)(totalCells * swampRatio);

            // Plain으로 기본 초기화
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    field[x, y] = new Cell(x, y, isOccupied: false, terrain: Model.Enum.Terrain.Plain);
                }
            }

            // 재귀적 확장 방식으로 특수 터레인 배치
            PlaceRecursiveClusteredTerrain(field, Model.Enum.Terrain.Mountain, mountainCells, random);
            PlaceRecursiveClusteredTerrain(field, Model.Enum.Terrain.Water, waterCells, random);
            PlaceRecursiveClusteredTerrain(field, Model.Enum.Terrain.Forest, forestCells, random);
            PlaceRecursiveClusteredTerrain(field, Model.Enum.Terrain.Swamp, swampCells, random);

            return field;
        }

        // 재귀적 확장 방식으로 터레인을 배치하는 함수
        private static void PlaceRecursiveClusteredTerrain(Cell[,] field, Model.Enum.Terrain terrain, int count, Random random)
        {
            int width = field.GetLength(0);
            int height = field.GetLength(1);

            // 랜덤한 시작점을 고르고 재귀적으로 확장
            while (count > 0)
            {
                int startX = random.Next(0, width);
                int startY = random.Next(0, height);

                // 확장 시작
                if (field[startX, startY].Terrain == Model.Enum.Terrain.Plain)
                {
                    field[startX, startY].Terrain = terrain;
                    count--;
                    RecursiveExpand(field, startX, startY, terrain, ref count, random, probability: 0.9);
                }
            }
        }

        // 재귀적으로 확장하는 함수
        private static void RecursiveExpand(Cell[,] field, int x, int y, Model.Enum.Terrain terrain, ref int count, Random random, double probability)
        {
            if (count <= 0 || probability <= 0.1)  // 종료 조건: 남은 셀 수가 0이거나 확률이 너무 낮을 때
                return;

            int width = field.GetLength(0);
            int height = field.GetLength(1);
            int[] dx = { 0, 0, -1, 1 };  // 좌우
            int[] dy = { -1, 1, 0, 0 };  // 상하

            // 4방향으로 확장
            for (int i = 0; i < 4; i++)
            {
                int newX = x + dx[i];
                int newY = y + dy[i];

                if (newX >= 0 && newX < width && newY >= 0 && newY < height)
                {
                    // 확장할 확률에 따라 결정
                    if (random.NextDouble() < probability && field[newX, newY].Terrain == Model.Enum.Terrain.Plain)
                    {
                        field[newX, newY].Terrain = terrain;
                        count--;

                        // 확률을 점진적으로 감소시키며 재귀적으로 확장
                        RecursiveExpand(field, newX, newY, terrain, ref count, random, probability * 0.7);  // 확률 감소
                    }
                }
            }
        }

        public static string Save(Cell[,] field, string timestamp, int index = 0)
        {
            string filename = index == 0 ?  timestamp+ ".map"
            : string.Format("{0}_{1}.map", timestamp, index);  // 파일 이름 생성

            using (StreamWriter writer = new StreamWriter(filename))
            {
                int width = field.GetLength(0);
                int height = field.GetLength(1);

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        // 터레인의 Enum 값을 숫자로 변환하여 공백으로 구분
                        writer.Write((int)field[x, y].Terrain + " ");
                    }
                    writer.WriteLine();  // 개행
                }
            }

            System.Console.WriteLine($"Field saved to {filename}");

            return filename;
        }

        public static Cell[,] Load(string filename)
        {
            if (!File.Exists(filename))
            {
                System.Console.WriteLine("File not found!");
                return new Cell[0, 0];
            }

            string[] lines = File.ReadAllLines(filename);
            int width = lines.Length;
            int height = lines[0].Split(' ').Length - 1; // 마지막 공백 제외

            Cell[,] field = new Cell[width, height];

            for (int x = 0; x < width; x++)
            {
                string[] values = lines[x].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                for (int y = 0; y < height; y++)
                {
                    int terrainValue = int.Parse(values[y]);
                    Model.Enum.Terrain terrain = (Model.Enum.Terrain)terrainValue;
                    field[x, y] = new Cell(x, y, isOccupied: false, terrain: terrain);
                }
            }
            return field;
        }

        // 필드를 2차원 배열 형태로 출력하는 함수
        public static void Print(Cell[,] field)
        {
            int width = field.GetLength(0);
            int height = field.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (field[x, y].IsOccupied && field[x, y].Occupant.HasValue)
                    {
                        System.Console.Write(CharacterSymbol(field[x, y].Occupant.Value));
                    }
                    else
                    {
                        System.Console.Write(TerrainToSymbol(field[x, y].Terrain));
                    }
                }
                System.Console.WriteLine();  // 줄바꿈
            }
        }

        // 터레인에 맞는 출력 기호 반환
        private static string TerrainToSymbol(Model.Enum.Terrain terrain)
        {
            return terrain switch
            {
                Model.Enum.Terrain.Plain => "[ ]",
                Model.Enum.Terrain.Forest => "[$]",
                Model.Enum.Terrain.Water => "[o]",
                Model.Enum.Terrain.Mountain => "[^]",
                _ => "[ ]",  // 기본적으로 Plain
            };
        }

        // 직업명 첫글자
        private static string CharacterSymbol(FieldCharacter fieldCharacter)
        {
            return $"[{fieldCharacter.Character.ClassType.ToString()[..1].ToLower()}]";
        }
    }
}

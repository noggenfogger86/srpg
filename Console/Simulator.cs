using System.Runtime.CompilerServices;
using Model;

namespace Console
{
    public static class Simulator
    {
        public static Core.Battle Build(Dictionary<long, List<Character>> teams, Field field)
        {
            // 예시: 팀과 캐릭터 구성 및 전투 시작
            var battle = new Core.Battle();
            foreach(var kv in teams)
            {
                var team = new Core.Team(kv.Key);
                // 캐릭터 배치
                foreach(var character in kv.Value)
                {
                    FieldCharacter fieldCharacter = new FieldCharacter();
                    RandomPlaceCharacter(team.TeamId, character, ref field, ref fieldCharacter);
                    Core.Helpers.Action.Initialize(ref fieldCharacter);
                    Core.Helpers.Move.Initialize(ref fieldCharacter);
                    team.AddCharacter(fieldCharacter);
                }
                battle.AddTeam(team);
            }
            battle.Field = field;
            Tool.Field.Print(field.Cells);  // 배치 후 다시 맵 출력

            return battle;
        }

        public static void Print(Core.Battle battle)
        {
            Core.Helpers.BattleHelper.Print(battle);
        }

        public static void Run(Core.Battle battle)
        {
            Core.Helpers.BattleHelper.AutoBattle(battle);
        }

        public static bool RandomPlaceCharacter(long UserId, Character character, ref Field field, ref FieldCharacter fieldCharacter)
        {
            Random random = new();
            int width = field.Cells.GetLength(0);
            int height = field.Cells.GetLength(1);

            // 가능한 배치 위치를 찾는 플래그
            bool placed = false;

            while (!placed)
            {
                int x = random.Next(0, width);
                int y = random.Next(0, height);
                // 평지(Plain Terrain)이고 Occupied가 false인 셀에 배치
                if (field.Cells[x, y].Terrain == Model.Enum.Terrain.Plain && !field.Cells[x, y].IsOccupied)
                {
                    // 캐릭터 배치
                    fieldCharacter = new FieldCharacter(character, x, y, userId: UserId);

                    // 셀을 Occupied 상태로 변경
                    field.Cells[x, y].IsOccupied = true;
                    field.Cells[x, y].Occupant = fieldCharacter;

                    // 캐릭터가 배치되었음을 알림
                    placed = true;
                    System.Console.WriteLine($"Character placed at position: X = {x}, Y = {y}");
                }
            }

            return placed;  // 캐릭터 배치 성공
        }

        // 캐릭터와 맵을 로드하는 함수
        public static (List<Character>, Cell[,]) LoadCharactersAndMap(string path)
        {
            // 캐릭터 파일들 로드
            List<Character> characters = Tool.Character.LoadAll(path);
            if (characters.Count == 0)
            {
                System.Console.WriteLine("No characters found in the specified path.");
            }
            else
            {
                System.Console.WriteLine($"{characters.Count} characters loaded.");
            }

            // .map 파일 로드
            string[] mapFiles = Directory.GetFiles(path, "*.map");

            if (mapFiles.Length == 0)
            {
                System.Console.WriteLine("No .map files found in the specified path.");
                return (null, new Cell[0, 0]);
            }

            // 첫 번째 .map 파일을 가져와서 로드
            string mapFile = mapFiles[0];
            System.Console.WriteLine($"Loading map: {mapFile}");
            Cell[,] map = Tool.Field.Load(mapFile);

            // 로드된 캐릭터 정보 및 맵을 콘솔에 출력 (테스트용)
            PrintLoadedData(characters, map);

            return (characters, map);
        }

        // 로드된 데이터를 콘솔에 출력하는 테스트 함수
        private static void PrintLoadedData(List<Character> characters, Cell[,] map)
        {
            System.Console.WriteLine("===== Loaded Characters =====");
            foreach (var character in characters)
            {
                Model.Character.Print(character); // 캐릭터 정보를 출력하는 함수 호출
            }

            System.Console.WriteLine("===== Loaded Map =====");
            Tool.Field.Print(map);  // 맵을 출력하는 가정하에
        }
    }
}
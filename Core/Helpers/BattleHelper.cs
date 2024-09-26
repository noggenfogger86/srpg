using Model;

namespace Core.Helpers
{
    public static class BattleHelper
    {
        // 전투를 시작하는 헬퍼 함수 (팀 구성 이후 호출 가능)
        public static void Print(Battle battle)
        {
            System.Console.WriteLine("Battle has started!");
            battle.PrintBattleTeams();  // 각 팀의 캐릭터 정보 출력
        }

        public static void AutoBattle(Battle battle)
        {
            System.Console.WriteLine("Battle has started!");
            battle.PrintBattleTeams();  // 각 팀의 캐릭터 정보 출력

            // 전투가 끝날 때까지 반복
            int i = 0;
            while (!battle.IsBattleEnd() && i++ < 20)  // 최대 100 턴 
            {
                // 각 팀의 턴을 진행
                foreach (var team in battle.TeamList.Values)
                {
                    System.Console.WriteLine($"Team {team.TeamId}'s turn.");
                    // 살아있는 캐릭터들의 턴을 진행
                    for (int j = 0; j < team.Alive.Count; j++)
                    {
                        var character = team.Alive[j];
                        var cells = battle.Field.Cells;
                        BehaviorTree.ExecuteTurn(ref character, ref cells, Model.Enum.RangeType.FourDirections, 1);
                        battle.Field.Cells = cells;
                        System.Console.WriteLine($"Character {character.Character.Name}'s turn.");
                    }
                }
            }

            System.Console.WriteLine("Battle has ended!");
        }
    }
}
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class BehaviorTree
    {
        // 캐릭터의 행동 트리 처리
        public static void ExecuteTurn(ref FieldCharacter character, ref Cell[,] field, Model.Enum.RangeType rangeType, int range)
        {
            range = 10;
            // 1. 적 캐릭터 인식
            (int x, int y) = Core.Helpers.Action.Attack.Search(character, field, rangeType, range);
            
            if (x < 0 || y < 0 || field[x, y].Occupant == null)
            {
                return;
            }

            var nearestEnemy = field[x, y].Occupant ?? new FieldCharacter();

            // 2. 이동 판단
            if (x >= 0 && y >= 0)
            {
                int distanceToEnemy = CalculateDistance(character, nearestEnemy);

                if (distanceToEnemy <= range)
                {
                    // 2.1 공격 가능한 거리라면 이동하지 않음
                    Console.WriteLine($"{character.Character.Name} stays in place, enemy is within attack range.");
                }
                else
                {
                    // 2.2, 2.3: 적이 너무 가까우면 멀리 이동, 멀리 있다면 가까운 지점으로 이동
                    MoveCharacterTowardOrAway(character, nearestEnemy, field, range);
                }
            }

            // 3. 공격 판단
            if (character.CurrentAP >= Model.Const.Action.AP_ATTACK)
            {
                Core.Helpers.Action.Attack.Normal(ref character, ref nearestEnemy);
            }
            else
            {
                Console.WriteLine($"{character.Character.Name} switches to defense mode.");
                Core.Helpers.Action.Defend(ref character);  // 방어 모드 전환
            }

            // 4. 이동 가능한 필드가 없으면 이동 포기
            if (!HasMovableFields(character, field))
            {
                Console.WriteLine($"{character.Character.Name} has no available moves.");
            }

            field[x, y].Occupant = nearestEnemy;
        }

        // 적과의 거리 계산
        private static int CalculateDistance(FieldCharacter character, FieldCharacter enemy)
        {
            return Math.Abs(character.X - enemy.X) + Math.Abs(character.Y - enemy.Y);
        }

        // 이동 로직 (가까운 적에게로 이동하거나 멀리 이동)
        private static void MoveCharacterTowardOrAway(FieldCharacter character, FieldCharacter enemy, Cell[,] field, int range)
        {
            int distanceToEnemy = CalculateDistance(character, enemy);
            if (distanceToEnemy > range)
            {
                Console.WriteLine($"{character.Character.Name} moves closer to the enemy.");
                // 간단한 이동 로직: 적에게 한 칸씩 다가가거나 반대로 이동
                // 이 부분에서 이동 경로를 계산해서 이동하면 됨.
            }
            else
            {
                Console.WriteLine($"{character.Character.Name} moves away from the enemy.");
                // 적과 반대 방향으로 이동 (필드 경계 검사 필요)
            }
        }

        // 이동 가능한 필드가 있는지 확인 (행동 종료 여부)
        private static bool HasMovableFields(FieldCharacter character, Cell[,] field)
        {
            // 현재 캐릭터의 이동력을 기준으로 이동 가능한 셀이 있는지 확인
            // 간단히 주변 셀을 체크하거나 남은 MoveAP를 고려해 구현
            return character.MoveAP > 0;
        }
    }
}

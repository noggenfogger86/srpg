using System;
using Model;
using Model.Const;
using Model.Enum;
namespace Core.Helpers
{
    public static partial class Action
    {
        public static class Attack {
            public static (int x, int y) Search(FieldCharacter attacker, Cell[,] field, RangeType rangeType = RangeType.FourDirections, int range = 1)
            {
                // 공격 범위에 따른 방향 배열 설정
                int[,] directions = rangeType switch
                {
                    Model.Enum.RangeType.SkipOneTile => new int[,] { { 0, 2 }, { 0, -2 }, { -2, 0 }, { 2, 0 } },// 한 칸 건너뜀
                    Model.Enum.RangeType.DiagonalDirections => new int[,] { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } },// 사선
                    _ => new int[,] { { 0, 1 }, { 0, -1 }, { -1, 0 }, { 1, 0 } },// 상, 하, 좌, 우
                };

                // 탐색 범위 내에서 적을 찾음
                for (int i = 0; i < directions.GetLength(0); i++)
                {
                    for (int dist = 1; dist <= range; dist++)  // 범위만큼 탐색
                    {
                        int newX = attacker.X + directions[i, 0] * dist;
                        int newY = attacker.Y + directions[i, 1] * dist;

                        // 필드 경계 검사
                        if (newX >= 0 && newX < field.GetLength(0) && newY >= 0 && newY < field.GetLength(1))
                        {
                            // 해당 셀이 비어있지 않고 적 캐릭터가 있으면 반환
                            if (field[newX, newY].IsOccupied && field[newX, newY].Occupant != null)
                            {
                                var target = field[newX, newY].Occupant;

                                // 적 캐릭터를 찾았으면 반환
                                if (attacker.UserId != target?.UserId)  // 다른 팀인지 확인
                                {
                                    return (newX, newY);
                                }
                            }
                        }
                    }
                }

                // 적 캐릭터를 찾지 못한 경우 null 반환
                return (-1, -1);
            }

            // 공격 처리 함수
            public static void Normal(ref FieldCharacter attacker, ref FieldCharacter target)
            {
                // 공격력에서 방어력을 뺀 값으로 데미지 계산 (최소 1)
                int damage = Math.Max(1, attacker.Character.Atk - target.Character.Def);

                // 피격자의 체력에서 데미지 차감
                target.Character.Hp -= damage;

                // 피격자의 체력이 0 이하가 되면 사망 처리
                if (target.Character.Hp <= 0)
                {
                    target.Character.Hp = 0;
                    Console.WriteLine($"{target.Character.Name} has died.");
                }

                // 공격 결과 출력
                Console.WriteLine($"{attacker.Character.Name} attacked {target.Character.Name} for {damage} damage. {target.Character.Name}'s HP is now {target.Character.Hp}.");
            }
        }

    }
}
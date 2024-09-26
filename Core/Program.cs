using System;
using Model;
using Model.Enum;

namespace Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 필드와 캐릭터 초기화
            Character attackerChar = new("001", "Warrior", CharacterClass.Warrior, 1, 100);
            Character targetChar = new("002", "Rogue", CharacterClass.Rogue, 1, 100);

            FieldCharacter attacker = new(attackerChar, 5, 5, userId: 1);
            FieldCharacter target = new(targetChar, 5, 6);

            Cell[,] cells = new Cell[10, 10];
            cells[5, 5].IsOccupied = true;
            cells[5, 5].Occupant = attacker;
            cells[5, 6].IsOccupied = true;
            cells[5, 6].Occupant = target;

            // 턴 실행
            BehaviorTree.ExecuteTurn(ref attacker, ref cells, RangeType.FourDirections, 1);
            cells[5, 5].Occupant = attacker;

            // // 적 캐릭터 탐색
            // var (x, y) = Core.Helpers.Action.Attack.Search(attacker, cells, RangeType.FourDirections, 3);
            // if (x < 0 && y < 0)
            // {
            //     Console.WriteLine("No enemies found nearby.");
            // }
            // else
            // {
            //     FieldCharacter enemy = cells[x, y].Occupant ?? new FieldCharacter();
            //     // 공격 실행
            //     Core.Helpers.Action.Consume(ref attacker);
            //     Core.Helpers.Action.Attack.Normal(ref attacker, ref enemy);
            //     cells[x, y].Occupant = enemy;
            // }
        }
    }
}

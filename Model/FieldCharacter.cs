using System;
using System.Collections.Generic;
using System.Text;
using Model.Enum;

namespace Model
{
    public struct FieldCharacter
    {
        public Character Character;  // 캐릭터 정보

        public long UserId;  // 플레이어 캐릭터인지 여부

        // 좌표 관련 ; 유니티 좌표계를 따름
        public int X;  // 가로 좌표
        public int Y;  // 전후 좌표
        public int Z;  // 높이 좌표

        public readonly bool IsPlayer => UserId != 0;  // 플레이어 캐릭터인지 여부

        // 행동력 관련
        public int CurrentAP;   // 현재 남아있는 행동력
        public int TurnPoint;  // 누적 행동력 (다음 행동 순서에 영향을 줌)

        // 이동력 관련
        public int MoveAP;      // 이동력
        public int MaxMoveAP;   // 최대 이동력

        public bool IsDead; // 캐릭터 사망 여부

        public FieldCharacter(Character character, int x, int y, int z = 0, long userId = 0, bool isDead = false)
        {
            Character = character;
            X = x;
            Y = y;
            Z = z;
            UserId = userId;

            CurrentAP = 0;  // 기본값으로 설정
            TurnPoint = 0;  // 기본값으로 설정

            // 이동 포인트 초기화
            MaxMoveAP = 0;
            MoveAP = 0;

            IsDead = isDead;
        }

        public static void Print(FieldCharacter fieldCharacter)
        {
            Console.WriteLine("=== Field Character Info ===");
            Console.WriteLine($"Position: X = {fieldCharacter.X}, Y = {fieldCharacter.Y}, Z = {fieldCharacter.Z}");
            Console.WriteLine($"User ID: {fieldCharacter.UserId}");
            Console.WriteLine(fieldCharacter.IsPlayer ? "This is a player-controlled character." : "This is an NPC character.");

            Console.WriteLine($"Current AP: {fieldCharacter.CurrentAP}");
            Console.WriteLine($"TurnPoint: {fieldCharacter.TurnPoint}");

            Console.WriteLine($"Move AP: {fieldCharacter.MoveAP}");
            Console.WriteLine($"Max Move AP: {fieldCharacter.MaxMoveAP}");

            // Character의 Print 함수 호출
            Model.Character.Print(fieldCharacter.Character);
        }
    }
}

/*
Unity3D의 좌표계에 맵핑하기위해 아래의 함수들을 사용합니다.
public static Vector3 ToUnity(int x, int y, int z)
{
    // 코어의 X는 Unity의 X, 코어의 Z는 Unity의 Y, 코어의 Y는 Unity의 Z
    return new Vector3(x, z, y);
}

public static (int x, int y, int z) ToGrid(Vector3 pos)
{
    // Unity의 X는 코어의 X, Unity의 Z는 코어의 Y, Unity의 Y는 코어의 Z
    return ((int)pos.x, (int)pos.z, (int)pos.y);
}
*/
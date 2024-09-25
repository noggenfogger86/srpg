using System;
using System.Collections.Generic;
using System.Text;
using Model.Enum;

namespace Model
{
    public struct FieldCharacter {
        public Character Character;  // 캐릭터 정보
        public int X;  // X 좌표
        public int Y;  // Y 좌표
        public int Z;  // 높이 정보 (선택적)
        public long UserId;  // 플레이어 캐릭터인지 여부
        public readonly bool IsPlayer => UserId != 0;  // 플레이어 캐릭터인지 여부

        // 행동력 관련
        public int CurrentAP;   // 현재 남아있는 행동력
        public int AccumulatedAP;  // 누적 행동력 (다음 행동 순서에 영향을 줌)

        // 이동력 관련
        public int MoveAP;      // 이동력
        public int MaxMoveAP;   // 최대 이동력

        public FieldCharacter(Character character, int x, int y, int z = 0, long userId = 0)
        {
            Character = character;
            X = x;
            Y = y;
            Z = z;
            UserId = userId;

            CurrentAP = 0;  // 기본값으로 설정
            AccumulatedAP = 0;  // 기본값으로 설정

            // 이동 포인트 초기화
            MaxMoveAP = 0;
            MoveAP = 0;
        }
    }
}
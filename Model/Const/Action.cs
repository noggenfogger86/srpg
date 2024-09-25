using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Const
{
    public readonly struct Action
    {
        // 전투와 관련된 상수를 정의합니다.
        public const int AP_END_TURN = 10;  // 턴 종료 시 소모 AP
        public const int AP_MOVE = 3;  // 한 칸 이동 시 소모 AP
        public const int AP_ATTACK = 5;  // 공격 시 소모 AP
        public const int AP_DEFENSE = 2;  // 방어 모드 전환 시 소모 AP
    }
}

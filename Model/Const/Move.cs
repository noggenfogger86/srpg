using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Const
{
    public readonly struct Move 
    {
        public const int AP_MOVE_MAX = 10;      // 최대 이동 포인트
        public const int AP_MOVE_PLAIN = 1;     // 땅에서 한 칸 이동 AP
        public const int AP_MOVE_MOUNTAIN = 5;  // 산에서 한 칸 이동 AP
        public const int AP_MOVE_WATER = 4;     // 물에서 한 칸 이동 AP
        public const int AP_MOVE_FOREST = 2;    // 숲에서 한 칸 이동 AP
        public const int AP_MOVE_SWAMP = 3;     // 늪에서 한 칸 이동 AP
    }
}


using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Enum
{
    public enum RangeType
    {
        FourDirections,      // 상, 하, 좌, 우
        SkipOneTile,         // 한 칸 건너뛰기
        DiagonalDirections   // 사선 방향
    }
}
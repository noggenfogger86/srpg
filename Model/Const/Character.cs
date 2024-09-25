using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Const
{
    public readonly struct Character
    {
        // 기본값 상수를 정의합니다.
        public const int DEFAULT_LEVEL = 1;
        public const int DEFAULT_EXP = 0;
        public const int DEFAULT_HP = 100;
        public const int DEFAULT_MP = 100;
        public const int DEFAULT_ATK = 10;
        public const int DEFAULT_DEF = 10;
        public const int DEFAULT_RES = 10;
        public const int DEFAULT_DODGE = 10;
        public const int DEFAULT_CRITICAL = 10;
        public const int DEFAULT_SPEED = 10;
        public const int MIN_LEVEL = 1;
        public const int MIN_EXP = 0;
        public const int MIN_HP = 0;
        public const int MIN_MP = 0;
        public const int MIN_ATK = 0;
        public const int MIN_DEF = 0;
        public const int MIN_RES = 0;
        public const int MIN_DODGE = 0;
        public const int MIN_CRITICAL = 0;
        public const int MIN_SPEED = 0;
        
        // 추후에 확장될 수 있는 변수들은 static readonly 로 정의한다.
        public static readonly int MAX_LEVEL = 100;

        // 레벨별로 상이한 경험치를 설정할 수 있도록 하기 위해 MaxExp 상수를 제거합니다.
        // public const int MAX_EXP = 1000000;
        public static readonly int MAX_HP = 1000;
        public static readonly int MAX_MP = 1000;
        public static readonly int MAX_ATK = 100;
        public static readonly int MAX_DEF = 100;
        public static readonly int MAX_RES = 100;

        // 확률의 최대값은 Common.PROB_MAX 상수를 사용합니다.
        // public const int MAX_DODGE = 10000;
        // public const int MAX_CRITICAL = 10000;
        // public const int MAX_SPEED = 10000;
    }
}

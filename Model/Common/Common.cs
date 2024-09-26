using System;

namespace Model.Common
{

    // Common 은 프로젝트를 분리해야 할 것 같음.
    public static class Common
    {
        public static int GetRandom(int min, int max)
        {
            return new Random().Next(min, max);
        }
        public static string GetEnumName<T>(T enumValue) where T : System.Enum
        {
            return enumValue.ToString();  // Enum 변수명을 문자열로 반환
        }
    }
}
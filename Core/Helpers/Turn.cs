using Model;

namespace Core.Helpers
{
    public static class Turn
    {
        // 턴 종료 시 행동력(AP)와 이동 포인트 처리
        public static void EndTurn(ref FieldCharacter fieldCharacter)
        {
            // 행동력 관련 처리
            Helpers.Action.EndTurn(ref fieldCharacter);
            // 이동 포인트 처리
            Helpers.Move.EndTurn(ref fieldCharacter);
        }
    }
}
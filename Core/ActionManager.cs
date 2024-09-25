using Model;
using Model.Const;

namespace Core
{
    public static class ActionManager
    {
        // AP 소모 메서드 (Core 로직)
        public static void ConsumeAP(ref FieldCharacter fieldCharacter, int ap)
        {
            fieldCharacter.CurrentAP -= ap;
            fieldCharacter.AccumulatedAP += ap;
        }

        // 턴 종료 처리
        public static void EndTurn(ref FieldCharacter fieldCharacter)
        {
            ConsumeAP(ref fieldCharacter, Action.AP_END_TURN);
        }

        // 이동 처리
        public static void Move(ref FieldCharacter fieldCharacter, int distance)
        {
            int totalAP = distance * Action.AP_MOVE;
            ConsumeAP(ref fieldCharacter, totalAP);
        }

        // 공격 처리
        public static void Attack(ref FieldCharacter fieldCharacter)
        {
            ConsumeAP(ref fieldCharacter, Action.AP_ATTACK);
        }

        // 방어 모드 전환 처리
        public static void Defend(ref FieldCharacter fieldCharacter)
        {
            ConsumeAP(ref fieldCharacter, Action.AP_DEFENSE);
        }
    }
}

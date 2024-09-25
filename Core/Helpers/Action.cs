using Model;
using Model.Const;

namespace Core.Helpers
{
    public static class Action
    {
        // AP 소모 로직
        public static void ConsumeAP(ref FieldCharacter fieldCharacter, int ap)
        {
            fieldCharacter.CurrentAP -= ap;
            fieldCharacter.AccumulatedAP += ap;
        }

        // 턴 종료 시 AP 소모 처리
        public static void EndTurn(ref FieldCharacter fieldCharacter)
        {
            // 이동 포인트 처리
            ConsumeAP(ref fieldCharacter, Model.Const.Action.AP_END_TURN);
        }

        // 공격 처리
        public static void Attack(ref FieldCharacter fieldCharacter)
        {
            ConsumeAP(ref fieldCharacter, Model.Const.Action.AP_ATTACK);
        }

        // 방어 모드 전환 처리
        public static void Defend(ref FieldCharacter fieldCharacter)
        {
            ConsumeAP(ref fieldCharacter, Model.Const.Action.AP_DEFENSE);
        }
    }

}
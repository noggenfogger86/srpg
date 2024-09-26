using Model;
using Model.Const;

namespace Core.Helpers
{
    public static partial class Action
    {
        // AP 소모 로직
        private static void ConsumeAP(ref FieldCharacter actor, int ap)
        {
            actor.CurrentAP -= ap;
            actor.TurnPoint += ap;
        }

        // 턴 종료 시 AP 소모 처리
        public static void EndTurn(ref FieldCharacter actor)
        {
            // 이동 포인트 처리
            ConsumeAP(ref actor, Model.Const.Action.AP_END_TURN);
        }

        // 공격 처리
        public static void Consume(ref FieldCharacter actor, int spellId = 0)
        {
            int cost = Model.Const.Action.AP_ATTACK; //spellId == 0 ? Model.Const.Action.AP_ATTACK : cost;
            ConsumeAP(ref actor, cost);
        }

        // 방어 모드 전환 처리
        public static void Defend(ref FieldCharacter actor)
        {
            ConsumeAP(ref actor, Model.Const.Action.AP_DEFENSE);
        }

        public static void Initialize(ref FieldCharacter actor)
        {
            var character = actor.Character;
            actor.CurrentAP = (character.Str + character.Agi + character.Int) / 5;
        }
    }
}
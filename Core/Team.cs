using System.Collections.Generic;
using Model;

namespace Core
{
    public class Team
    {
        // 팀에 속한 캐릭터 목록
        public List<Model.FieldCharacter> Alive { get; set; }
        public List<Model.FieldCharacter> Dead { get; set; }

        // 팀 아이디 (기본 값은 UserId를 사용하도록 함)
        public long TeamId { get; set; }

        // 생성자
        public Team(long teamId)
        {
            TeamId = teamId;
            Alive = new List<Model.FieldCharacter>();
            Dead = new List<Model.FieldCharacter>();
        }

        // 팀에 캐릭터 추가
        public void AddCharacter(Model.FieldCharacter character)
        {
            Alive.Add(character);
        }

        // 팀의 캐릭터 목록 출력 (테스트용)
        public void PrintTeamInfo()
        {
            System.Console.WriteLine($"Team for User ID: {TeamId}");
            foreach (var fieldCharacter in Alive)
            {
                // 캐릭터 정보 출력
                Model.FieldCharacter.Print(fieldCharacter);
            }
        }

        public void Die(ref FieldCharacter fieldCharacter) {
            Alive.Remove(fieldCharacter);
            Dead.Add(fieldCharacter);
        }

        public void Revive(ref FieldCharacter fieldCharacter) {
            Dead.Remove(fieldCharacter);
            Alive.Add(fieldCharacter);
        }

        public bool IsAllDead() {
            return Alive.Count == 0;
        }
    }
}
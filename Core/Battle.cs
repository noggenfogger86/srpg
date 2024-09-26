using Model;
using System;
using System.Collections.Generic;

namespace Core
{
    public class Battle
    {
        // 팀 목록, UserId를 키로 사용
        public Dictionary<long, Team> TeamList;
        public Field Field;

        // 생성자
        public Battle()
        {
            TeamList = new Dictionary<long, Team>();
        }

        // 팀 추가
        public void AddTeam(Team team)
        {
            if (!TeamList.ContainsKey(team.TeamId))
            {
                TeamList.Add(team.TeamId, team);
            }
        }

        // 팀 정보 출력 (테스트용)
        public void PrintBattleTeams()
        {
            foreach (var team in TeamList.Values)
            {
                team.PrintTeamInfo();
            }
        }

        // 전투 종료 여부 확인
        internal bool IsBattleEnd()
        {
            var e = TeamList.Values.GetEnumerator();
            int teamCount = TeamList.Values.Count;
            while (e.MoveNext())
            {
                teamCount = e.Current.IsAllDead() ? teamCount - 1 : teamCount;
            }
            return teamCount <= 1;
        }

        // 승자 팀 반환 ; null이면 아직 승자가 없음(Draw)
        public Team? GetWinner()
        {
            var e = TeamList.Values.GetEnumerator();
            while (e.MoveNext())
            {
                if (!e.Current.IsAllDead())
                {
                    return e.Current;
                }
            }
            return null;
        }
    }
}
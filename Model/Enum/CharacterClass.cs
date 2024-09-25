using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Enum
{
    public enum CharacterClass
    {
        // 전사 계열
        Warrior,        // 전사
        Berserker,      // 광전사
        Knight,         // 기사
        Paladin,        // 성기사

        // 마법사 계열
        Mage,           // 마법사
        Sorcerer,       // 흑마법사
        Wizard,         // 마도사

        // 도적 계열
        Rogue,          // 도적
        Assassin,       // 암살자
        Ranger,         // 레인저
        Thief,          // 도둑

        // 탱커 계열
        Guardian,       // 수호자
        Crusader,       // 성전사
        Defender,       // 방어자

        // 지원 계열
        Healer,         // 치유사
        Cleric,         // 성직자
        Druid,          // 드루이드
        Bard,           // 음유시인

        // 특수 클래스
        Necromancer,    // 강령술사
        Warlock,        // 흑기사
        Monk,           // 수도승
        Summoner,       // 소환사
        Alchemist       // 연금술사
    }
}

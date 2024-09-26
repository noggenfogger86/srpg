using System;
using System.Collections.Generic;
using System.Text;
using Model.Enum;

namespace Model
{
    public readonly struct EnumCount
    {
        public static readonly int ElementalCount = System.Enum.GetValues(typeof(Elemental)).Length;
        public static readonly int EffectCount = System.Enum.GetValues(typeof(Effect)).Length;
        public static readonly int ItemSlotCount = System.Enum.GetValues(typeof(ItemSlot)).Length;
    }
    
    public struct Character
    {
        public string Id;   // 고유코드 (외형)
        public string Name; // 이름
        public CharacterClass ClassType;    // 직업
        public int Lv;      // 레벨
        public int Exp;     // 경험치

        // 1차 스테이터스
        public int Str;  // 힘
        public int Agi;  // 민첩
        public int Vit;  // 체력
        public int Int;  // 지능
        public int Lck;  // 운

        public int Hp;      // 체력
        public int Mp;      // 마력
        public int Atk;     // 공격력
        public int Def;     // 방어력
        public int Dodge;   // 회피력

        // 2차 스테이터스 (속성별 공격력 및 저항력). Elemental(속성) Enum 참조
        public ElementalStatus[] ElementalStatus;

        // 효과 저항력 Effect(효과) Enum 참조
        public int[] EffectRes;

        // 효과 상태 (효과 종류별 레벨 및 지속시간). Effect(효과) Enum 참조
        public EffectStatus[] EffectStatus;

        // 장착 아이템 정보. ItemSlot Enum 참조
        public Item[] Equip;

        public Character(string id, string name, CharacterClass classType, int lv, int exp)
        {
            Id = id;
            Name = name;
            ClassType = classType;
            Lv = lv;
            Exp = exp;

            Str = 10;
            Agi = 10;
            Vit = 10;
            Int = 10;
            Lck = 10;

            Hp = 100;
            Mp = 100;
            Atk = 10;
            Def = 10;
            Dodge = 5;

            // 배열 초기화
            ElementalStatus = new ElementalStatus[EnumCount.ElementalCount];
            EffectRes = new int[EnumCount.EffectCount];
            EffectStatus = new EffectStatus[EnumCount.EffectCount];
            Equip = new Item[EnumCount.ItemSlotCount];
        }
    }
}

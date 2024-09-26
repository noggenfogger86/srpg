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
    
    public class Character
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

        public Character(string id, string name, CharacterClass classType, int lv, int exp = 0)
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

        public static void Print(Character character)
        {
            System.Console.WriteLine("====== Character Info ======");
            System.Console.WriteLine($"ID: {character.Id}");
            System.Console.WriteLine($"Name: {character.Name}");
            System.Console.WriteLine($"Class: {character.ClassType}");
            System.Console.WriteLine($"Level: {character.Lv}");
            System.Console.WriteLine($"Exp: {character.Exp}");

            // 1차 스탯 출력
            System.Console.WriteLine("------ Primary Stats ------");
            System.Console.WriteLine($"Str: {character.Str}");
            System.Console.WriteLine($"Agi: {character.Agi}");
            System.Console.WriteLine($"Vit: {character.Vit}");
            System.Console.WriteLine($"Int: {character.Int}");
            System.Console.WriteLine($"Lck: {character.Lck}");

            // 2차 스탯 출력
            System.Console.WriteLine("------ Secondary Stats ------");
            System.Console.WriteLine($"HP: {character.Hp}");
            System.Console.WriteLine($"MP: {character.Mp}");
            System.Console.WriteLine($"Atk: {character.Atk}");
            System.Console.WriteLine($"Def: {character.Def}");
            System.Console.WriteLine($"Dodge: {character.Dodge}");

            System.Console.WriteLine("------ Elemental Status ------");
            for (int i = 0; i < character.ElementalStatus.Length; i++)
            {
                System.Console.WriteLine($"Element {((Model.Enum.Elemental)i).ToString()}: Attack Power: {character.ElementalStatus[i].Atk}, Resistance: {character.ElementalStatus[i].Res}, Dodge: {character.ElementalStatus[i].Dodge}");
            }

            System.Console.WriteLine("------ Effect Resistances ------");
            for (int i = 0; i < character.EffectRes.Length; i++)
            {
                System.Console.WriteLine($"Effect {((Model.Enum.Effect)i).ToString()} Resistance: {character.EffectRes[i]}");
            }

            System.Console.WriteLine("------ Effect Status ------");
            for (int i = 0; i < character.EffectStatus.Length; i++)
            {
                System.Console.WriteLine($"Effect {((Model.Enum.Effect)i).ToString()}: Level: {character.EffectStatus[i].Lv}, Duration: {character.EffectStatus[i].Duration}");
            }

            System.Console.WriteLine("------ Equipment ------");
            for (int i = 0; i < character.Equip.Length; i++)
            {
                System.Console.WriteLine($"Equipment {((Model.Enum.ItemSlot)i).ToString()}: {character.Equip[i].Id}");  // assuming Item has a Name property
            }

            System.Console.WriteLine("===============================");
        }
    }
}

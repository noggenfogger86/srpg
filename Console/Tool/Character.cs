// See https://aka.ms/new-console-template for more information
using Model;
namespace Console.Tool
{
    public static class Character
    {
        private static readonly Random random = new Random();

        // 캐릭터 클래스별로 스탯 범위를 지정하여 임의의 캐릭터 생성
        public static Model.Character Generate(Model.Enum.CharacterClass classType)
        {
            string id = $"ID_{random.Next(1000, 9999)}";  // 임의의 ID 생성
            string name = $"Dummy_{classType}_{random.Next(1000, 9999)}";  // 임의의 이름 생성
            int lv = random.Next(1, 10);  // 레벨은 1~10 사이로 랜덤
            int exp = random.Next(0, 1000);  // 경험치는 0~1000 사이

            // 캐릭터 생성
            Model.Character character = new Model.Character(id, name, classType, lv, exp);

            // 클래스별 스탯 범위
            switch (classType)
            {
                case Model.Enum.CharacterClass.Warrior:
                    character.Str = random.Next(15, 25);
                    character.Agi = random.Next(8, 15);
                    character.Vit = random.Next(12, 20);
                    character.Int = random.Next(5, 10);
                    character.Lck = random.Next(8, 12);
                    break;
                case Model.Enum.CharacterClass.Mage:
                    character.Str = random.Next(5, 10);
                    character.Agi = random.Next(8, 12);
                    character.Vit = random.Next(7, 12);
                    character.Int = random.Next(15, 25);
                    character.Lck = random.Next(10, 15);
                    break;
                case Model.Enum.CharacterClass.Rogue:
                    character.Str = random.Next(8, 12);
                    character.Agi = random.Next(15, 25);
                    character.Vit = random.Next(10, 15);
                    character.Int = random.Next(5, 10);
                    character.Lck = random.Next(10, 20);
                    break;
                default:
                    System.Console.WriteLine("Unknown class. Using default stats.");
                    break;
            }

            // 2차 스테이터스는 기본값으로 설정
            character.Hp = character.Vit * 10;
            character.Mp = character.Int * 10;
            character.Atk = character.Str * 2;
            character.Def = character.Vit * 2;
            character.Dodge = character.Agi / 2;

            // ElementalStatus, EffectRes, EffectStatus, Equip는 배열 초기화
            for (int i = 0; i < character.ElementalStatus.Length; i++)
            {
                character.ElementalStatus[i] = new ElementalStatus { Atk = 0, Res = 0, Dodge = 0 };
            }
            for (int i = 0; i < character.EffectRes.Length; i++)
            {
                character.EffectRes[i] = 0;
            }
            for (int i = 0; i < character.EffectStatus.Length; i++)
            {
                character.EffectStatus[i] = new EffectStatus { Lv = 0, Duration = 0 };
            }
            for (int i = 0; i < character.Equip.Length; i++)
            {
                character.Equip[i] = new Item();  // 아이템 초기화 (비어있는 상태)
            }

            return character;
        }

        // 캐릭터를 파일로 저장
        public static string Save(Model.Character character)
        {
            string filename = $"{DateTime.Now:yyyyMMddHHmmss}_{character.ClassType}.char";
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(character.Id);
                writer.WriteLine(character.Name);
                writer.WriteLine(character.ClassType);
                writer.WriteLine(character.Lv);
                writer.WriteLine(character.Exp);
                writer.WriteLine(character.Str);
                writer.WriteLine(character.Agi);
                writer.WriteLine(character.Vit);
                writer.WriteLine(character.Int);
                writer.WriteLine(character.Lck);
                writer.WriteLine(character.Hp);
                writer.WriteLine(character.Mp);
                writer.WriteLine(character.Atk);
                writer.WriteLine(character.Def);
                writer.WriteLine(character.Dodge);
            }

            System.Console.WriteLine($"Character saved to {filename}");

            return filename;
        }

        // 특정 디렉토리에서 *.char 파일을 읽어 캐릭터 리스트로 반환
        public static List<Model.Character> LoadAll(string directoryPath)
        {
            var characterList = new List<Model.Character>();

            if (!Directory.Exists(directoryPath))
            {
                System.Console.WriteLine("Directory not found.");
                return characterList;
            }

            string[] charFiles = Directory.GetFiles(directoryPath, "*.char");

            foreach (var file in charFiles)
            {
                // IDE0063: 간단한 using 문 사용 (C# 8.0 부터 지원)
                using StreamReader reader = new StreamReader(file);
                string id = reader.ReadLine();
                string name = reader.ReadLine();
                Model.Enum.CharacterClass classType = (Model.Enum.CharacterClass)Enum.Parse(typeof(Model.Enum.CharacterClass), reader.ReadLine());
                int lv = int.Parse(reader.ReadLine());
                int exp = int.Parse(reader.ReadLine());
                int str = int.Parse(reader.ReadLine());
                int agi = int.Parse(reader.ReadLine());
                int vit = int.Parse(reader.ReadLine());
                int intl = int.Parse(reader.ReadLine());
                int lck = int.Parse(reader.ReadLine());
                int hp = int.Parse(reader.ReadLine());
                int mp = int.Parse(reader.ReadLine());
                int atk = int.Parse(reader.ReadLine());
                int def = int.Parse(reader.ReadLine());
                int dodge = int.Parse(reader.ReadLine());

                var character = new Model.Character(id, name, classType, lv, exp)
                {
                    Str = str,
                    Agi = agi,
                    Vit = vit,
                    Int = intl,
                    Lck = lck,
                    Hp = hp,
                    Mp = mp,
                    Atk = atk,
                    Def = def,
                    Dodge = dodge
                };

                characterList.Add(character);
            }

            System.Console.WriteLine($"{characterList.Count} character(s) loaded from {directoryPath}");
            return characterList;
        }
        public static void Print(Model.Character character)
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

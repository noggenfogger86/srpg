using System;
using System.Collections.Generic;
using System.Text;
using Model.Enum;

namespace Model
{
    public struct Cell
    {
        public int X;  // X 좌표
        public int Y;  // Y 좌표
        public int Z;  // 높이 정보 (선택적)
        public Terrain Terrain;  // 지형 타입 (Enum 사용)
        public bool IsOccupied;  // 캐릭터나 장애물이 있는지 여부
        public Item[] Item;  // 아이템 또는 오브젝트

        public Cell(int x, int y, Terrain terrain, int z=0, bool isOccupied = false)
        {
            X = x;
            Y = y;
            Z = z;
            Terrain = terrain;
            IsOccupied = isOccupied;  // 기본적으로 캐릭터는 없음
            Item = new Item[0];  // 기본적으로 아이템은 없음
        }

        // 캐릭터가 이 셀에 진입할 수 있는지 여부를 판단하는 메서드
        public readonly bool CanEnter()
        {
            return !IsOccupied;  // 캐릭터가 없으면 진입 가능
        }
    }
}

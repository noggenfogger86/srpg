using System;
using System.Collections.Generic;
using System.Text;
using Model.Enum;

namespace Model
{
    public struct Field
    {
        public int Width;  // 필드의 가로 크기 (X축)
        public int Height;  // 필드의 세로 크기 (Y축)
        public Cell[,] Cells;  // 셀 배열

        public Field(int width, int height, Cell[,] cells = null)
        {
            Width = width;
            Height = height;
            if (cells != null)
            {
                Cells = cells;
            }
            else
            {
                Cells = new Cell[width, height];  // 셀 배열 초기화

                // 각 셀에 대해 기본적으로 초기화
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Cells[x, y] = new Cell(x, y, Terrain.Plain);  // 기본적으로 평지로 설정
                    }
                }
            }
        }

        // 특정 좌표의 셀 가져오기
        public readonly Cell GetCell(int x, int y)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                return Cells[x, y];
            }
            throw new ArgumentOutOfRangeException("잘못된 좌표입니다.");
        }

        // 특정 좌표의 셀에 캐릭터 배치하기
        public readonly void PlaceCharacter(int x, int y)
        {
            if (Cells[x, y].CanEnter())
            {
                Cells[x, y].IsOccupied = true;
            }
            else
            {
                throw new InvalidOperationException("이 셀에는 캐릭터를 배치할 수 없습니다.");
            }
        }

        // 특정 좌표의 셀에서 캐릭터 제거하기
        public readonly void RemoveCharacter(int x, int y)
        {
            if (Cells[x, y].IsOccupied)
            {
                Cells[x, y].IsOccupied = false;
            }
        }
    }
}

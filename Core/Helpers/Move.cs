using Model;
using System.Collections.Generic;

namespace Core.Helpers
{
    /**
        usage : 
        // 캐릭터가 (5, 5)에서 시작하고, 최대 이동 포인트가 10일 때
        var movableCells = Helpers.Move.GetMovableArea(grid, 5, 5, fieldCharacter.MoveAP);

        // 이동 가능한 셀 출력
        foreach (var cell in movableCells)
        {
            System.Console.WriteLine($"Movable Cell: ({cell.X}, {cell.Y})");
        }

        // 턴 종료 시 이동 포인트 처리
        Helpers.Move.EndTurnMoveAP(ref fieldCharacter);
    */
    public static class Move
    {
        // 4방향으로 움직일 때의 X, Y 좌표 변화
        private static readonly int[] Dx = { 0, 0, -1, 1 };  // 좌우
        private static readonly int[] Dy = { -1, 1, 0, 0 };  // 상하

        // 최대 이동 포인트 계산 (기본값 + (민첩 / 5), 최대값은 10)
        public static int CalcMaxMoveAP(Character character)
        {
            return System.Math.Min(Model.Const.Move.AP_MOVE_MAX, 3 + 1 + (character.Agi / 5));  // 기본값 1 + (민첩 / 5), 최대 10
        }

        // 사용 가능한 이동 포인트 계산 (기본값 + (민첩 / 5), 최대값은 10)
        public static int CalcMoveAP(Character character)
        {
            return System.Math.Min(Model.Const.Move.AP_MOVE_MAX, 1 + (character.Agi / 5));  // 기본값 1 + (민첩 / 5), 최대 10
        }

        // 터레인에 따른 이동 소모량 계산
        private static int CalcMoveCost(Model.Enum.Terrain terrain)
        {
            return terrain switch
            {
                Model.Enum.Terrain.Plain => Model.Const.Move.AP_MOVE_PLAIN,
                Model.Enum.Terrain.Mountain => Model.Const.Move.AP_MOVE_MOUNTAIN,
                Model.Enum.Terrain.Water => Model.Const.Move.AP_MOVE_WATER,
                Model.Enum.Terrain.Forest => Model.Const.Move.AP_MOVE_FOREST,
                Model.Enum.Terrain.Swamp => Model.Const.Move.AP_MOVE_SWAMP,
                _ => Model.Const.Move.AP_MOVE_PLAIN // 기본값은 평지
            };
        }

        // 이동 가능한 영역을 반환하는 함수
        public static List<Cell> GetMovableArea(Cell[,] grid, int startX, int startY, int maxMoveAP)
        {
            int size = grid.GetLength(0);  // 그리드 크기 (11x11)
            var visited = new bool[size, size];  // 방문한 셀 추적
            var result = new List<Cell>();  // 이동 가능한 셀 저장

            // BFS 탐색을 위한 큐
            var queue = new Queue<(int x, int y, int moveLeft)>();
            queue.Enqueue((startX, startY, maxMoveAP));  // 시작 위치와 남은 이동 포인트

            while (queue.Count > 0)
            {
                var (x, y, moveLeft) = queue.Dequeue();

                // 남은 이동 포인트가 없거나 이미 방문했으면 스킵
                if (moveLeft <= 0 || visited[x, y])
                {
                    continue;
                }

                visited[x, y] = true;  // 현재 셀을 방문으로 마크

                // 이동할 수 없는 셀은 스킵
                if (!grid[x, y].IsOccupied)
                {
                    result.Add(grid[x, y]);  // 이동 가능한 셀을 결과에 추가
                }

                // 4방향으로 이동 시도
                for (int i = 0; i < 4; i++)
                {
                    int newX = x + Dx[i];
                    int newY = y + Dy[i];

                    // 그리드 내에 있는지 확인
                    if (newX >= 0 && newX < size && newY >= 0 && newY < size && !visited[newX, newY])
                    {
                        // 이동하려는 셀의 터레인에 따른 이동 소모량 계산
                        int cost = CalcMoveCost(grid[newX, newY].Terrain);

                        // 남은 이동 포인트가 충분하면 큐에 추가
                        if (moveLeft >= cost)
                        {
                            queue.Enqueue((newX, newY, moveLeft - cost));
                        }
                    }
                }
            }

            return result;
        }

        // 턴 종료 시 이동 포인트 처리 (남은 포인트 절반 저장)
        public static void EndTurn(ref FieldCharacter fieldCharacter)
        {
            fieldCharacter.MoveAP = System.Math.Min(fieldCharacter.MaxMoveAP, fieldCharacter.MoveAP / 2);
        }

        // 인게임에서 이동 포인트를 초기화하는 메서드
        public static void Initialize(ref FieldCharacter fieldCharacter)
        {
            fieldCharacter.MaxMoveAP = CalcMaxMoveAP(fieldCharacter.Character);
            fieldCharacter.MoveAP = CalcMoveAP(fieldCharacter.Character);
        }
    }
}
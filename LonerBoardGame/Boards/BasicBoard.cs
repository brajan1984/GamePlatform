using GamePlatform.Api.Entities;
using LonerBoardGame.Boards.Interfaces;
using System;
using System.Collections.Generic;

namespace LonerBoardGame.Boards
{
    public class BasicBoard : IBasicBoard
    {
        private Guid _id;
        public IEnumerable<IBasicPolygon> Cells { get; private set; }

        public void CreateBoard()
        {
            _id = Guid.NewGuid();
            var cells = new List<BasicPolygon>();

            CreateSolidCells(cells);
            CreateFilledCells(cells);
            CreateEmptyCell(cells);

            Cells = cells;
        }

        private void CreateSolidCells(List<BasicPolygon> cellListToFill)
        {
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    if (((x >= 0 && x <= 1) && (y >= 0 && y <= 1)) ||
                        ((x >= 0 && x <= 1) && (y >= 5 && y <= 6)) ||
                        ((x >= 5 && x <= 6) && (y >= 0 && y <= 1)) ||
                        ((x >= 5 && x <= 6) && (y >= 5 && y <= 6)))
                    {
                        cellListToFill.Add(new BasicPolygon() { State = PolygonState.Solid, Coordintes = new Point3d() { X = x, Y = y } });
                    }
                }
            }
        }

        private void CreateFilledCells(List<BasicPolygon> cellListToFill)
        {
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    if (x == 3 && y == 3)
                    {
                        continue;
                    }

                    if ((x >= 0 && x <= 6 && y >= 2 && y <= 4))
                    {
                        cellListToFill.Add(new BasicPolygon() { State = PolygonState.Filled, Coordintes = new Point3d() { X = x, Y = y } });
                    }

                    if (x >= 2 && x <= 4 && (y >= 0 && y <= 1 || y >= 5 && y <= 6))
                    {
                        cellListToFill.Add(new BasicPolygon() { State = PolygonState.Filled, Coordintes = new Point3d() { X = x, Y = y } });
                    }
                }
            }
        }

        private void CreateEmptyCell(List<BasicPolygon> cellListToFill)
        {
            cellListToFill.Add(new BasicPolygon() { State = PolygonState.Empty, Coordintes = new Point3d() { X = 3, Y = 3 } });
        }
    }
}
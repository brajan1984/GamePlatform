using LonerBoardGame.Boards.Interfaces;
using System;
using System.Collections.Generic;

namespace LonerBoardGame.Boards
{
    public class BasicBoard : IBasicBoard
    {
        public List<IBasicPolygon> Cells { get; }
        
        public void CreateBoard()
        {
            throw new NotImplementedException();
        }
    }
}
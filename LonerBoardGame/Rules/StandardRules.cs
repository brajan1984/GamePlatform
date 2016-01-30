using GamePlatform.Api.Rulers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePlatform.Api.ModifierBus.Interfaces;
using LonerBoardGame.Boards.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using LonerBoardGame.Modifiers;
using LonerBoardGame.Games.Interfaces;
using GamePlatform.Api.Entities;
using GamePlatform.Api.Modifiers.Modifiers;

namespace LonerBoardGame.Rules
{
    public class StandardRules : IRules
    {
        private readonly Dictionary<Type, Func<IModifier, IScenario>> _judge = new Dictionary<Type, Func<IModifier, IScenario>>();
        private readonly ILonerGame _game;

        public StandardRules(ILonerGame game)
        {
            _game = game;

            _judge.Add(typeof(MakeMoveModifier), m => { return MoveScenario(m as MakeMoveModifier); });
        }

        private IBasicPolygon GetCell(Point3d coordinates)
        {
            return _game.Board.Cells.Where(c => c.Coordintes.X == coordinates.X &&
                                                    c.Coordintes.Y == coordinates.Y &&
                                                    c.Coordintes.Z == coordinates.Z)
                                                    .FirstOrDefault();
        }

        private IBasicPolygon GetMiddleCell(Point3d from, Point3d to)
        {
            return GetCell(new Point3d() { X = (from.X + to.X) / 2, Y = (from.Y + to.Y) / 2, Z = (from.Z + to.Z) / 2 });
        }

        private void CheckCellExists(Point3d coordinates)
        {
            var cell = GetCell(coordinates);

            if (cell == null)
            {
                throw new InvalidOperationException("Cell with coordinates {coordinates} not found.");
            }
        }

        private void CheckTargetCellIsSolid(Point3d coordinates)
        {
            var cell = GetCell(coordinates);

            if (cell.State == PolygonState.Solid)
            {
                throw new InvalidOperationException("Cell with coordinates {coordinates} is solid.");
            }
        }

        private void CheckCoordiantes(Point3d from, Point3d to)
        {
            var xDiff = Math.Abs(from.X - to.X);
            var yDiff = Math.Abs(from.Y - to.Y);

            if ((xDiff == 2 && yDiff == 0) || (yDiff == 2 && xDiff == 0))
            {
                return;
            }
            else
            {
                throw new InvalidOperationException("Can't move {from} -> {to}.");
            }
        }

        private void CheckMoveState(Point3d from, Point3d to)
        {
            var cellFrom = GetCell(from);
            var cellTo = GetCell(to);

            var middleCell = GetMiddleCell(from, to);

            if (middleCell == null || 
                middleCell.State != PolygonState.Filled || 
                cellFrom.State != PolygonState.Filled || 
                cellTo.State != PolygonState.Empty)
            {
                throw new InvalidOperationException("Can't move from {from}.");
            }
        }

        private void CheckMoveCoordiantes(Point3d from, Point3d to)
        {
            CheckCellExists(from);
            CheckCellExists(to);
            CheckTargetCellIsSolid(to);
            CheckCoordiantes(from, to);
            CheckMoveState(from, to);
        }

        private IScenario MoveScenario(MakeMoveModifier moveModifier)
        {
            CheckMoveCoordiantes(moveModifier.From, moveModifier.To);

            var cell = GetMiddleCell(moveModifier.From, moveModifier.To);

            var emptyModifier = new EmptyCellModifier(cell) { Source = this };

            var scenario = new SimpleScenario();

            scenario.Modifiers.Add(moveModifier);
            scenario.Modifiers.Add(emptyModifier);

            return scenario;
        }

        public IScenario Advise(IModifier modifier)
        {
            if (_judge.ContainsKey(modifier.GetType()))
            {
                return _judge[modifier.GetType()](modifier);
            }
            else
            {
                throw new InvalidOperationException("Not supported modifier {modifier}.");
            }
        }
    }
}

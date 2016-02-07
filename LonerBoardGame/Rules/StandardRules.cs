using GamePlatform.Api.Entities;
using GamePlatform.Api.Infos;
using GamePlatform.Api.Infos.Interfaces;
using GamePlatform.Api.ModifierBus.Interfaces;
using GamePlatform.Api.Modifiers.Interfaces;
using GamePlatform.Api.Modifiers.Modifiers;
using LonerBoardGame.Boards.Interfaces;
using LonerBoardGame.Games.Interfaces;
using LonerBoardGame.Modifiers;
using LonerBoardGame.Rules.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LonerBoardGame.Rules
{
    public class StandardRules : IStandardRules
    {
        private readonly Dictionary<Type, Func<IModifier, IScenario>> _judge = new Dictionary<Type, Func<IModifier, IScenario>>();
        public ILonerGame Game { get; set; }

        public StandardRules()
        {
            _judge.Add(typeof(MakeMoveModifier), m => { return MoveScenario(m as MakeMoveModifier); });
        }

        private IBasicPolygon GetCell(Point3d coordinates)
        {
            return Game.Board.Cells.Where(c => c.Coordintes.X == coordinates.X &&
                                                    c.Coordintes.Y == coordinates.Y &&
                                                    c.Coordintes.Z == coordinates.Z)
                                                    .FirstOrDefault();
        }

        private IBasicPolygon GetMiddleCell(Point3d from, Point3d to)
        {
            return GetCell(new Point3d() { X = (from.X + to.X) / 2, Y = (from.Y + to.Y) / 2, Z = (from.Z + to.Z) / 2 });
        }

        private List<IBasicPolygon> GetXNeighbours(IBasicPolygon polygon)
        {
            var neighbours = new List<IBasicPolygon>();

            for (int x = polygon.Coordintes.X - 1; x <= polygon.Coordintes.X + 1; x++)
            {
                if (x == polygon.Coordintes.X)
                {
                    continue;
                }

                var cell = GetCell(new Point3d() { X = x, Y = polygon.Coordintes.Y, Z = 0 });

                if (cell != null)
                {
                    neighbours.Add(cell);
                }
            }

            return neighbours;
        }

        private List<IBasicPolygon> GetYNeighbours(IBasicPolygon polygon)
        {
            var neighbours = new List<IBasicPolygon>();

            for (int y = polygon.Coordintes.Y - 1; y <= polygon.Coordintes.Y + 1; y++)
            {
                if (y == polygon.Coordintes.Y)
                {
                    continue;
                }

                var cell = GetCell(new Point3d() { X = polygon.Coordintes.X, Y = y, Z = 0 });

                if (cell != null)
                {
                    neighbours.Add(cell);
                }
            }

            return neighbours;
        }

        private List<IBasicPolygon> GetNeighbours(IBasicPolygon polygon)
        {
            var neighbours = new List<IBasicPolygon>();

            neighbours.AddRange(GetXNeighbours(polygon));
            neighbours.AddRange(GetYNeighbours(polygon));

            return neighbours;
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
                throw new InvalidOperationException(string.Format("Cell with coordinates {0} is solid.", coordinates));
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
                throw new InvalidOperationException(string.Format("Can't move {0} -> {1}.", from, to));
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
                throw new InvalidOperationException(string.Format("Can't move from {0}.", from));
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

        private bool IsGameWon()
        {
            return Game.Board.Cells.Where(c => c.State == PolygonState.Filled).Count() <= 1;
        }

        private bool HasNeighbourWithState(IBasicPolygon polygon, PolygonState state)
        {
            var neighbours = GetNeighbours(polygon);

            return neighbours.Where(cn => cn.State == state).Count() > 0;
        }

        private bool IsGameLost()
        {
            bool isLost = true;

            var filledCells = Game.Board.Cells.Where(c => c.State == PolygonState.Filled).ToList();

            filledCells.ForEach(c =>
            {
                if (HasNeighbourWithState(c, PolygonState.Filled))
                {
                    var neighbours = GetNeighbours(c);

                    neighbours.ForEach(n =>
                    {
                        if (HasNeighbourWithState(n, PolygonState.Empty))
                        {
                            isLost = false;
                        }
                    });
                }
            });

            return isLost;
        }

        public IInfo ReportGameStatus()
        {
            if (IsGameWon())
            {
                return new GameWonInfo();
            }
            else if (IsGameLost())
            {
                return new GameLostInfo();
            }
            else
            {
                return new GameInProgressInfo();
            }
        }

        public IScenario PostProcessing()
        {
            var postScenario = new SimpleScenario();

            if (IsGameLost() || IsGameWon())
            {
                var endGameModifier = new EndGameModifier(Game);
                endGameModifier.Source = this;

                postScenario.Modifiers.Add(endGameModifier);
            }

            return postScenario;
        }
    }
}
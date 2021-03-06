﻿using Autofac.Features.OwnedInstances;
using GamePlatform.Api.Entities;
using GamePlatform.Api.Infos.Interfaces;
using GamePlatform.Api.Players.Interfaces;
using GamePlatform.Api.Services.Interfaces;
using LonerBoardGame.Boards.Interfaces;
using LonerBoardGame.Games.Interfaces;
using LonerBoardGame.Modifiers;
using LonerConsole.Bootstrappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LonerConsole
{
    internal class Program
    {
        private static void DrawBoard(List<IBasicPolygon> board, int offset = 5)
        {
            for (int i = 0; i < 7; i++)
            {
                Console.SetCursorPosition(i * 2 + offset, offset - 2);
                Console.Write(i.ToString());
            }

            for (int i = 0; i < 7; i++)
            {
                Console.SetCursorPosition(offset - 2, i * 2 + offset);
                Console.Write(i.ToString());
            }

            board.ForEach(c =>
            {
                string cell = string.Empty;

                switch (c.State)
                {
                    case PolygonState.Empty:
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        cell = "1";
                        break;

                    case PolygonState.Filled:
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        cell = "2";
                        break;

                    default:
                    case PolygonState.Solid:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        cell = "0";
                        break;
                }

                Console.SetCursorPosition(c.Coordintes.X * 2 + offset, c.Coordintes.Y * 2 + offset);
                Console.Write(cell);
            });

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private class InfoPrinter : IObserver<IInfo>
        {
            public void OnCompleted()
            {
                throw new NotImplementedException();
            }

            public void OnError(Exception error)
            {
                throw new NotImplementedException();
            }

            public void OnNext(IInfo value)
            {
                int x = Console.CursorLeft;
                int y = Console.CursorTop;

                Console.SetCursorPosition(0, 23);
                Console.WriteLine("Info: " + value.Message);

                Console.SetCursorPosition(x, y);
            }
        }

        private static void Main(string[] args)
        {
            var boostrapper = new Bootstrapper();
            var infoChannel = new InfoPrinter();

            boostrapper.Configure();
            var factory = boostrapper.Get();

            IPlayer player = null;
            ILonerGame game = null;
            Owned<IGameService> gameS = null;

            using (gameS = factory())
            {
                game = gameS.Value.Game as ILonerGame;

                player = gameS.Value.GetNewPlayer();

                game.Join(player);

                game.Start();

                game.InfoChannel.Subscribe(infoChannel);

                while (true)
                {
                    DrawBoard(game.Board.Cells.ToList());

                    Console.SetCursorPosition(0, 25);
                    Console.WriteLine("Print move: ");
                    string read = Console.ReadLine();

                    if (read == "q")
                    {
                        break;
                    }
                    else
                    {
                        string[] coordinates = read.Split(',');

                        Console.SetCursorPosition(0, 26);
                        Console.WriteLine("");

                        if (coordinates.Length == 4)
                        {
                            var from = new Point3d() { X = int.Parse(coordinates[0]), Y = int.Parse(coordinates[1]) };
                            var to = new Point3d() { X = int.Parse(coordinates[2]), Y = int.Parse(coordinates[3]) };

                            var modifier = gameS.Value.GetModifierOfType<MakeMoveModifier>();

                            modifier.From = from;
                            modifier.To = to;

                            player.HeaveModifier(modifier);
                        }
                    }
                }
            }
        }
    }
}
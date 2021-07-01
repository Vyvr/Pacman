using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7_pacman
{
    class GamePacman
    {
        public enum FieldTypeEnum
        {
            Wall,
            Empty,
            Coin
        }
        public enum DirectionEnum
        {
            Up, Down, Left, Right
        }
        private FieldTypeEnum [,]board;
        private Point pacman;
        private DirectionEnum direction;
        private List<Point> ghosts;
        private int score;

        public Point Pacman { get => pacman; private set => pacman = value; }
        public List<Point> Ghosts { get => ghosts; private set => ghosts = value; }
        public int Score { get => score; private set => score = value; }

        public int Width { get => board.GetLength(0); }
        public int Height { get => board.GetLength(1); }

        internal DirectionEnum Direction { get => direction; set => direction = value; }

        public FieldTypeEnum getFieldType(int x, int y)
        {
            return board[x, y];
        }

        public GamePacman()
        {
            score = 0;
            ghosts = new List<Point>();


            string boardString = Properties.Resources.Board1;
            boardString = boardString.Replace("\r", "");
            string[] lines =  boardString.Split('\n');

            board = new FieldTypeEnum[lines[0].Length, lines.Length];

            for(int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    switch (lines[y][x])
                    {
                        case '*':
                            board[x, y] = FieldTypeEnum.Wall;
                            break;

                        case '.':
                            board[x, y] = FieldTypeEnum.Coin;
                            break;

                        default:
                            board[x, y] = FieldTypeEnum.Empty;
                            if (lines[y][x] == 'P')
                            {
                                pacman = new Point(x, y);
                                direction = DirectionEnum.Right;
                            }
                            else if (lines[y][x] == 'G')
                            {
                                ghosts.Add(new Point(x, y));
                            }
                            break;
                    }
                }
            }
        }

        internal void Move()
        {
            Point newPosition = pacman;
            switch(direction)
            {
                case DirectionEnum.Up:
                    newPosition.Y--;
                    break;
                case DirectionEnum.Down:
                    newPosition.Y++;
                    break;
                case DirectionEnum.Left:
                    newPosition.X--;
                    break;
                case DirectionEnum.Right:
                    newPosition.X++;
                    break;

            }

            if(board[newPosition.X, newPosition.Y] != FieldTypeEnum.Wall)
            {
                pacman = newPosition;
                if(board[pacman.X, pacman.Y] == FieldTypeEnum.Coin)
                {
                    board[pacman.X, pacman.Y] = FieldTypeEnum.Empty;
                    score++;
                }
            }
        }

        internal void MoveGhosts()
        {
            for (int i = 0; i < ghosts.Count; i++)
            {
                Point newPosition = ghosts[i];
                if (newPosition.X < pacman.X)
                {
                    newPosition.X++;
                }
                else if(newPosition.X > pacman.X)
                {
                    newPosition.X--;
                }
                if(board[newPosition.X, newPosition.Y] != FieldTypeEnum.Wall && newPosition != ghosts[i])
                {
                    ghosts[i] = newPosition;
                    continue;
                }

                newPosition = ghosts[i];
                if (newPosition.Y < pacman.Y)
                {
                    newPosition.Y++;
                }
                else if (newPosition.Y > pacman.Y)
                {
                    newPosition.Y--;
                }
                if (board[newPosition.X, newPosition.Y] != FieldTypeEnum.Wall && newPosition != ghosts[i])
                {
                    ghosts[i] = newPosition;
                }
            }
            checkTheEnd();
        }

        private void checkTheEnd()
        {
            for(int i = 0; i < ghosts.Count; i++)
            {
                if()
            }
        }
    }
}

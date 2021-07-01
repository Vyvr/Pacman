using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_7_pacman
{
    public partial class Form1 : Form
    {
        const int fieldSize = 70;
        Graphics graphics;
        GamePacman myGame;

        public Form1()
        {
            InitializeComponent();

            myGame = new GamePacman();

            pictureBoxBoard.Image = new Bitmap(myGame.Width * fieldSize, myGame.Height * fieldSize);
            graphics = Graphics.FromImage(pictureBoxBoard.Image);
            drawBoard();
        }

        private void drawBoard()
        {
            graphics.Clear(Color.Black);
            for(int x = 0; x < myGame.Width; x++)
            {
                for (int y = 0; y < myGame.Height; y++)
                {
                    switch(myGame.getFieldType(x, y))
                    {
                        case GamePacman.FieldTypeEnum.Wall:
                            graphics.FillRectangle(new SolidBrush(Color.Blue), x * fieldSize, y * fieldSize, fieldSize, fieldSize);
                            break;
                        case GamePacman.FieldTypeEnum.Coin:
                            graphics.FillEllipse(new SolidBrush(Color.Gold), x * fieldSize + fieldSize/4, y * fieldSize + fieldSize /4, fieldSize/2, fieldSize/2);
                            break;
                    }
                }
            }
            if(myGame.Direction == GamePacman.DirectionEnum.Left)
            {
                graphics.DrawImage(Properties.Resources.PacmanLeft, myGame.Pacman.X * fieldSize, myGame.Pacman.Y * fieldSize, fieldSize, fieldSize);
            }
            else if(myGame.Direction == GamePacman.DirectionEnum.Right)
            {
                graphics.DrawImage(Properties.Resources.PacmanRight, myGame.Pacman.X * fieldSize, myGame.Pacman.Y * fieldSize, fieldSize, fieldSize);
            }
            else
            {
                graphics.FillEllipse(new SolidBrush(Color.Yellow), myGame.Pacman.X * fieldSize, myGame.Pacman.Y * fieldSize, fieldSize, fieldSize);
            }

            foreach (Point g in myGame.Ghosts)
            {
                graphics.FillEllipse(new SolidBrush(Color.Violet), g.X * fieldSize, g.Y * fieldSize, fieldSize, fieldSize);
            }
            graphics.DrawString("Score: " + myGame.Score, this.Font, new SolidBrush(Color.White), 0, 0);
            pictureBoxBoard.Refresh();
        }

        private void timerPacman_Tick(object sender, EventArgs e)
        {
            myGame.Move();
            drawBoard();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Up:
                    myGame.Direction = GamePacman.DirectionEnum.Up;
                    break;
                case Keys.Down:
                    myGame.Direction = GamePacman.DirectionEnum.Down;
                    break;
                case Keys.Left:
                    myGame.Direction = GamePacman.DirectionEnum.Left;
                    break;
                case Keys.Right:
                    myGame.Direction = GamePacman.DirectionEnum.Right;
                    break;
            }
        }

        private void timerGhosts_Tick(object sender, EventArgs e)
        {
            myGame.MoveGhosts();
        }
    }
}

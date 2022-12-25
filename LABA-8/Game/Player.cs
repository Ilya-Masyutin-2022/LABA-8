using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace LABA_8.Game
{
    public class Player : Render
    {
        /// <summary>
        /// Класс игрок
        /// </summary>

        int number;

        public string Name { get; set; } = "";

        public int point_number { get; set; } = 0;

        private PointF shift;

        public bool Shift { get; set; } = true;

        private PointF delta;

        static Image[] sprites = new Image[4] {
            Resource1.Red,
            Resource1.Blue,
            Resource1.Green,
            Resource1.Yellow,
        };

        public Player(int newnumber, string newname, Size size, PointF point)
        {
            number = newnumber;
            Position = point;
            Name = newname;
            sprite = sprites[number - 1];
            entity_size = size;
            point_number = 0;

            switch (newnumber)
            {
                case 1: shift = new PointF(0, -10f); break;
                case 2: shift = new PointF(-10f, 0); break;
                case 3: shift = new PointF(0, 10f); break;
                case 4: shift = new PointF(10f, 0); break;

                default: break;
            }

            delta = new PointF(entity_size.Width / 2 + 25, 90);
        }

        public override void DrawSprite(Graphics graphics)
        {
            float renderx = Position.X - (delta.X / 2) + ((Shift) ? (shift.X) : (0));
            float rendery = Position.Y - (delta.Y / 2) + ((Shift) ? (shift.Y) : (0));

            if (sprite == null)
                sprite = sprites[number - 1];

            if (sprite == null)
                sprite = Resource1.missing_texture;

            graphics.DrawImage(sprite, renderx, rendery, entity_size.Width, entity_size.Height);
        }
    }
}

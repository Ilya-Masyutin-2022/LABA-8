using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABA_8.Engine
{
    public class Transform
    {
        /// <summary>
        /// Отвечает за всё, что связано с перемещениями объектов
        /// </summary>

        private PointF position;

        public virtual void MoveToward(PointF target, float speed)
        {
            if (target != position)
            {
                double x = Math.Abs(position.X - target.X);
                double y = Math.Abs(position.Y - target.Y);


                double c = Math.Sqrt((x * x) + (y * y));

                double k = speed / c;

                float delta_x = (float)(x * k);
                float delta_y = (float)(y * k);

                position.X += (target.X > position.X) ? (delta_x) : (-delta_x);
                position.Y += (target.Y > position.Y) ? (delta_y) : (-delta_y);

                if (c < speed)
                {
                    position.X = target.X;
                    position.Y = target.Y;
                }
            }
        }

        public PointF Position
        {
            get { return position; }
            set { position = value; }
        }
    }
}



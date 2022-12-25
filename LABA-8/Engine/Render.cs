using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABA_8.Engine
{
    public class Render : Transform
    {
        /// <summary>
        /// Хранит изображение объекта, его размера и его позицию на форме
        /// </summary>
        public Image sprite { get; set; }

        public Size entity_size { get; set; }

        public Render(Size size)
        {
            Position = new PointF(100, 100);
            entity_size = size;

            sprite = Resource1.missing_texture;
        }
        public Render(Image image, Size size)
        {
            Position = new PointF(100, 100);
            entity_size = size;

            sprite = image;
        }

        public Render()
        {
            sprite = Resource1.missing_texture;
        }

        public virtual void DrawSprite(Graphics graphics)
        {
            if (sprite == null)
                sprite = Resource1.missing_texture;

            graphics.DrawImage(sprite, Position.X, Position.Y, entity_size.Width, entity_size.Height);
        }
    }
}


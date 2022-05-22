using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEngine
{
    public class Vector
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vector Copy()
        {
            return new Vector(this.X, this.Y);
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector operator +(Vector v1, float f1)
        {
            return new Vector(v1.X + f1, v1.Y + f1);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector operator -(Vector v1, float f1)
        {
            return new Vector(v1.X - f1, v1.Y - f1);
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y;
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }

        public static Vector Zero()
        {
            return new Vector(0.0f, 0.0f);
        }

        public override string ToString()
        {
            return $"Vector({this.X}, {this.Y})";
        }

        public Size ToSize()
        {
            return new Size((int)this.X, (int)this.Y);
        }

        public Point ToPoint()
        {
            return new Point((int)this.X, (int)this.Y);
        }

        public PointF ToPointF()
        {
            return new PointF(this.X, this.Y);
        }
    }
}

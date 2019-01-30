using System;

namespace TravelHelper.Model.Math
{
    public struct Vector2
    {
        public float X { get; set; }

        public float Y { get; set; }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float Distance(float x, float y)
        {
            float dx = (X - x);
            dx *= dx;

            float dy = (Y - y);
            dy *= dy;

            return MathF.Sqrt(dx + dy);
        }

        public float Distance(Vector2 other)
        {
            return Distance(other.X, other.Y);
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            float dx = (a.X - b.X);
            dx *= dx;

            float dy = (a.Y - b.Y);
            dy *= dy;

            return MathF.Sqrt(dx + dy);
        }

        public static Vector2 Zero { get { return new Vector2(0, 0); } }

        public override string ToString()
        {
            return "X : " + X + ", Y : " + Y;
        }
    }
}
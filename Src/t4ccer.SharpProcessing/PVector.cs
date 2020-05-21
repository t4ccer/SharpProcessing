using System;
using System.Numerics;

namespace t4ccer.SharpProcessing
{
    public class PVector
    {
        private Vector2 v;

        public float X { get => v.X; set => v.X = value; }
        public float Y { get => v.Y; set => v.Y = value; }

        private PVector(Vector2 v)
        {
            this.v = v;
        }
        public PVector(double x, double y)
        {
            v = new Vector2((float)x, (float)y);
        }

        public void Mult(float s) => v = Vector2.Multiply(v, s);
        public void SetMag(float m) => v = Vector2.Normalize(v) * m;
        public void Add(PVector v1) => v = Vector2.Add(v, v1.v);
        public void Limit(float l)
            => SetMag(v.Length() > l ? l : v.Length());

        public PVector Copy() => new PVector(new Vector2(X, Y));
        public static PVector FromAngle(double angle) => new PVector(new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)));
    }
}

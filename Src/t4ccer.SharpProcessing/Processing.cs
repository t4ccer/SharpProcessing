
using SFML.Graphics;
using System;

namespace t4ccer.SharpProcessing
{
    public class Processing
    {
        private Random rng = new Random();
        public int Width => SFMLProcessingWindow.Width;
        public int Height => SFMLProcessingWindow.Height;

        public Pixels Pixels = new Pixels();
        public int FrameCount => SFMLProcessingWindow.Frame;

        public const float Pi = MathF.PI;
        public const float TwoPi = MathF.PI * 2;

        public virtual void Setup() { }
        public virtual void Draw() { }


        public void Size(uint width, uint height)
        {
            SFMLProcessingWindow.CreateWindow(width, height);
        }

        public void SaveFrame(string filename)
        {
            SFMLProcessingWindow.SaveFrame(filename);
        }
        public void SaveFrame()
        {
            SaveFrame($"screen-{FrameCount}.png");
        }

        public float Random(float max)
        {
            return (float)rng.NextDouble() * max;
        }

        #region 2D primitives

        public void Rect(float a, float b, float c, float d)
             => SFMLProcessingWindow.Rect(a, b, c, d);
        public void Square(float a, float b)
            => Rect(a, a, b, b);
        public void Triangle(float x1, float y1, float x2, float y2, float x3, float y3)
            => SFMLProcessingWindow.DrawTriangle(x1, y1, x2, y2, x3, y3);
        public void Circle(float x, float y, float d)
            => SFMLProcessingWindow.DrawCircle(x, y, d / 2);
        public void Line(float x1, float y1, float x2, float y2)
            => SFMLProcessingWindow.DrawLine(x1, y1, x2, y2);
        public void Point(float x, float y)
            => SFMLProcessingWindow.DrawPoint(x, y);
        #endregion

        #region Pixels
        public void LoadPixels()
            => SFMLProcessingWindow.GetPixels();
        public void UpdatePixels()
            => SFMLProcessingWindow.DrawPixels();

        #endregion

        #region Attributes
        public void StrokeWeight(float weight)
            => SFMLProcessingWindow.ChangeStrokeweight(weight);
        #endregion

        #region Colors
        public void ColorMode(ColorMode colorMode)
            => SFMLProcessingWindow.ChangeColorMode(colorMode);

        public Color Color(float v1, float v2, float v3, int a = 255)
            => SFMLProcessingWindow.GetColor(v1, v2, v3, a);
        public Color Color(float gray, int a = 255)
            => SFMLProcessingWindow.GetColor(gray, gray, gray, a);

        public void Background(float v1, float v2, float v3, int a = 255)
            => SFMLProcessingWindow.Background(v1, v2, v3, a);
        public void Background(byte gray, byte alpha = 255)
            => SFMLProcessingWindow.Background(gray, gray, gray, alpha);

        public void Fill(float v1, float v2, float v3, int a = 255)
            => SFMLProcessingWindow.ChangeFillColor(v1, v2, v3, a);
        public void Fill(byte gray, byte alpha = 255)
            => SFMLProcessingWindow.ChangeFillColor(gray, gray, gray, alpha);
        public void NoFill()
            => SFMLProcessingWindow.ChangeFillColor(0, 0, 0, 0);

        public void Stroke(float v1, float v2, float v3, int a = 255)
            => SFMLProcessingWindow.ChangeStrokeColor(v1, v2, v3, a);
        public void Stroke(byte gray, byte alpha = 255)
            => SFMLProcessingWindow.ChangeStrokeColor(gray, gray, gray, alpha);
        public void NoStroke()
            => SFMLProcessingWindow.ChangeStrokeColor(0, 0, 0, 0);
        #endregion
    }
}

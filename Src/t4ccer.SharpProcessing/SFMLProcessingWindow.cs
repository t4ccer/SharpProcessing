using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Xml.Schema;

namespace t4ccer.SharpProcessing
{
    internal static class SFMLProcessingWindow
    {
        private static ColorMode colorMode;
        public static byte[] Pixels { get; private set; }

        private static readonly StackWithDefault<Color> StrokeColors = new StackWithDefault<Color>(ColorBuilder.FromGrayscale(0));
        private static readonly StackWithDefault<Color> FillColors = new StackWithDefault<Color>(ColorBuilder.FromGrayscale(255));
        private static readonly StackWithDefault<float> StrokeWeights = new StackWithDefault<float>(1);

        private static RenderWindow renderWindow;

        public static int Frame { get; private set; }
        public static int Width => (int)renderWindow.Size.X;
        public static int Height => (int)renderWindow.Size.Y;
        public static bool IsOpen => renderWindow.IsOpen;

        public static void CreateWindow(uint width, uint height)
        {
            if (renderWindow != null)
                return;

            renderWindow = new RenderWindow(new VideoMode(width, height), "Processing", Styles.Close);
            renderWindow.SetActive();
            renderWindow.Closed += (a, b)
             => renderWindow.Close();
            renderWindow.SetFramerateLimit(60);

        }

        public static void SaveFrame(string filename)
        { 
            var textute = new Texture((uint)Width, (uint)Height);
            textute.Update(renderWindow);
            textute.CopyToImage().SaveToFile(filename);
        }


        public static void Rect(float a, float b, float c, float d)
        {
            var shape = new RectangleShape(new Vector2f(c, d))
            {
                Position = new Vector2f(a, b),
                FillColor = FillColors.Peek(),
                OutlineThickness = StrokeWeights.Peek(),
                OutlineColor = StrokeColors.Peek()
            };
            renderWindow.Draw(shape);
        }
        public static void DrawTriangle(float x1, float y1, float x2, float y2, float x3, float y3)
        {
            var shape = new ConvexShape(3)
            {
                //Position = new Vector2f(x1, x2),
                FillColor = FillColors.Peek(),
                OutlineThickness = StrokeWeights.Peek(),
                OutlineColor = StrokeColors.Peek(),
            };
            shape.SetPoint(0, new Vector2f(x1, y1));
            shape.SetPoint(1, new Vector2f(x2, y2));
            shape.SetPoint(2, new Vector2f(x3, y3));

            renderWindow.Draw(shape);
        }
        public static void DrawCircle(float x, float y, float r)
        {
            var ellipse = new CircleShape(r)
            {
                Position = new Vector2f(x - r, y - r),
                FillColor = FillColors.Peek(),
                OutlineThickness = StrokeWeights.Peek(),
                OutlineColor = StrokeColors.Peek(),
            };

            renderWindow.Draw(ellipse);
        }
        public static void DrawPoint(float x, float y)
        {
            var r = StrokeWeights.Peek() / 2;
            if (r < 1) r = 1;
            var ellipse = new CircleShape(r)
            {
                Position = new Vector2f(x - r, y - r),
                FillColor = StrokeColors.Peek(), //Its OK
            };

            renderWindow.Draw(ellipse);
        }
        public static void DrawLine(float x1, float y1, float x2, float y2)
        {
            var n = StrokeWeights.Peek();
            var l = MathF.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));

            var a = MathF.Atan2(y2 - y1, x2 - x1) * (180 / MathF.PI);

            var shape = new RectangleShape(new Vector2f(l, n + 1))
            {
                Position = new Vector2f(x1 + (n / 2), y1 + (n / 2)),
                FillColor = StrokeColors.Peek(), //Its OK
                Rotation = a,
                OutlineThickness = 0
            };
            renderWindow.Draw(shape);
        }

        public static void GetPixels() => Pixels = new byte[renderWindow.Size.X * renderWindow.Size.Y * 4];
        public static void DrawPixels()
        {
            var text = new Texture(renderWindow.Size.X, renderWindow.Size.Y);
            var sprite = new Sprite(text);
            text.Update(Pixels);
            renderWindow.Draw(sprite);
        }

        //Do not touch
        public static void Refresh()
        {
            var textute = new Texture((uint)Width, (uint)Height);
            var sprite = new Sprite(textute);
            textute.Update(renderWindow);

            renderWindow.Display();

            renderWindow.Draw(sprite);

            renderWindow.Display();

            renderWindow.DispatchEvents();
            Frame++;

            if (Frame % 10 == 0)
                GC.Collect();
        }

        public static void Background(float v1, float v2, float v3, int a)
            => renderWindow.Clear(GetColor(v1, v2, v3, a));
        public static void ChangeFillColor(float v1, float v2, float v3, int a)
            => FillColors.SwapTop(GetColor(v1, v2, v3, a));
        public static void ChangeStrokeColor(float v1, float v2, float v3, int a)
            => StrokeColors.SwapTop(GetColor(v1, v2, v3, a));
        public static void ChangeStrokeweight(float weight)
            => StrokeWeights.SwapTop(weight);
        public static void ChangeColorMode(ColorMode mode)
            => colorMode = mode;

        public static Color GetColor(float v1, float v2, float v3, int a)
        {
            if (colorMode == ColorMode.RGB)
                return ColorBuilder.FromARGB((byte)v1, (byte)v2, (byte)v3, (byte)a);
            else
                return FromHSB(v1, v2, v3);
        }
        public static Color FromHSB(float hue, float saturation, float brightness)
        {
            hue = (hue / 360);
            if (hue > 1)
                hue %= 1;

            saturation = (saturation / 100);
            if (saturation > 1)
                saturation %= 1;

            brightness = (brightness / 100);
            if (brightness > 1)
                brightness %= 1;

            int r = 0, g = 0, b = 0;
            if (saturation == 0)
            {
                r = g = b = (int)(brightness * 255.0f + 0.5f);
            }
            else
            {
                var h = (hue - (float)Math.Floor(hue)) * 6.0f;
                var f = h - (float)Math.Floor(h);
                var p = brightness * (1.0f - saturation);
                var q = brightness * (1.0f - saturation * f);
                var t = brightness * (1.0f - (saturation * (1.0f - f)));
                switch ((int)h)
                {
                    case 0:
                        r = (int)(brightness * 255.0f + 0.5f);
                        g = (int)(t * 255.0f + 0.5f);
                        b = (int)(p * 255.0f + 0.5f);
                        break;
                    case 1:
                        r = (int)(q * 255.0f + 0.5f);
                        g = (int)(brightness * 255.0f + 0.5f);
                        b = (int)(p * 255.0f + 0.5f);
                        break;
                    case 2:
                        r = (int)(p * 255.0f + 0.5f);
                        g = (int)(brightness * 255.0f + 0.5f);
                        b = (int)(t * 255.0f + 0.5f);
                        break;
                    case 3:
                        r = (int)(p * 255.0f + 0.5f);
                        g = (int)(q * 255.0f + 0.5f);
                        b = (int)(brightness * 255.0f + 0.5f);
                        break;
                    case 4:
                        r = (int)(t * 255.0f + 0.5f);
                        g = (int)(p * 255.0f + 0.5f);
                        b = (int)(brightness * 255.0f + 0.5f);
                        break;
                    case 5:
                        r = (int)(brightness * 255.0f + 0.5f);
                        g = (int)(p * 255.0f + 0.5f);
                        b = (int)(q * 255.0f + 0.5f);
                        break;
                }
            }
            return ColorBuilder.FromRGB(Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b));
        }
    }
}


using SFML.Graphics;

namespace t4ccer.SharpProcessing
{
    public class Pixels
    {
        public Color this[int x, int y]
        {
            get
            {
                Color color = new Color();
                int index = (y * SFMLProcessingWindow.Width + x) * 4;
                color.R = SFMLProcessingWindow.Pixels[index + 0];
                color.G = SFMLProcessingWindow.Pixels[index + 1];
                color.B = SFMLProcessingWindow.Pixels[index + 2];
                color.A = SFMLProcessingWindow.Pixels[index + 3];
                return color;
            }
            set
            {
                int index = (y * SFMLProcessingWindow.Width + x) * 4;
                SFMLProcessingWindow.Pixels[index + 0] = value.R;
                SFMLProcessingWindow.Pixels[index + 1] = value.G;
                SFMLProcessingWindow.Pixels[index + 2] = value.B;
                SFMLProcessingWindow.Pixels[index + 3] = value.A;
            }
        }
        public Color this[int i]
        {
            get
            {
                Color color = new Color();
                int x = i / SFMLProcessingWindow.Height;
                int y = i % SFMLProcessingWindow.Height;
                int index = (x * SFMLProcessingWindow.Width + y) * 4; color.R = SFMLProcessingWindow.Pixels[index + 0];
                color.G = SFMLProcessingWindow.Pixels[index + 1];
                color.B = SFMLProcessingWindow.Pixels[index + 2];
                color.A = SFMLProcessingWindow.Pixels[index + 3];
                return color;
            }
            set
            {
                int x = i / SFMLProcessingWindow.Height;
                int y = i % SFMLProcessingWindow.Height;
                int index = (x * SFMLProcessingWindow.Width + y) * 4;
                SFMLProcessingWindow.Pixels[index + 0] = value.R;
                SFMLProcessingWindow.Pixels[index + 1] = value.G;
                SFMLProcessingWindow.Pixels[index + 2] = value.B;
                SFMLProcessingWindow.Pixels[index + 3] = value.A;
            }
        }
    }
}


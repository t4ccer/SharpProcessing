using SFML.Graphics;

namespace t4ccer.SharpProcessing
{
    internal static class ColorBuilder
    {
        public static Color FromGrayscale(byte grayscale)
            
 
 			=> new Color(grayscale, grayscale, grayscale);
        public static Color FromAGrayscale(byte grayscale, byte a)
            
 
 			=> new Color(grayscale, grayscale, grayscale, a);
        public static Color FromRGB(byte r, byte g, byte b)
            
 
 			=> new Color(r, g, b);
        public static Color FromARGB(byte r, byte g, byte b, byte a)
            
 
 			=> new Color(r, g, b, a);
    }
}

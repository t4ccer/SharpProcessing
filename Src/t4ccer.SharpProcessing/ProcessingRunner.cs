using System;

namespace t4ccer.SharpProcessing
{
    public static class ProcessingRunner
    {
        public static void Run(Processing processing)
        {
            processing.Setup();
            while (SFMLProcessingWindow.IsOpen)
            {

                processing.Draw();
                SFMLProcessingWindow.Refresh();
            }
        }
    }
}

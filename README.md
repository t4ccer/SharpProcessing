# SharpProcessing
SharpProcessing is attempt to port [Processing](https://processing.org/) to C#. Project is in a really early stage so majority of features is not implemented yet. Currently SharpProcessing uses [SFML.NET](https://www.sfml-dev.org/download/sfml.net/) for window handling (I consider changing it later to something else).
## Installation
To use in Your code just install [t4ccer.SharpProcessing](https://www.nuget.org/packages/t4ccer.SharpProcessing/) nuget package.
## Example
Example code from [this](https://p5js.org/examples/hello-p5-animation.html) p5.js example ported to SharpProcessing.
```csharp
    public class MyApp : Processing
    {
        float x, y;

        public override void Setup()
        {
            Size(500, 500);
            x = Width / 2;
            y = Height;
        }
        public override void Draw()
        {
            Background(200);

            // Draw a circle
            Stroke(50);
            Fill(100);
            Circle(x, y, 24);

            // Jiggling randomly on the horizontal axis
            x = x + Random(2)-1;
            // Moving up at a constant speed
            y = y - 1;

            // Reset to the bottom
            if (y < 0)
                y = Height;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ProcessingRunner.Run(new MyApp());
        }
    }
```

 ## Roadmap
  - Implement mouse and keyboard handling
  - Implement all 2D primitives and curves.
  - Add OpenGL renderer
  - Implement missing 2D features
  - Implement 3D features
 
 ## Contribution
 Feel free to add some features or fix bugs
namespace oH_mY_goD
{
    class Program
    {
        static Matrix matrix;
        static Camera cam;
        static Objects.Cube cube;
        static Shapes.Triangle tri;
        static Shapes.Square sqr;
        static Dot dot;
        static int[] size = { 30, 120 };
        static float move = 1;
        static void Main(string[] args)
        {
            Console.WindowHeight = size[0];
            Console.WindowWidth = size[1];
            Console.SetBufferSize(1200, size[0]);
            matrix = new Matrix(size);
            cam = new Camera(new Vector(0, 10, 10), new Vector(0, 0, 90));
            cube = new Objects.Cube(new Vector(10, 0, 0), 20);
            tri = new Shapes.Triangle(new Vector(10, 0, 0), new Vector(10, 10, 0), new Vector(20, 5, -5));
            dot = new Dot(new Vector(10, 2, 2));
            sqr = new Shapes.Square(new Vector(10, 0, 0), 20);
            World.matrix = matrix;
            World.cam = cam;
            Update();
        }
        static void Update()
        {
            while (true)
            {
                matrix.Update();
                cam.Update();
                if (cam.pos.y == 40|| cam.pos.y == -40)
                    move*=-1;
                //cam.pos.y += move;
                //dot.Update();
                cube.Rotate(cube.centroid,10, 2);
                cube.Rotate(cube.centroid,5, 1);
                cube.Rotate(cube.centroid,-8, 0);
                cube.makeMe();
                //cube.Fill();
                //cube.In(dot);
                //tri.MakeMe();
                //sqr.MakeMe();
                //sqr.Rotate(10, 0);
                matrix.PutTheShoe();
                Thread.Sleep(100);
                Console.Clear();
            }
        }
    }
}


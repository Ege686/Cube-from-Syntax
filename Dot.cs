using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oH_mY_goD
{
    internal class Dot
    {
        public Vector pos;
        public Vector v_pos;
        Camera cam;
        Matrix matrix;
        public float dis;
        public Dot(Vector pos)
        {
            this.pos = pos;
            v_pos = new Vector(0, 0);
            matrix = World.matrix;
            cam = World.cam;
            Update(false);
        }
        public void Update()
        {
            float t = find_t();
            v_pos.vector = projection(t);
            v_pos.vector = repo(t);
            repro();
            putInAShoe();
        }
        public void Update(bool put)
        {
            float t = find_t();
            v_pos.vector = projection(t);
            v_pos.vector = repo(t);
            repro();
            if (put)
                putInAShoe();
        }
        float find_t()
        {
            float[] dif = Operations.Substract(pos.vector, cam.pos.vector);
            float sqr = Operations.Dot_Product(cam.dir.vector, cam.dir.vector);
            float mul = Operations.Dot_Product(cam.dir.vector, dif);
            float t = mul / sqr;
            return t;
        }
        float[] projection(float t)
        {
            float[] dir = Operations.Scaler_Multiplication(t, cam.dir.vector);
            float[] loc = Operations.Add(cam.pos.vector, dir);
            float[] vec = Operations.Substract(pos.vector, loc);
            float[] rep = new float[2];
            rep[0] = Operations.Dot_Product(vec, cam.right.vector);
            rep[1] = Operations.Dot_Product(vec, cam.up.vector);
            //Console.WriteLine(rep[0]+" " + rep[1]+" "+ cam.up.vector[0]+" " + cam.up.vector[1]+" " + cam.up.vector[2]);
            return rep;
        }
        float[] repo(float t)
        {
            float dis = t;
            this.dis = dis;
            float min = MathF.Min(matrix.size[0], matrix.size[1]);
            float r_size = MathF.Tan(cam.fov) * dis + min;
            float ratio = min / r_size;
            float[] repro = new float[2];
            repro[0] = v_pos.x * ratio*1.75f;
            repro[1] = v_pos.y * ratio;
            return repro;
        }
        void repro()
        {
            v_pos.x = matrix.size[1]/2-v_pos.x;
            v_pos.y = matrix.size[0]/2-v_pos.y;
        }
        void putInAShoe()
        {
            int x = (int)v_pos.x;
            int y = (int)v_pos.y;
            if (dis > 0 && x >= 0 && x < matrix.size[1] && y >= 0 && y < matrix.size[0])
            {
                Dot d = matrix.matrix[y][x];
                if (d==null ||d.dis > dis)
                {
                    if (d == null)
                    {
                        d = this;
                        matrix.matrix[y][x] = d;
                    }
                    else
                        matrix.matrix[y][x].dis = dis;
                }
            }
        }
    }
}

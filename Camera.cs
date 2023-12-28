using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oH_mY_goD
{
    internal class Camera
    {
        public Vector pos;
        Vector rot;
        public Vector dir;
        public Vector up;
        public Vector right;
        public float fov=30;
        int dim = 0;
        public Camera(Vector pos, Vector rot)
        {
            this.pos = pos;
            dir=new Vector(0,0,0);
            up=new Vector(0,0,0);
            right=new Vector(0,0,0);
            rot.vector = Deg_to_Rad(rot.vector);
            this.rot = rot;
            fov=deg_to_rad(fov);
            dim = pos.dim;
            World.cam = this;
        }
        public void Update()
        {
            find_dir();

            find_up();
            find_right();
        }
        void find_dir()
        {
            dir.vector[dim - 1] = MathF.Cos(rot.vector[dim - 1]);
            float pre = MathF.Sin(rot.vector[dim - 1]);
            for (int i = dim - 2; i > 0; i--)
            {
                dir.vector[i] = pre * MathF.Cos(rot.vector[i]);
                if (i != 1)
                    pre *= MathF.Sin(rot.vector[i]);
            }
            dir.vector[0] = dir.vector[1];
            dir.vector[1] = pre * MathF.Sin(rot.vector[1]);
        }
        void find_up()
        {
            up.vector[dim - 1] = MathF.Cos(rot.vector[dim - 1]-MathF.PI/2);
            float pre = MathF.Sin(rot.vector[dim - 1] - MathF.PI / 2);
            for (int i = dim - 2; i > 0; i--)
            {
                up.vector[i] = pre * MathF.Cos(rot.vector[i]);
                if (i != 1)
                    pre *= MathF.Sin(rot.vector[i]);
            }
            up.vector[0] = up.vector[1];
            up.vector[1] = pre * MathF.Sin(rot.vector[1]);
        }
        void find_right()
        {
            float[] rota=new float[dim];
            for(int i=0;i<dim;i++)
                rota[i]=rot.vector[i];
            rota[1] = rota[1] - MathF.PI / 2;
            right.vector[dim - 1] = MathF.Cos(rota[dim - 1]);
            float pre = MathF.Sin(rota[dim - 1]);
            for (int i = dim - 2; i > 0; i--)
            {
                right.vector[i] = pre * MathF.Cos(rota[i]);
                if (i != 1)
                    pre *= MathF.Sin(rota[i]);
            }
            right.vector[0] = right.vector[1];
            right.vector[1] = pre * MathF.Sin(rota[1]);
        }
        float[] Deg_to_Rad(float[] degs)
        {
            for(int i = 0; i < degs.Length; i++)
            {
                degs[i]=deg_to_rad(degs[i]);
            }
            return degs;
        }
        float deg_to_rad(float deg)
        {
            return deg / 180 * MathF.PI;
        }
    }
}

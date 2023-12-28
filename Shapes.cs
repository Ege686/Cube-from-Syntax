using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oH_mY_goD
{
    internal class Shapes
    {
        public class Line
        {
            public Vector start;
            public Vector end;
            public Vector r_start;
            public Vector r_end;
            public float length;
            public List<Dot> line=new List<Dot>();
            public Line(Dot start, Dot end)
            {
                this.start = start.v_pos;
                this.end = end.v_pos;
                r_start = start.pos;
                r_end = end.pos;
            }
            public void Draw(string s)
            {
                float slope = 0;
                float delta_y = end.y - start.y;
                float delta_x = end.x - start.x;

                float r_delta_y = r_end.y - r_start.y;
                float r_delta_x = r_end.x - r_start.x;
                float r_delta_z = r_end.x - r_start.x;
                Vector dir=new Vector(r_delta_x,r_delta_y,r_delta_z);
                dir.Normalize();

                line.Clear();
                length = Operations.Distance(start, end);
                if (delta_x != 0)
                    slope = delta_y / (delta_x);
                
                if (slope<1&&slope>-1&&delta_x!=0)
                {
                    int addision = (int)(delta_x / MathF.Abs(delta_x));
                    for (int i = (int)start.x+1; i*addision < end.x*addision; i+=addision)
                    {
                        int d_y = (int)((i- start.x) * slope);
                        if ((int)start.y + d_y < World.matrix.matrix.Length&& (int)start.y + d_y>0 &&i>0&& i < World.matrix.matrix[0].Length)
                        {
                            World.matrix.s_matrix[(int)start.y + d_y][i] = s;
                            //line.Add(new Vector( i,(int)start.y + d_y));
                        }
                    }
                }
                else
                {
                    slope = delta_x / (delta_y + 0.00001f);

                    int addision = (int)(delta_y / MathF.Abs(delta_y));
                    for (int i = (int)start.y + 1; i*addision < end.y*addision; i+=addision)
                    {
                        int d_x = (int)((i - start.y) * slope);
                        if ((int)start.x + d_x < World.matrix.matrix[0].Length&&(int)start.x + d_x>0 &&i>0&& i < World.matrix.matrix.Length)
                        {
                            World.matrix.s_matrix[i][(int)start.x + d_x] = s;
                            //line.Add(new Vector ((int)min.x + d_x,  i ));
                        }
                    }
                }
            }
        }
        public class Triangle
        {
            public Dot[] dots;
            Camera cam = World.cam;
            Matrix matrix = World.matrix;
            public Vector centroid;
            public Line[] lines;
            public Triangle(params Vector[] p)
            {
                dots=new Dot[p.Length];
                lines = new Line[p.Length];
                for(int d = 0; d < dots.Length; d++)
                {
                    dots[d] = new Dot(p[d]);
                }
                Centroid();
            }
            public void MakeMe()
            {
                foreach(var d in dots)
                {
                    d.Update();
                }
                Draw();
            }
            public void Draw()
            {
                for(int d=0; d < dots.Length-1; d++)
                {
                    lines[d] = new Line(dots[d], dots[d + 1]);
                    lines[d].Draw(".");
                }
                lines[lines.Length-1] = new Line(dots[dots.Length-1], dots[0]);
                lines[lines.Length - 1].Draw(".");
                //Fill();
            }
            public void Fill()
            {
                for(int v = 1; v < lines.Length-1; v++)
                {
                    for(int d = 0; d < lines[v].line.Count; d++)
                    {
                        //Line l = new Line(dots[0], new Dot(new Vector(lines[v].line[d].x, lines[v].line[d].y, lines[v].line[d].z)));
                        //l.Draw("-");
                    }
                }
            }
            public void Rotate(Vector about, float degree, int axis)
            {
                foreach(Dot d in dots)
                {
                    d.pos = Operations.Rotate(d.pos, about, degree, axis);
                }
            }
            public Vector Centroid()
            {
                float[] vec=new float[dots[0].pos.dim];
                for (int d = 0; d < dots[0].pos.dim; d++)
                {
                    vec[d] = 0;
                    foreach(Dot dot in dots)
                    {
                        vec[d] += dot.pos.vector[d] / dots.Length;
                    }
                }
                centroid = new Vector(vec);
                return centroid;
            }
        }
        public class Square
        {
            Vector p1;
            public Triangle[] tris;
            float length;
            public Vector centroid;
            public Square(Vector p1,float lenght)
            {
                this.p1 = p1;
                tris = new Triangle[2];
                this.length=lenght;
                Vector a = new Vector(p1.x,p1.y+ lenght, p1.z);
                Vector b = new Vector(p1.x, p1.y, p1.z+lenght);
                Vector c = new Vector(p1.x, p1.y+lenght, p1.z+lenght);
                tris[0] = new Triangle(p1, a, b);
                tris[1] = new Triangle(c, a, b);
                Centroid();
            }
            public Square(params Triangle[] t1)
            {
                tris=new Triangle[t1.Length];
                for (int t = 0; t < t1.Length; t++)
                {
                    tris[t] = t1[t];
                }
            }
            public void MakeMe()
            {
                for(int t = 0; t < tris.Length; t++)
                {
                    tris[t].MakeMe();
                }
            }
            public void Rotate(Vector about,float degree,int axis)
            {
                Centroid();
                foreach (Triangle t in tris)
                {
                    t.Rotate(about, degree, axis);
                }
            }
            public void Fill()
            {
                foreach(Triangle t in tris)
                {
                    t.Fill();
                }
            }
            public void In()
            {
                Console.WriteLine(Operations.InArea(tris[0], new Vector(10,2,-2)));
            }
            public void Centroid()
            {
                Vector v = new Vector(0, 0, 0);
                foreach (Triangle t in tris)
                {
                    t.Centroid();
                    v.x += t.centroid.x;
                    v.y += t.centroid.y;
                    v.z += t.centroid.z;
                }
                v.x /= tris.Length;
                v.y/= tris.Length;
                v.z /= tris.Length;
                centroid = v;
            }
        }
    }
}

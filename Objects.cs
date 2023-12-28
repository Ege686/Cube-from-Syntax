using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oH_mY_goD
{
    class Objects
    {
        public class Cube
        {
            Vector pos;
            float length;
            Shapes.Square[] faces;
            public Vector centroid;
            Shapes.Line[] lines;
            public Cube(Vector pos, float length)
            {
                this.pos = pos;
                faces = new Shapes.Square[6];
                this.length = length;
                faces[0] = new Shapes.Square(pos, length);
                faces[1] = new Shapes.Square(new Vector(pos.x + length, pos.y, pos.z), length);
                Shapes.Triangle s1 = new Shapes.Triangle(faces[0].tris[0].dots[0].pos, faces[0].tris[0].dots[2].pos, faces[1].tris[0].dots[2].pos, faces[1].tris[0].dots[0].pos);
                Shapes.Triangle s2 = new Shapes.Triangle(faces[0].tris[0].dots[0].pos, faces[0].tris[0].dots[1].pos, faces[1].tris[0].dots[1].pos, faces[1].tris[0].dots[0].pos);
                Shapes.Triangle s3 = new Shapes.Triangle(faces[0].tris[1].dots[1].pos, faces[0].tris[1].dots[0].pos, faces[1].tris[1].dots[0].pos, faces[1].tris[1].dots[1].pos);
                Shapes.Triangle s4 = new Shapes.Triangle(faces[0].tris[1].dots[2].pos, faces[0].tris[1].dots[0].pos, faces[1].tris[1].dots[0].pos, faces[1].tris[1].dots[2].pos);
                faces[2] = new Shapes.Square(s1);
                faces[3] = new Shapes.Square(s2);
                faces[4] = new Shapes.Square(s3);
                faces[5] = new Shapes.Square(s4);
                Centroid();
            }
            public void Senc()
            {
                faces[2].tris[0].dots = new Dot[4] { new Dot(faces[0].tris[0].dots[0].pos), new Dot(faces[0].tris[0].dots[2].pos), new Dot(faces[1].tris[0].dots[2].pos), new Dot(faces[1].tris[0].dots[0].pos) };
                faces[3].tris[0].dots = new Dot[4] { new Dot(faces[0].tris[0].dots[0].pos), new Dot(faces[0].tris[0].dots[1].pos), new Dot(faces[1].tris[0].dots[1].pos), new Dot(faces[1].tris[0].dots[0].pos) };
                faces[4].tris[0].dots = new Dot[4] { new Dot(faces[0].tris[1].dots[1].pos), new Dot(faces[0].tris[1].dots[0].pos), new Dot(faces[1].tris[1].dots[0].pos), new Dot(faces[1].tris[1].dots[1].pos) };
                faces[5].tris[0].dots = new Dot[4] { new Dot(faces[0].tris[1].dots[2].pos), new Dot(faces[0].tris[1].dots[0].pos), new Dot(faces[1].tris[1].dots[0].pos), new Dot(faces[1].tris[1].dots[2].pos) };
                faces[2].MakeMe();
                faces[3].MakeMe();
                faces[4].MakeMe();
                faces[5].MakeMe();
            }
            public void makeMe()
            {
                faces[0].MakeMe();
                faces[1].MakeMe();
                /*foreach (Shapes.Square face in faces)
                {
                    face.MakeMe();
                }*/
                Senc();
            }
            public void Fill()
            {
                foreach (Shapes.Square face in faces)
                {
                    face.Fill();
                }
            }
            public void Rotate(Vector about,float angle,int axis)
            {
                faces[0].Rotate(about, angle, axis);
                faces[1].Rotate(about, angle, axis);
                //Senc();
                /*foreach ( Shapes.Square face in faces)
                {
                    Centroid();
                    face.Rotate(about,angle,axis);
                }*/
            }
            public void In(Dot d)
            {
                //Console.WriteLine(Operations.InArea(faces[0].tris[0], new Vector(d.v_pos.x,d.v_pos.y)));
            }
            public void Centroid()
            {
                Vector c = new Vector(0,0,0);
                foreach (Shapes.Square face in faces)
                {
                    face.Centroid();
                    c.x += face.centroid.x;
                    c.y += face.centroid.y;
                    c.z += face.centroid.z;
                }
                c.x /= faces.Length;
                c.y /= faces.Length;
                c.z /= faces.Length;
                centroid = c;
            }
        }
    }
}

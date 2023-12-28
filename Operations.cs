using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oH_mY_goD
{
    internal static class Operations
    {
        static float[][] rot_matrix = new float[3][];
        public static float[] Add(float[] a, float[] b)
        {
            float[] result = new float[a.Length];
            for(int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] + b[i];
            }
            return result;
        }
        public static float[] Substract(float[] a, float[] b)
        {
            float[] result = new float[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] - b[i];
            }
            return result;
        }
        public static float[] Scaler_Multiplication(float a, float[] b)
        {
            float[] result = new float[b.Length];
            for(int i = 0; i < b.Length; i++)
            {
                result[i] = b[i] * a;
            }
            return result;
        }
        public static float[][] Matrix_Multiplication(float[][] a, float[][] b)
        {
            float[][] result=new float[a[0].Length][];
            for (int i = 0; i < result.Length; i++)
                result[i] = new float[b.Length];
            for(int r = 0; r < a.Length; r++)
            {
                for(int c = 0; c < b[0].Length; c++)
                {
                    float m = 0;
                    for (int e = 0; e < a[r].Length; e++)
                    {
                        m += a[r][e] * b[e][c];
                    }
                    result[r][c] = m;
                }
            }
            return result;
        }
        public static float[] Element_Multiplication(float[] a, float[] b)
        {
            float[] result = new float[b.Length];
            for (int i = 0; i < b.Length; i++)
            {
                result[i] = b[i] * a[i];
            }
            return result;
        }
        public static float Dot_Product(float[] a, float[] b)
        {
            float result = 0;
            for(int i = 0; i < a.Length; i++)
            {
                result += a[i] * b[i];
            }
            return result;
        }
        public static Vector Rotate(Vector victim, Vector about, float degree,int axis)
        {
            degree = degree * MathF.PI / 180;
            if (axis == 0)
            {
                rot_matrix[0] =new float[3] { 1,0,0};
                rot_matrix[1] =new float[3] { 0,MathF.Cos(degree),MathF.Sin(degree)};
                rot_matrix[2] =new float[3] { 0,-MathF.Sin(degree),MathF.Cos(degree)};
            }
            if (axis == 1)
            {
                rot_matrix[0] = new float[3] { MathF.Cos(degree), 0, -MathF.Sin(degree) };
                rot_matrix[1] = new float[3] { 0, 1, 0 };
                rot_matrix[2] = new float[3] { MathF.Sin(degree), 0, MathF.Cos(degree) };
            }
            if (axis == 2)
            {
                rot_matrix[0] = new float[3] { MathF.Cos(degree), MathF.Sin(degree), 0 };
                rot_matrix[1] = new float[3] { -MathF.Sin(degree), MathF.Cos(degree), 0 };
                rot_matrix[2] = new float[3] { 0, 0, 1 };
            }
            float[][] origi=new float[1][];
            origi[0] = Substract(victim.vector, about.vector);
            origi = Transpose(origi);
            float[][] result = Matrix_Multiplication(rot_matrix, origi);
            result = Transpose(result);
            result[0] = Add(result[0], about.vector);
            Vector res = new Vector(result[0]);
            return res;
        }
        public static float[][] Transpose(float[][] a)
        {
            float[][] result = new float[a[0].Length][];
            for(int r = 0; r < a[0].Length; r++)
            {
                result[r] = new float[a.Length];
                for(int c = 0; c < a.Length; c++)
                {
                    result[r][c] = a[c][r];
                }
            }
            return result;
        }
        public static float[][] ChangeRow(float[][] a,int index1,int index2)
        {
            float[][] result = new float[a.Length][];
            for (int r = 0; r < a.Length; r++)
            {
                result[r] = new float[a[r].Length];
                for (int c = 0; c < a[r].Length; c++)
                {
                    if (r == index1)
                    {
                        result[r][c] = a[index2][c];
                    }
                    else if (r == index2)
                    {
                        result[r][c] = a[index1][c];
                    }
                    else
                        result[r][c] = a[r][c];
                }
            }
            return result;
        }
        public static float[][] AddRows(float[][] a, int index1, int index2,float coef)
        {
            float[][] result = new float[a.Length][];
            for (int r = 0; r < a.Length; r++)
            {
                result[r] = new float[a[r].Length];
                for (int c = 0; c < a[r].Length; c++)
                {
                    if (r == index2)
                    {
                        result[r] = Add(a[index2], Scaler_Multiplication(coef, a[index1]));
                    }
                    else
                        result[r][c] = a[r][c];
                }
            }
            return result;
        }
        public static float[][] MultiplyRow(float[][] a, int index1, float coef)
        {
            float[][] result = new float[a.Length][];
            for (int r = 0; r < a.Length; r++)
            {
                result[r] = new float[a[r].Length];
                for (int c = 0; c < a[r].Length; c++)
                {
                    if (r == index1)
                    {
                        result[r] = Scaler_Multiplication(coef, a[index1]);
                    }
                    else
                        result[r][c] = a[r][c];
                }
            }
            return result;
        }
        public static float[][] REF(float[][] a)
        {
            float[][] result = Equalize(a);
            for(int r = 0; r < a.Length; r++)
            {
                /*
                float min = 10000;
                int index = 0;
                for (int i = r; i < a.Length; i++)
                {
                    if (a[i][r] < min)
                    {
                        min = a[i][r];
                        index = i;
                    }
                }
                result = ChangeRow(a, r, index);*/
                for(int rr=r+1;rr<a.Length; rr++)
                {
                    float coef = -result[rr][r] / result[r][r];
                    result = AddRows(result, r, rr, coef);
                }
            }
            /*
            string amca = "";
            for(int r = 0; r < result.Length; r++)
            {
                for(int c = 0; c < result[r].Length; c++)
                {
                    amca+=result[r][c]+" ";
                }
                amca += "\n";
            }
            amca += "\n";
            Console.WriteLine(amca);*/
            return result;
        }
        public static float[][] Equalize(float[][] a)
        {
            float[][] result=new float[a.Length][];
            for(int r = 0; r < a.Length; r++)
            {
                result[r] = new float[a[r].Length];
                for (int c = 0; c < a[r].Length; c++)
                    result[r][c] = a[r][c];
            }
            return result;
        }
        public static float Determinant(float[][] a)
        {
            float det = 1;
            for (int r = 0; r < a.Length; r++)
            {
                det *= a[r][r];
            }
            if (det < 0)
                det *= -1;
            det=MathF.Round(det,2);

            return det;
        }
        public static bool InArea(Shapes.Triangle tri,Vector p)
        {
            float[][] matrix = new float[3][];
            matrix[0] = new float[3] { tri.dots[0].v_pos.x, tri.dots[0].v_pos.y, 1 };
            matrix[1] = new float[3] { tri.dots[1].v_pos.x, tri.dots[1].v_pos.y, 1 };
            matrix[2] = new float[3] { tri.dots[2].v_pos.x, tri.dots[2].v_pos.y, 1 };
            float[][] ref_matrix = REF(matrix);
            float det=Determinant(ref_matrix);
            float v_det = 0;
            for(int i = 0; i < 3; i++)
            {
                float[][] v_matrix = Equalize(matrix);
                v_matrix[i] = new float[3] { p.x, p.y, 1 };
                v_matrix = REF(v_matrix);
                v_det += Determinant(v_matrix);
            }
            if (v_det == det)
                return true;
            else
                return false;
        }
        public static float Distance(Vector p1, Vector p2)
        {
            float dis = 0;
            for(int i = 0; i < p1.dim; i++)
            {
                dis += (p2.vector[i] - p1.vector[i]) * (p2.vector[i] - p1.vector[i]);
            }
            dis = MathF.Sqrt(dis);
            return dis;
        }
        /*
        public static float[] Rotation(Vector victim, Vector about, float deg,int axis)
        {
            int dim = victim.dim;
            float[][] rot_mat = new float[dim][];
            for (int i = 0; i < dim; i++)
                rot_mat[i] = new float[dim];
            int p = 0;
            for(int r = 0; r < dim; r++)
            {
                for(int c = 0; c < dim; c++)
                {
                    if(r==axis||c==axis)
                        rot_mat[r][c] = 0;
                    else
                    {
                        if (p == 0)
                        {
                            rot_mat[r][c] = 0;
                        }
                    }

                }
            }
        }*/
    }
}

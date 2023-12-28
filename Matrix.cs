using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oH_mY_goD
{
    internal class Matrix
    {
        public int[] size;
        public Dot[][] matrix;
        public string[][] s_matrix;
        public string image="";
        public Matrix(int[] size)
        {
            this.size = size; 
            matrix = new Dot[size[0]][];
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = new Dot[size[1]];
            }
            s_matrix = new string[size[0]][];
            for (int i = 0; i < s_matrix.Length; i++)
            {
                s_matrix[i] = new string[size[1]+2];
                if (i != s_matrix.Length - 1)
                    s_matrix[i][s_matrix[i].Length - 1] = "\n";
                else
                    s_matrix[i][s_matrix[i].Length - 1] = " ";
                s_matrix[i][s_matrix[i].Length - 2] = "|";
            }
            World.matrix = this;
        }
        public void Update()
        {
            Clear();
        }

        void Clear()
        {
            for(int y = 0; y < matrix.Length; y++)
            {
                for (int x = 0; x < matrix[y].Length; x++)
                {
                    matrix[y][x] = null;
                    s_matrix[y][x] = " ";                 
                }
            }
        }
        public void PutTheShoe()
        {
            for (int y = 0; y < matrix.Length; y++)
            {
                for (int x = 0; x < matrix[y].Length; x++)
                {
                    if (matrix[y][x] != null)
                        s_matrix[y][x] = "*";
                }
            }
            to_string();
            Console.Write(image);
        }
        public void to_string()
        {
            image = "";
            for (int y = 0; y < s_matrix.Length; y++)
            {
                for (int x = 0; x < s_matrix[y].Length; x++)
                {
                    image+=s_matrix[y][x];
                }
            }
        }
    }
}

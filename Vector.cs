using System;

public class Vector
{
    public float[] vector;
	public Vector(params float[] vector)
    {
        this.vector=new float[vector.Length];
        this.vector = vector;
    }
    public void Normalize()
    {
        float magnitude = 0;
        for (int i = 0; i < vector.Length; i++)
        {
            magnitude = vector[i] * vector[i];
        }
        magnitude = MathF.Sqrt(magnitude);
        for(int i = 0; i < vector.Length; i++)
        {
            vector[i] /= magnitude;
        }
    }
    public int dim { get { return vector.Length; } }
    public float x { get { return vector[0]; } set { vector[0] = value; } }
    public float y { get { return vector[1]; } set { vector[1] = value; } }
    public float z { get { return vector[2]; } set { vector[2] = value; } }
}

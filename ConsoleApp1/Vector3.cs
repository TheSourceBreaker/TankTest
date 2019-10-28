using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Vector3
    {
        public float x, y, z;


        public Vector3()
        {
            x = 0;
            y = 0;
            z = 0;
        }
        public Vector3(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }
        public Vector3(float _x)
        {
            x = _x;

        }

        public static Vector3 Min(Vector3 a, Vector3 b)
        {
            return new Vector3(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z));
        }
        public static Vector3 Max(Vector3 a, Vector3 b)
        {
            return new Vector3(Math.Max(a.x, b.x), Math.Max(a.y, b.y), Math.Max(a.z, b.z));
        }
        public static Vector3 Clamp(Vector3 t, Vector3 a, Vector3 b)
        {
            return Max(a, Min(b, t));
        }
        

        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);                //V = V + V
        }

        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);                //V = V - V
        }

        public static Vector3 operator *(Vector3 lhs, float rhs)
        {
            return new Vector3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);                      //V = V * F
        }

        public static Vector3 operator *(float lhs, Vector3 rhs)
        {
            return new Vector3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);                      //V = F * V
        }


        public static Vector3 operator *(Matrix3 lhs, Vector3 rhs)
        {

            return new Vector3((lhs.m1 * rhs.x) + (lhs.m4 * rhs.y) + (lhs.m7 * rhs.z),
                               (lhs.m2 * rhs.x) + (lhs.m5 * rhs.y) + (lhs.m8 * rhs.z),      //V = M * V
                               (lhs.m3 * rhs.x) + (lhs.m6 * rhs.y) + (lhs.m9 * rhs.z));
        }

        public float Dot(Vector3 rhs)
        {
            return (x * rhs.x) + (y * rhs.y) + (z * rhs.z);              //f = V.Dot( V )                          
        }

        public Vector3 Cross(Vector3 rhs)
        {
            return new Vector3(y * rhs.z - z * rhs.y, z * rhs.x - x * rhs.z, x * rhs.y - y * rhs.x);    //V = V.Cross( V )
        }


        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);     //f = V.Magnitude()  
        }

        public float Normalize()
        {
            float m = Magnitude();                              //V.Normalise() 
            x /= m;
            y /= m;
            z /= m;
            return m;
        }
    }
}

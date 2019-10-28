using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class AABB
    {
        public Vector3 min = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

        public Vector3 max = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

        public AABB()
        {}

        public AABB(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
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


        public void Fit(List<Vector3> points)
        {
            
            min = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

            max = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
            
            foreach (Vector3 p in points)
            {
                min = Vector3.Min(min, p);

                max = Vector3.Max(max, p);
            }
        }


        public bool Overlaps(Vector3 p)
        {
            return !(p.x < min.x || p.y < min.y || p.x > max.x || p.y > max.y);
        }

        public bool Overlaps(AABB other)
        {
            return !(max.x < other.min.x || max.y < other.min.y || min.x > other.max.x || min.y > other.max.y);
        }

        public Vector3 Center()
        {
            return (min + max) * 0.5f;
        }

        public Vector3 Extents()
        {
            return new Vector3(Math.Abs(max.x - min.x) * 0.5f, Math.Abs(max.y - min.y) * 0.5f, Math.Abs(max.z - min.z) * 0.5f);
        }

        public Vector3 ClosestPoint(Vector3 p)
        {
            return Vector3.Clamp(p, min, max);
        }

        public bool IsEmpty()
        {
            if (float.IsNegativeInfinity(min.x) && float.IsNegativeInfinity(min.y) && float.IsNegativeInfinity(min.z) && float.IsInfinity(max.x) && float.IsInfinity(max.y) && float.IsInfinity(max.z))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void Empty()
        {
            min = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

            max = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        }


        void SetToTransformedBox(AABB box, Matrix3 m)
        {
            // If we're empty, then exit (an empty box defined as having the min/max set to infinity)

            if (box.IsEmpty())
            {
                Empty();

                return;
            }

            // Examine each of the nine matrix elements
            // and compute the new AABB


            if (m.m1 > 0.0f) // m1 = m11 in the formula above
            {
                min.x += m.m1 * box.min.x; max.x += m.m1 * box.max.x;
            }
            else
            {
                min.x += m.m1 * box.max.x; max.x += m.m1 * box.min.x;
            }


            if (m.m2 > 0.0f) // m2 = m12 in the formula above
            {
                min.y += m.m2 * box.min.x; max.y += m.m2 * box.max.x;
            }
            else
            {
                min.y += m.m2 * box.max.x; max.y += m.m2 * box.min.x;
            }


            if (m.m3 > 0.0f) // m3 = m13 in the formula above
            {
                min.z += m.m3 * box.min.x; max.z += m.m3 * box.max.x;
            }
            else
            {
                min.z += m.m3 * box.max.x; max.z += m.m3 * box.min.x;
            }


            if (m.m1 > 0.0f) // m4 = m21 in the formula above
            {
                min.x += m.m4 * box.min.x; max.x += m.m4 * box.max.x;
            }
            else
            {
                min.x += m.m4 * box.max.x; max.x += m.m4 * box.min.x;
            }


            if (m.m2 > 0.0f) // m5 = m22 in the formula above
            {
                min.y += m.m5 * box.min.x; max.y += m.m5 * box.max.x;
            }
            else
            {
                min.y += m.m5 * box.max.x; max.y += m.m5 * box.min.x;
            }


            if (m.m3 > 0.0f) // m6 = m23 in the formula above
            {
                min.z += m.m6 * box.min.x; max.z += m.m6 * box.max.x;
            }
            else
            {
                min.z += m.m6 * box.max.x; max.z += m.m6 * box.min.x;
            }


            if (m.m1 > 0.0f) // m7 = m31 in the formula above
            {
                min.x += m.m7 * box.min.x; max.x += m.m7 * box.max.x;
            }
            else
            {
                min.x += m.m7 * box.max.x; max.x += m.m7 * box.min.x;
            }


            if (m.m2 > 0.0f) // m8 = m32 in the formula above
            {
                min.y += m.m8 * box.min.x; max.y += m.m8 * box.max.x;
            }
            else
            {
                min.y += m.m8 * box.max.x; max.y += m.m8 * box.min.x;
            }


            if (m.m3 > 0.0f) // m9 = m33 in the formula above
            {
                min.z += m.m9 * box.min.x; max.z += m.m9 * box.max.x;
            }
            else
            {
                min.z += m.m9 * box.max.x; max.z += m.m9 * box.min.x;
            }
        }
    }
}

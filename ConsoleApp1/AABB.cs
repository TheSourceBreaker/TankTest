using System;
using System.Collections.Generic;
using System.Text;
using Raylib;
using static Raylib.Raylib;

namespace ConsoleApp1
{
    class AABB
    {
        public Vector3 min = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
        public Vector3 max = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        public Color color = Color.RED;

        readonly List<Vector3> corners = new List<Vector3>(4);

        public AABB()
        {
            corners.Add(new Vector3());
            corners.Add(new Vector3());
            corners.Add(new Vector3());
            corners.Add(new Vector3());
        }

        public AABB(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
            corners.Add(new Vector3());
            corners.Add(new Vector3());
            corners.Add(new Vector3());
            corners.Add(new Vector3());

        }

        public Vector3 Center()
        {                                       //This is used to find the center of the box by finding the point
                                                //in between the min and max.
                return (min + max) * 0.5f;
        }

        public Vector3 Extents()
        {
            return new Vector3(Math.Abs(max.x - min.x) * 0.5f,  //Used to calculate half-extents by subtracting 
                               Math.Abs(max.y - min.y) * 0.5f,  //the min point from the max point, then halving
                               Math.Abs(max.z - min.z) * 0.5f); //the absolute value for each vector component.
        }

        public List<Vector3> Corners()
        {
                                                     //This method is useful with returning the corners
                                                     // of the box, using min and max.
            corners[0] = min;
            corners[1] = new Vector3(min.x, max.y, min.z);
            corners[2] = max;
            corners[3] = new Vector3(max.x, min.y, min.z);
            return corners;
        }

        public void Fit(List<Vector3> points)
        {
                                                          //To fit an AABB to a collection of points we must
                                                          //first invalidate our current min and max by setting
                                                          //min to the largest value possible, and by setting
                                                          //max to the smallest value possible. 

            min = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
            max = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

            foreach (Vector3 p in points)
            {
                min = Vector3.Min(min, p);                 //Then loop through all of the points and find the 
                                                           //min() and  max().
                max = Vector3.Max(max, p);
            }
        }


        public bool Overlaps(Vector3 p) 
        {                                                                        //This is used to check if the point is in range of 
            return !(p.x < min.x || p.y < min.y || p.x > max.x || p.y > max.y);  //the min and max corners. (point overlaps an AABB)
        }


        public bool Overlaps(AABB other)
        {                                                 //This is used to check if the point is in range of
                                                          //another AABB. (AABB overlaps another AABB)
            return !(max.x < other.min.x || max.y < other.min.y || min.x > other.max.x || min.y > other.max.y);
        }



        public Vector3 ClosestPoint(Vector3 p)
        {                                                 //This is used to find the closest point on the
                                                          //surface of an arbitrary point. This clamps the
            return Vector3.Clamp(p, min, max);            //arbitrary point to the min and max corners of
                                                          //the box.
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


            if (m.m4 > 0.0f) // m4 = m21 in the formula above
            {
                min.x += m.m4 * box.min.x; max.x += m.m4 * box.max.x;
            }
            else
            {
                min.x += m.m4 * box.max.x; max.x += m.m4 * box.min.x;
            }


            if (m.m5 > 0.0f) // m5 = m22 in the formula above
            {
                min.y += m.m5 * box.min.x; max.y += m.m5 * box.max.x;
            }
            else
            {
                min.y += m.m5 * box.max.x; max.y += m.m5 * box.min.x;
            }


            if (m.m6 > 0.0f) // m6 = m23 in the formula above
            {
                min.z += m.m6 * box.min.x; max.z += m.m6 * box.max.x;
            }
            else
            {
                min.z += m.m6 * box.max.x; max.z += m.m6 * box.min.x;
            }


            if (m.m7 > 0.0f) // m7 = m31 in the formula above
            {
                min.x += m.m7 * box.min.x; max.x += m.m7 * box.max.x;
            }
            else
            {
                min.x += m.m7 * box.max.x; max.x += m.m7 * box.min.x;
            }


            if (m.m8 > 0.0f) // m8 = m32 in the formula above
            {
                min.y += m.m8 * box.min.x; max.y += m.m8 * box.max.x;
            }
            else
            {
                min.y += m.m8 * box.max.x; max.y += m.m8 * box.min.x;
            }


            if (m.m9 > 0.0f) // m9 = m33 in the formula above
            {
                min.z += m.m9 * box.min.x; max.z += m.m9 * box.max.x;
            }
            else
            {
                min.z += m.m9 * box.max.x; max.z += m.m9 * box.min.x;
            }
        }

        public void OnDraw()
        {
            Corners();
            DrawLine((int)corners[0].x, (int)corners[0].y, (int)corners[1].x, (int)corners[1].y, color);
            DrawLine((int)corners[1].x, (int)corners[1].y, (int)corners[2].x, (int)corners[2].y, color);
            DrawLine((int)corners[2].x, (int)corners[2].y, (int)corners[3].x, (int)corners[3].y, color);
            DrawLine((int)corners[3].x, (int)corners[3].y, (int)corners[0].x, (int)corners[0].y, color);

            
        }
    }
}

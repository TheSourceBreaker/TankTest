using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Ray
    {
        Vector3 origin;
        Vector3 direction;
        float length;

        public Ray()
        {}

        public Ray(Vector3 start, Vector3 direction, float length = float.MaxValue)
        {
            this.origin = start;
            this.direction = direction;
            this.length = length;
        }

        float Clamp(float t, float a, float b)
        {
            return Math.Max(a, Math.Min(b, t));
        }
        public Vector3 closestPoint(Vector3 point)
        {
            Vector3 p = point - origin;
            float t = Clamp(p.Dot(direction), 0, length);
            return origin + direction * t;
        }
        public bool Intersects(AABB aabb, Vector3 I = null)
        {
            float xmin, xmax, ymin, ymax;
            if (direction.x < 0)
            {
                xmin = (aabb.max.x - origin.x) / direction.x;
                xmax = (aabb.min.x - origin.x) / direction.x;
            }
            else
            {
                xmin = (aabb.min.x - origin.x) / direction.x;
                xmax = (aabb.max.x - origin.x) / direction.x;
            }

            if (direction.y < 0)
            {
                ymin = (aabb.max.y - origin.y) / direction.y;
                ymax = (aabb.min.y - origin.y) / direction.y;
            }
            else
            {
                ymin = (aabb.min.y - origin.y) / direction.y;
                ymax = (aabb.max.y - origin.y) / direction.y;
            }

            if (xmin > ymax || ymin > xmax)
                return false;

            float t = Math.Max(xmin, ymin);
            if (t >= 0 && t <= length)
            {
                if (I != null)
                    I = origin + direction * t;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

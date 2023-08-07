using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZUtils
{
    public static class MathUtils
    {
        // based on https://github.com/zalo/MathUtilities
        public static void FitLine(List<Vector3> points, out Vector3 origin,
            ref Vector3 direction, int iters = 100, bool drawGizmos = false) {
            if (direction == Vector3.zero || float.IsNaN(direction.x) || float.IsInfinity(direction.x)) 
                direction = Vector3.up;

            //Calculate Average
            origin = Vector3.zero;
            for (int i = 0; i < points.Count; i++) origin += points[i];
            origin /= points.Count;

            // Step the optimal fitting line approximation:
            for (int iter = 0; iter < iters; iter++) {
                Vector3 newDirection = Vector3.zero;
                foreach (Vector3 worldSpacePoint in points) {
                    Vector3 point = worldSpacePoint - origin;
                    newDirection += Vector3.Dot(direction, point) * point;
                }
                direction = newDirection.normalized;
            }

            if (drawGizmos) 
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(origin, direction * 2f);
                Gizmos.DrawRay(origin, -direction * 2f);
            }
        }

        /// <summary>Average distance between a set of points and a line</summary>
        public static float AverageDistance(List<Vector3> points, Vector3 lineOrigin, Vector3 lineDir) =>
            points.Average(point => Vector3.Distance(Vector3.Project(point - lineOrigin, lineDir.normalized), point - lineOrigin));
    }
}
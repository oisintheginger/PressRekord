using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This implementation of a bezier curve was found at: https://www.gamasutra.com/blogs/VivekTank/20180806/323709/How_to_work_with_Bezier_Curve_in_Games_with_Unity.php
public static class BezierCalculator
{
    public static List<Vector2> ProjectileArcCalculation(Vector2 TargetPos, Vector2 StartPosition, int CurveSegments, float ControlPointHeightA = 5f, float ControlPointHeightB = 5f)
    {
        //Vector2[] ProjectileControlPoints = new Vector2[4];
        List<Vector2> ProjectileControlPoints = new List<Vector2>();
        for (int i = 0; i < 4; i++)
        {
            Vector2 n = new Vector2();
            ProjectileControlPoints.Add(n);
        }
        Vector2 Distance = TargetPos - StartPosition;
        ProjectileControlPoints[0] = StartPosition;
        ProjectileControlPoints[1] = (Vector2)StartPosition + new Vector2(Distance.x * .1f, ControlPointHeightA);
        ProjectileControlPoints[2] = (Vector2)TargetPos + new Vector2(Distance.x * -.1f, ControlPointHeightB);
        ProjectileControlPoints[3] = TargetPos;
        int CurveCount = (int)ProjectileControlPoints.Count / 3;
        List<Vector2> ProjectileCurvePositions = new List<Vector2>();
        for (int j = 0; j < CurveCount; j++)
        {
            for (int i = 1; i <= CurveSegments; i++)
            {
                float t = i / (float)CurveSegments;
                int nodeIndex = j;
                Vector2 point = CalculateCubicBezierPoint(t, ProjectileControlPoints[nodeIndex], ProjectileControlPoints[nodeIndex + 1], ProjectileControlPoints[nodeIndex + 2], ProjectileControlPoints[nodeIndex + 3]);
                ProjectileCurvePositions.Add(point);
            }
        }
        return ProjectileCurvePositions;
    }



    public static Vector2 CalculateCubicBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector2 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }
}

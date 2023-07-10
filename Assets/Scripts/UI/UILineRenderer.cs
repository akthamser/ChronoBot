using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UILineRenderer : Graphic
{

    public float Thickness = 4;
    public List<Vector2> Points;

    public bool Closed;
    //Note(Simon): These buffers are allocated once, and reused each frame.

    protected override void OnPopulateMesh(VertexHelper vh)
    {

        DrawLines(vh);
        
    }


    public void DrawLines(VertexHelper vh)
    {
        vh.Clear();

        if (Points.Count>1)
        {
            DrawFirstLine(Points, Thickness, vh);

            int reach = Closed ? Points.Count - 1 : Points.Count;
            for (int i = 1; i < reach; i++)
            {
                DrawNextPoint(Points, i, Thickness, vh);
            }

            if (Closed)
                CloseLines(Points, Thickness, vh);
        }
    }

    public void SetPoints(List<Vector2> points)
    {
        Points = points;
        DrawLines(new VertexHelper());
        UpdateGeometry();
    }

    private void DrawFirstLine(List<Vector2> Points,float Thickness,VertexHelper vh)
    {
        Vector2 lineDir = (Points[0] - Points[1]).normalized;
        Vector2 pointDir = Quaternion.AngleAxis(90, Vector3.forward) * lineDir;

        float forwardEcart = 0;
        float rightEcart = 0;
        if (Closed)
        {
            float Angle = Vector3.SignedAngle(Points[0] - Points[1], Points[Points.Count -1] - Points[0], Vector3.forward);
            forwardEcart = Angle / 90 * Thickness / 2;

            //Debug.Log(Vector3.SignedAngle(Points[PointIndex] - Points[PointIndex + 1], Points[PointIndex - 1] - Points[PointIndex], Vector3.forward));

            if (Angle > 90)
                rightEcart = Thickness / 2 - forwardEcart;
            if (Angle < -90)
                rightEcart = Thickness / 2 + forwardEcart;
        }

        UIVertex vert1 = new UIVertex();
        vert1.color = color;
        vert1.position = Points[0] - lineDir * forwardEcart + pointDir * Thickness / 2 + pointDir * rightEcart;
        vh.AddVert(vert1);

        UIVertex vert2 = new UIVertex();
        vert2.color = color;
        vert2.position = Points[0] + lineDir * forwardEcart - pointDir * Thickness / 2 - pointDir * rightEcart;
        vh.AddVert(vert2);

    }

    private void DrawNextPoint(List<Vector2> Points, int PointIndex, float Thickness, VertexHelper vh)
    {
        int vCount = vh.currentVertCount;
        Vector2 lineDir = (Points[PointIndex - 1] - Points[PointIndex]).normalized;
        Vector2 pointDir = Quaternion.AngleAxis(90, Vector3.forward) * lineDir;
        float forwardEcart = 0;
        float rightEcart = 0;
        if (PointIndex< Points.Count - 1)
        {
            float Angle = Vector3.SignedAngle(Points[PointIndex] - Points[PointIndex + 1], Points[PointIndex - 1] - Points[PointIndex], Vector3.forward);
            forwardEcart = Angle / 90  * Thickness/2;

            //Debug.Log(Vector3.SignedAngle(Points[PointIndex] - Points[PointIndex + 1], Points[PointIndex - 1] - Points[PointIndex], Vector3.forward));
       
            if (Angle > 90  )
                rightEcart = Thickness / 2 - forwardEcart;
            if(Angle < -90)
                rightEcart = Thickness/2 + forwardEcart;
        }


        UIVertex vert3 = new UIVertex();
        vert3.color = color;
        vert3.position = Points[PointIndex] + lineDir * forwardEcart + pointDir * Thickness / 2 + pointDir * rightEcart;
        vh.AddVert(vert3);

        UIVertex vert4 = new UIVertex();
        vert4.color = color;
        vert4.position = Points[PointIndex] - lineDir * forwardEcart - pointDir * Thickness / 2 - pointDir * rightEcart;
        vh.AddVert(vert4);

        vh.AddTriangle(vCount - 2, vCount, vCount - 1);
        vh.AddTriangle(vCount - 1, vCount + 1, vCount);
    }


    public void CloseLines(List<Vector2> Points, float Thickness, VertexHelper vh)
    {
        int vCount = vh.currentVertCount;
        int PointIndex = Points.Count - 1;

        Vector2 lineDir = (Points[PointIndex - 1] - Points[PointIndex]).normalized;
        Vector2 pointDir = Quaternion.AngleAxis(90, Vector3.forward) * lineDir;
        float forwardEcart = 0;
        float rightEcart = 0;
        if (Closed)
        {
            float Angle = Vector3.SignedAngle(Points[PointIndex] - Points[0], Points[PointIndex - 1] - Points[PointIndex], Vector3.forward);
            forwardEcart = Angle / 90 * Thickness / 2;

            //Debug.Log(Vector3.SignedAngle(Points[PointIndex] - Points[PointIndex + 1], Points[PointIndex - 1] - Points[PointIndex], Vector3.forward));

            if (Angle > 90)
                rightEcart = Thickness / 2 - forwardEcart;
            if (Angle < -90)
                rightEcart = Thickness / 2 + forwardEcart;
        }


        UIVertex vert3 = new UIVertex();
        vert3.color = color;
        vert3.position = Points[PointIndex] + lineDir * forwardEcart + pointDir * Thickness / 2 + pointDir * rightEcart;
        vh.AddVert(vert3);

        UIVertex vert4 = new UIVertex();
        vert4.color = color;
        vert4.position = Points[PointIndex] - lineDir * forwardEcart - pointDir * Thickness / 2 - pointDir * rightEcart;
        vh.AddVert(vert4);

        vh.AddTriangle(vCount - 2, vCount, vCount - 1);
        vh.AddTriangle(vCount - 1, vCount + 1, vCount);

        if (Closed)
        {
            vh.AddTriangle(0, vCount, 1);
            vh.AddTriangle(1, vCount + 1, vCount);
        }
    }
}

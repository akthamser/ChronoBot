using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float Radius = 10f;
    public float Angle = 90f;
    public bool isTargetVisible = false;
    public Vector3 RayCastsOffset = new Vector3(0,0.5f,0);
    [Header("Visulization")]
    public Vector3 fieldOfViewOffset;
    public float RaysAngle = 10;
    public LayerMask HitLayers;
    public Material VisulizeMaterial;
    public bool IsTargetVisible()
    {
        return isTargetVisible;
    }

    private void Start()
    {
        SetupFieldOfViewVisulizer();
    }

    private void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, MovmentController.Instance.transform.position);
        Vector3 directionToTarget = (MovmentController.Instance.transform.position - transform.position).normalized;

        float angleToTarget = Vector3.Angle(from: transform.forward, directionToTarget);

        if (distanceToTarget <= Radius && angleToTarget <= Angle / 2f)
        {
            Ray ray = new Ray(transform.position + RayCastsOffset, (MovmentController.Instance.transform.position - transform.position - RayCastsOffset).normalized);
            Debug.DrawRay(ray.origin, ray.direction * 20);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Radius) && hit.transform.gameObject == MovmentController.Instance.gameObject)
                isTargetVisible = true;
            else
                isTargetVisible = false;
        }
        else
        {
            isTargetVisible = false;
        }

        VisulizeFieldOfView();
    }

    private GameObject FieldOfViewVisulizer;
    private MeshFilter mesh;
    private MeshRenderer meshRenderer;
    private List<Vector3> Verticies;
    private List<int> Indices;
    public void VisulizeFieldOfView()
    {
        Verticies = new List<Vector3>();
        Indices = new List<int>();

        Verticies.Add(Vector3.zero);
        
        float CurrentAngle = Angle/2 ;
        for (int i = 0; i < Angle /RaysAngle +2; i++)
        {
            float radian = CurrentAngle * Mathf.Deg2Rad;

            Vector3 Point = new Vector3(Mathf.Sin(radian) * Radius , 0, Mathf.Cos(radian) * Radius);

            Vector3 Direction = FieldOfViewVisulizer.transform.TransformPoint(Point) - FieldOfViewVisulizer.transform.position;
            Ray ray = new Ray(FieldOfViewVisulizer.transform.position, Direction);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, Radius, HitLayers,QueryTriggerInteraction.Ignore))
                Verticies.Add(FieldOfViewVisulizer.transform.InverseTransformPoint(hit.point));
            else 
                Verticies.Add(Point);



            if (i > 1)
            {
                Indices.Add(0);
                Indices.Add(i - 1);
                Indices.Add(i);
            }
            //GameObject obj = new GameObject(i.ToString());
            //obj.transform.parent = FieldOfViewVisulizer.transform;
            //obj.transform.localPosition = Point;


            mesh.mesh.Clear();
            mesh.mesh.SetVertices(Verticies);
            mesh.mesh.SetIndices(Indices, MeshTopology.Triangles, 0);
            //Indices

            CurrentAngle -= RaysAngle;
        }
    }

    public void SetupFieldOfViewVisulizer()
    {
        GameObject FieldOfView = new GameObject("Field of view");
        FieldOfView.transform.parent = transform;
        FieldOfView.transform.localPosition =fieldOfViewOffset;
        FieldOfView.transform.eulerAngles = new Vector3(0, 0, 180);
        mesh = FieldOfView.AddComponent<MeshFilter>();
        meshRenderer = FieldOfView.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = VisulizeMaterial;
        FieldOfViewVisulizer = FieldOfView;
    }
}

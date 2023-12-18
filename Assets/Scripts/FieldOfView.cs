using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float fov = 90f;
    [SerializeField]
    private int raycount = 50;
    [SerializeField]
    private float viewDistance = 10f;


    private Vector3 origin = Vector3.zero;
    private Mesh mesh;
    private float angle;

    

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void LateUpdate()
    {
        float angleIncrease = fov / raycount;

        Vector3[] vertices = new Vector3[raycount +2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[raycount*3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0;  i <= raycount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetAngleFromVector(angle), viewDistance, layerMask);
            

            if (raycastHit2D.collider == null)
            {
                vertex = origin + GetAngleFromVector(angle) * viewDistance;
            }
            else
            {
                vertex = raycastHit2D.point;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;

            angle -= angleIncrease;
        }

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
    }

    private Vector3 GetAngleFromVector(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
    private float GetAngleFromVectorFloat(Vector3 dir)
    {
        Vector3 normDir = new Vector3(dir.x, dir.y, dir.z).normalized;
        float n = Mathf.Atan2(normDir.y, normDir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void setAimDirection (Vector3 aimDirection)
    {
        angle = GetAngleFromVectorFloat(aimDirection) - fov / 2f + 90;
    }

}

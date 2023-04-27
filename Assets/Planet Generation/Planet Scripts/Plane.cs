using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{

    [Range (2,256)]
    public int xresolution = 4;
    public int yresolution;
    
    [SerializeField, HideInInspector]
    MeshFilter meshFilter;
    Mesh mesh;
    
    Vector3 axisA = new Vector3(Vector3.up.y, Vector3.up.z, Vector3.up.x);
    Vector3 axisB = Vector3.Cross(Vector3.up, new Vector3(Vector3.up.y, Vector3.up.z, Vector3.up.x));

    Renderer planerenderer;
    
    void OnValidate()
    {
        Initialize();
        ConstructMesh();
    }

    void Initialize() 
    {
        yresolution = (int)(xresolution / 2);
        if (meshFilter == null) {
            meshFilter = new MeshFilter();
            GameObject meshObject = new GameObject("plane");
            meshObject.transform.parent = transform;

            meshObject.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
            meshFilter = meshObject.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = new Mesh();

            planerenderer = meshObject.GetComponent<Renderer>();
        }
    }

    public void ConstructMesh() 
    {
        mesh = meshFilter.sharedMesh;
        Vector3[] vertices = new Vector3[xresolution * yresolution]; // total amount of vertices in mesh = r^2
        Vector2[] uvs = new Vector2[xresolution * yresolution]; // total amount of uv coordinates
        int[] triangles = new int [(xresolution - 1) * (yresolution - 1) * 2 * 3]; // total amount of triangle vertices
        int triIndex = 0;

        for (int y = 0; y < yresolution; y++) {
            for (int x = 0; x < xresolution; x++) {
                int i = x + y * xresolution; // counter
                Vector2 percent = new Vector2(x, y); // percentage of completion as x and y go up when done percent will equal one
                percent.x = percent.x / (xresolution - 1);
                percent.y = percent.y / (yresolution - 1);
                Vector3 pointOnUnitCube = Vector3.up + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                vertices[i] = pointOnUnitCube; // storing all vertice points in array
                uvs[i] = new Vector2((float)x / xresolution, (float)y / yresolution);

                // Saving triangle vertice indexes in clockwise direction
                if (x != xresolution - 1 && y != yresolution - 1) { // not at right end or bottom end of mesh 
                    // first triangle
                    triangles[triIndex + 0] = i;
                    triangles[triIndex + 1] = i + xresolution + 1;
                    triangles[triIndex + 2] = i + xresolution;
                    // second triangle 
                    triangles[triIndex + 3] = i;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + xresolution + 1;

                    triIndex += 6;
                }
            }
        }
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }

}

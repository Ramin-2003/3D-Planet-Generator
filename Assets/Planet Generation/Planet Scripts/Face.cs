using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face
{
    ShapeGenerator shapeGenerator;
    Mesh mesh;
    int resolution; // amount of vertices along one edge
    Vector3 localUp; // surface normal
    Vector3 axisA; // side of a mesh
    Vector3 axisB; // other side of a mesh
    float[] colorArray; // color heights

    public Face(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp, float[] colorArray)
    {
        this.colorArray = colorArray;
        this.shapeGenerator = shapeGenerator;
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x); // Calculating direction of side of mesh
        axisB = Vector3.Cross(localUp, axisA); // calculating direction of other side of mesh (line parrallel to axisa and localup)
    }

    public void ConstructMesh() 
    {
        Vector3[] vertices = new Vector3[resolution * resolution]; // total amount of vertices in mesh = r^2
        int[] triangles = new int [(resolution-1) * (resolution-1) * 2 * 3]; // calculating amount of triangle vertices
        int triIndex = 0;
        Vector3[] edges = new Vector3[resolution];

        for (int y = 0; y < resolution; y++) {
            for (int x = 0; x < resolution; x++) {
                int i = x + y * resolution; // index of vertice
                Vector2 percent = new Vector2(x, y) / (resolution - 1); // percentage of completion as x and y go up when done percent will equal one
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB; // which vertice we are at on cube
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized; // "inflating" cube into sphere by normalizing each vertice

                pointOnUnitSphere = pointOnUnitSphere * colorArray[i]; // multiplying by color height data

                vertices[i] = shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere); // storing all vertice points in array

                // Saving triangle vertice indexes in clockwise direction
                if (x != resolution-1 && y != resolution-1) { // not at right end or bottom end of mesh 
                    // first triangle
                    triangles[triIndex] = i;
                    triangles[triIndex + 1] = i + resolution + 1;
                    triangles[triIndex + 2] = i + resolution;
                    // second triangle 
                    triangles[triIndex + 3] = i;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + resolution + 1;

                    triIndex += 6;
                }
            }
        }
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

}
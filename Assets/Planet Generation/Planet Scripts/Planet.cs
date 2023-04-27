using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Planet : MonoBehaviour
{
    [Range(2,256)]
    public int resolution = 10;

    public ShapeSettings shapeSettings;
    public ColourSettings colourSettings;
    
    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColorGenerator colorGenerator = new ColorGenerator();

    private TextureManage textureManage;
    private Mapping mapping;

    [SerializeField, HideInInspector] // serializing mesh filters so it is saved
    MeshFilter[] meshFilters;
    Face[] faces;

    private void Start()
    {
        GeneratePlanet();
    }

    void Initialize() 
    {
        shapeGenerator.UpdateSettings(shapeSettings);
        colorGenerator.UpdateSettings(colourSettings);

        textureManage = GetComponent<TextureManage>();

        mapping = new Mapping(textureManage.xresolution, textureManage.yresolution);
       
        faces = new Face[6]; // array of faces
        if (meshFilters == null || meshFilters.Length == 0 ) { // only create new filters if dont already exist
            meshFilters = new MeshFilter[6]; // array of 6 mesh filters to go with faces
        }

        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.forward, Vector3.back, Vector3.left, Vector3.right}; // all directions of faces in cube
        var maps = new List<Func<Color[],float[]>>() {mapping.MapU, mapping.MapD, mapping.MapF, mapping.MapB, mapping.MapL, mapping.MapR}; // list of functions

        for (int i = 0; i < 6; i++) {
            if (meshFilters[i] == null) {
                GameObject meshObject = new GameObject("mesh"); // new game object "mesh" which is one face of cube each iteration
                meshObject.transform.parent = transform;

                meshObject.AddComponent<MeshRenderer>(); // adding shared material component
                meshFilters[i] = meshObject.AddComponent<MeshFilter>(); // adding mesh filter component to object
                meshFilters[i].sharedMesh = new Mesh(); // sharing mesh between all faces
            }
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colourSettings.planetMaterial;
            
            faces[i] = new Face(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i], maps[i](textureManage.arrayCol)); // filling array with 6 face objects
            if (i == 2) {transform.GetChild(i).localRotation = Quaternion.Euler(0, 0, -270);} // getting child of planet which is a single face mesh and rotating if forward face
            if (i == 3) {transform.GetChild(i).localRotation = Quaternion.Euler(0, 0, 90);}
        }
    }

    public void GeneratePlanet() 
    {
        Initialize();
        GenerateMesh();
        GenerateColours();
    }

    public void OnShapeSettingsUpdated() 
    {
        Initialize();
        GenerateMesh();
    }
    
    public void OnColourSettingsUpdated() 
    {
        Initialize();
        GenerateColours();
    }

    void GenerateMesh() 
    {
        foreach(Face face in faces) {
            face.ConstructMesh();
        }

        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColours() 
    {
        colorGenerator.UpdateColors();
    }
}

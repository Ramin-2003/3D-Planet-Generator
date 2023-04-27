using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    ShapeSettings settings;
    public MinMax elevationMinMax;

    public void UpdateSettings(ShapeSettings settings) {
        this.settings = settings;
        elevationMinMax = new MinMax();
    }

    public Vector3 CalculatePointOnPlanet (Vector3 pointOnUnitSphere) 
    {   
        float elevation = 0;
        elevation = pointOnUnitSphere.magnitude;
        elevation = settings.planetRadius * (elevation); 
        elevationMinMax.AddValue(elevation);
        return pointOnUnitSphere * elevation;
    }
}

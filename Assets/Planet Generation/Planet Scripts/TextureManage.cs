using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Unity.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TextureManage : MonoBehaviour
{
    public Color[] arrayCol;
    
    public int xresolution;
    public int yresolution;
    
    public Texture2D texture;

    void Awake()
    {
        texture = StaticPass.texturePass;

        xresolution = 16 * 32;
        yresolution = 8 * 32;

        arrayCol = new Color[xresolution * yresolution];  
        for (int y =  0; y < yresolution; y++) {
            for (int x = 0; x < xresolution; x++) {
                int i = x + y * xresolution; // counter
                arrayCol[i] = texture.GetPixel((texture.width / (xresolution)) * x, (texture.height / (yresolution)) * y); // getting pixel color at i
            }
        }
    }
}
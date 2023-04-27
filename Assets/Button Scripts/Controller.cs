using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public GameObject blur;

    public void finishdraw()
    {
        blur.SetActive(true);
        StartCoroutine(CoroutineScreenshot());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator CoroutineScreenshot() 
    {
        yield return new WaitForEndOfFrame();
        int width = Screen.width - 70;
        int height = width / 2;
        Texture2D screenshotTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);
        Rect rect = new Rect(35, 125, width, height); // Area of screen shot (1850 x 925 area) with bottom left of rectangle at (35,125)
        screenshotTexture.ReadPixels(rect, 0 , 0);
        screenshotTexture.Apply();

        byte[] byteArray = screenshotTexture.EncodeToPNG();
        StaticPass.texturePass.LoadImage(byteArray);
    }
}

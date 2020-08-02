///
/// @wtfmig
///
using UnityEngine;
using System.Collections;
 
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/PixelBoy")]
[RequireComponent(typeof(Camera))]
public class PixelBoy : MonoBehaviour
{
    public int w = 720;
    Camera cam;
    int h;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Update() {
 
        float ratio = ((float)cam.pixelHeight / (float)cam.pixelWidth);
        h = Mathf.RoundToInt(w * ratio);
       
    }
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        source.filterMode = FilterMode.Point;
        RenderTexture buffer = RenderTexture.GetTemporary(w, h, -1);
        buffer.filterMode = FilterMode.Point;
        Graphics.Blit(source, buffer);
        Graphics.Blit(buffer, destination);
        RenderTexture.ReleaseTemporary(buffer);
    }
}
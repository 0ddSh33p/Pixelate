using UnityEngine;

public class PixelRunner : MonoBehaviour
{
    public ComputeShader PixelateComputeShader;
    public Texture2D Colors;
    int numColors;

    int _screenWidth;
    int _screenHeight;
    RenderTexture _renderTexture;
     Vector4[] ExportColors = new Vector4[32];

    void Start()
    {
        CreateRenderTexture();
        numColors = Colors.width;

        for(int i = 0; i < Colors.width; i++){
            ExportColors[i] = (Colors.GetPixel(i, 0));
        }
    }

    void CreateRenderTexture()
    {
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
        
        _renderTexture = new RenderTexture(_screenWidth, _screenHeight, 24);
        _renderTexture.filterMode = FilterMode.Point;
        _renderTexture.enableRandomWrite = true;
        _renderTexture.Create();
    }

    void Update()
    {
        if (Screen.width != _screenWidth || Screen.height != _screenHeight)
            CreateRenderTexture();
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, _renderTexture);

        var mainKernel = PixelateComputeShader.FindKernel("Pixelate");
        PixelateComputeShader.SetInt("_BlockSize", 3);
        PixelateComputeShader.SetInt("_numColors", numColors);
        PixelateComputeShader.SetInt("_ResultWidth", _renderTexture.width);
        PixelateComputeShader.SetInt("_ResultHeight", _renderTexture.height);
        PixelateComputeShader.SetVectorArray("_Colors", ExportColors);
        PixelateComputeShader.SetTexture(mainKernel, "_Result", _renderTexture);
        PixelateComputeShader.GetKernelThreadGroupSizes(mainKernel, out uint xGroupSize, out uint yGroupSize, out _);
        PixelateComputeShader.Dispatch(mainKernel,
            Mathf.CeilToInt(_renderTexture.width / 2 / xGroupSize),
            Mathf.CeilToInt(_renderTexture.height / 2 / yGroupSize),
            1);

        Graphics.Blit(_renderTexture, dest);
    }
}
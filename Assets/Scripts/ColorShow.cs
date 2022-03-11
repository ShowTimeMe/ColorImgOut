using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorShow : MonoBehaviour
{
    public static ColorShow I = null;

    public Transform tfContent;
    public GameObject item;
    public Image image;
    public bool isFrame;

    float w = 256, h = 128;//图像宽高
    int pixelBlock = 20;//色块宽高
    int line = 0, column = 0;//行列
    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
        if (ColorImageOut.I.colorData.Count <= 0)
            return;

        ShowPreviewImage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 循环生成色块
    /// </summary>
    public void ShowPreviewImage()
    {
        image.sprite = Sprite.Create(new Texture2D((int)w + 2, (int)h), new Rect(1, 0, w - 1, h), new Vector2(0, 0));
        Color[] color = new Color[ColorImageOut.I.colorData.Count];
        for (int i = 0; i < ColorImageOut.I.colorData.Count; i++)
        {
            color[i] = ColorImageOut.I.colorData[i];
        }
        for (int index = 0; index < ColorImageOut.I.colorData.Count; index++)
        {
            PreviewImage(line, column, pixelBlock, color[index], isFrame);
            line++;
            if (line >= 12)
            {
                column++;
                line = 0;
            }
        }
    }
    /// <summary>
    /// 色块赋值颜色
    /// </summary>
    /// <param name="line"></param>
    /// <param name="column"></param>
    /// <param name="pixelBlock"></param>
    /// <param name="color"></param>
    public void PreviewImage(int line, int column,int pixelBlock, Color color,bool isFrame)
    {
        Color c = isFrame ? Color.black : color;
        for (int x = line * pixelBlock; x < (line + 1) * pixelBlock; x++)
        {
            for (int y = column * pixelBlock; y < (column + 1) * pixelBlock; y++)
            {
                image.sprite.texture.SetPixel(x, y, c);
            }
        }
        image.sprite.texture.Apply();
    }
}

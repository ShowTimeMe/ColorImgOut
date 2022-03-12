using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class ColorShow : MonoBehaviour
{
    public Transform tfContent;
    public GameObject itemGameObject;
    public Image image;
    public Text parameterText;
    public Toggle toggle;
    public InputField inputFieldPNGName;
    public Button btnOut;
    //private bool isFrame;
    

    private bool pubIs; 
    private byte[] bytes;
    private int spacing = 2;
    private int blockSpacing = 0;

    float w = 256, h = 128;//ͼ����
    int pixelBlock = 20;//ɫ����
    int line = 0, column = 0;//����

   
    private void Awake()
    {
        btnOut.onClick.AddListener(delegate
        {
            if (inputFieldPNGName.text == "")
            {
                Debug.Log("��������Ҫ������ļ���");
            }
            else
            {
                string path = "E:/Project/ColorImgOut/Assets/ImageOut/" + inputFieldPNGName.text;
                OnBtnOutPng(path);
            }
        });
    }
    // Start is called before the first frame update
    void Start()
    {
        blockSpacing = pixelBlock - spacing;
        pubIs = toggle.isOn;
        parameterText.text = "������\nwidth ��" + w + "\nheight : " + h;
    }
    private void OnEnable()
    {
        if (ColorImageOut.I.colorData.Count <= 0)
            return;
        
        ShowPixelBlock();
        ShowPreviewImage();
    }

    // Update is called once per frame
    void Update()
    {
        if (pubIs != toggle.isOn)
        {
            ShowPreviewImage();
            pubIs = toggle.isOn;
        }
    }


    public void OnBtnOutPng(string path)
    {
        string fname = path + ".png";
        File.WriteAllBytes(fname,bytes);
    }

    /// <summary>
    /// չʾɫ��
    /// </summary>
    public void ShowPixelBlock()
    {
        int Count = tfContent.childCount;
        int newcount = ColorImageOut.I.colorData.Count - Count;
        if (newcount > 0)
        {
            for (int i = 0; i < newcount; i++)
            {
                var item = Instantiate(itemGameObject);
                item.transform.SetParent(tfContent);

            }
        }
        //ˢ����ʾ
        for (int i = 0; i < tfContent.childCount; i++)
        {
            if (i >= ColorImageOut.I.colorData.Count)
            {
                tfContent.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                var t = tfContent.GetChild(i).gameObject;
                t.transform.GetComponent<Image>().color = ColorImageOut.I.colorData[i];
                if (t.gameObject.activeInHierarchy == false)
                {
                    t.gameObject.SetActive(true);
                }
            }
        }
    }

    /// <summary>
    /// ѭ������ɫ��
    /// </summary>
    public void ShowPreviewImage()
    {
        line = 0;
        column = 0;
        image.sprite = Sprite.Create(new Texture2D((int)w + 2, (int)h), new Rect(1, 0, w - 1, h), new Vector2(0, 0));
        Color[] color = new Color[ColorImageOut.I.colorData.Count];
        for (int i = 0; i < ColorImageOut.I.colorData.Count; i++)
        {
            color[i] = ColorImageOut.I.colorData[i];
        }
        for (int index = 0; index < ColorImageOut.I.colorData.Count; index++)
        {
            PreviewImage(line, column, pixelBlock, color[index], toggle.isOn);
            line++;
            if (line >= 12)
            {
                column++;
                line = 0;
            }
        }
    }
    /// <summary>
    /// ɫ�鸳ֵ��ɫ
    /// </summary>
    /// <param name="line">��</param>
    /// <param name="column">��</param>
    /// <param name="pixelBlock">���ؿ���</param>
    /// <param name="color">��ɫ</param>
    public void PreviewImage(int line, int column,int pixelBlock, Color color,bool toggle)
    {

        int lineStart = line * pixelBlock;
        int lineEnd = (line + 1) * pixelBlock;
        int columnStart = column * pixelBlock;
        int columnEnd = (column + 1) * pixelBlock;
        //Color c = toggle ? Color.black : color;
        for (int x = lineStart; x < lineEnd; x++)
        {
            for (int y = columnStart; y < columnEnd; y++)
            {
                image.sprite.texture.SetPixel(x, y, color);
            }
        }

        if (toggle)
        {
            for (int x = lineStart+blockSpacing; x < lineEnd; x++)
            {
                for (int y = columnStart; y < columnEnd; y++)
                {
                    image.sprite.texture.SetPixel(x, y, Color.black);
                }
            }
            for (int x = lineStart; x < lineEnd; x++)
            {
                for (int y = columnStart+blockSpacing; y < columnEnd; y++)
                {
                    image.sprite.texture.SetPixel(x, y, Color.black);
                }
            }
        }
        image.sprite.texture.Apply();
        bytes=image.sprite.texture.EncodeToPNG();
    }
}

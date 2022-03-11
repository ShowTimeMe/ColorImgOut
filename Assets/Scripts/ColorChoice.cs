using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChoice : MonoBehaviour
{
    public static ColorChoice I = null;
    public Transform tfColorBlock;
    public GameObject colorBlockItem;
    public Button btnConfirm;
    public SelectColorPanel selectColorPanel;
    private Color pubColor;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        btnConfirm.onClick.AddListener(delegate { OnBtnConfirm(); });
        selectColorPanel.ChangeColorAction += ChangeColorAction;
    }


    /// <summary>
    /// 点击确认 在左边生成相对应的 色块
    /// </summary>
    public void OnBtnConfirm()
    {
        ColorImageOut.I.colorData.Add(pubColor);
        ColorBlockShow();
    }
    /// <summary>
    /// 显示刷新
    /// </summary>
    public void ColorBlockShow()
    {
        int Count = tfColorBlock.childCount;
        int newcount = ColorImageOut.I.colorData.Count - Count;
        if (newcount > 0)
        {
            for (int i = 0; i < newcount; i++)
            {
                var item = Instantiate(colorBlockItem);
                item.transform.SetParent(tfColorBlock);

            }
        }
        //刷新显示
        for (int i = 0; i < tfColorBlock.childCount; i++)
        {
            if (i >= ColorImageOut.I.colorData.Count)
            {
                tfColorBlock.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                var t = tfColorBlock.GetChild(i).gameObject;
                t.GetComponent<ColorChoiceItem>().ItemShow(i);
                if (t.gameObject.activeInHierarchy == false)
                {
                    t.gameObject.SetActive(true);
                }
            }
        }
    }
    /// <summary>
    /// 颜色改变调用
    /// </summary>
    /// <param name="color"></param>
    private void ChangeColorAction(Color color)
    {
        pubColor.r = color.r;
        pubColor.g = color.g;
        pubColor.b = color.b;
        pubColor.a = color.a;
    }
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="index"></param>
    public void OnRemoveData(int index)
    {
        ColorImageOut.I.colorData.RemoveAt(index);
        ColorBlockShow();
    }
}

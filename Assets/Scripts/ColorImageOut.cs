using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorImageOut : MonoBehaviour
{
    public static ColorImageOut I = null;
    public Transform tfTab;
    public Transform tfPage;
    public List<Color> colorData = new List<Color>();
    
    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        for (int i = 0; i < tfTab.childCount; i++)
        {
            int index = i;
            GameObject go = tfTab.GetChild(i).gameObject;
            go.GetComponent<Button>().onClick.AddListener(delegate { OnTabShow(index); });
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        OnTabShow(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// tab显示
    /// </summary>
    /// <param name="index"></param>
    private void OnTabShow(int index)
    {
        for (int i = 0; i < tfPage.childCount; i++)
        {
            GameObject go = tfPage.GetChild(i).gameObject;
            go.SetActive(index == i);
        }
    }
   
}

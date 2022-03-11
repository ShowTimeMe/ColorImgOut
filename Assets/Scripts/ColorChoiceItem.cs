using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChoiceItem : MonoBehaviour
{
    public Button button;
    public Image image;
    private int gameObjectIndex;
    //private ColorImageOut colorImageOut;
    // Start is called before the first frame update
    void Awake()
    {
       
        button.onClick.AddListener(delegate {
            ColorChoice.I.OnRemoveData(gameObjectIndex);
        });
    }
    private void OnEnable()
    {
       
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ItemShow(int index)
    {
        gameObjectIndex = index;
        image.color = ColorImageOut.I.colorData[gameObjectIndex];
    }
}

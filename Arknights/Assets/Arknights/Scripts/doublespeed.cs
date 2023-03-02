using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doublespeed : MonoBehaviour
{
    Image speedimage;
    public Sprite[] sprites = null;

    private void Start()
    {
        speedimage= GetComponent<Image>();
    }
    private void Update()
    {
        if(UIManager.onDoubleSpeed)
        {
            speedimage.sprite = sprites[0];
        }
        else
        {
            speedimage.sprite = sprites[1];
        }
    }
}

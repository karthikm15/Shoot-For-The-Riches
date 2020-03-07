using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slotScript : MonoBehaviour
{
    public Color regColor;
    public Color highlightColor;
    public void Highlight()
    {
        GetComponent<Image>().color = highlightColor;
    }
    public void resetColor()
    {
        GetComponent<Image>().color = regColor;
    }
}

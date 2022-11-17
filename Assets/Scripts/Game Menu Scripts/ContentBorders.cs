using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBorders : MonoBehaviour
{
    //For Contract panel
    float contentBottomBorder;
    int activeContactCount;
    public int contentWide;

    void Update()
    {
        CheckContentBorders();
    }

    void CheckContentBorders()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                activeContactCount++;
            }
        }

        //contentWide is one page y length and one page have 2 contract
        contentBottomBorder = ((activeContactCount / 2) * contentWide) - contentWide/2;

        if (activeContactCount % 2 == 1)
        {
            contentBottomBorder += contentWide/2;
        }
        if (gameObject.GetComponent<RectTransform>().anchoredPosition.y < 0)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
        else if (gameObject.GetComponent<RectTransform>().anchoredPosition.y > contentBottomBorder)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, contentBottomBorder);
        }

        activeContactCount = 0;
    }

}

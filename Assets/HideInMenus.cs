using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInMenus : MonoBehaviour
{
    public static bool enableGO = true;

    void Update()
    {
        if (enableGO)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}

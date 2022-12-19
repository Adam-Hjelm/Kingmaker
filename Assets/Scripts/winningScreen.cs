using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinningScreen : MonoBehaviour
{
    public Image characterSprite;
    //public GameObject crownSprite;
    //public GameObject lightRay;
    public GameObject shadow;
    //public GameObject text;
    //public GameObject buttons;

    private void OnEnable()
    {
        StartCoroutine(DoCode());
    }

    IEnumerator DoCode()
    {
        Debug.Log("PrintOnEnable: script was enabled");
        characterSprite.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        //crownSprite.SetActive(true);
        yield return new WaitForSeconds(.7f);
        //lightRay.SetActive(true);
        //shadow.SetActive(true);
        yield return new WaitForSeconds(.5f);
        //text.SetActive(true);
        //buttons.SetActive(true);
        yield return null;
    }


}




using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputToText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        if (text == null)
            text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void SetText()
    {
        //// Call GetBindingDisplayString() such that it also returns information about the
        //// name of the device layout and path of the control on the device. This information
        //// is useful for reliably associating imagery with individual controls.
        //var bindingString = action.GetBindingDisplayString(out deviceLayout, out controlPath);

        //// If it's a gamepad, look up an icon for the control.
        //Sprite icon = null;
        //if (!string.IsNullOrEmpty(deviceLayout)
        //    && !string.IsNullOrEmpty(controlPath)
        //    && InputSystem.IsFirstLayoutBasedOnSecond(deviceLayout, "Gamepad"))
        //{
        //    switch (controlPath)
        //    {
        //        case "buttonSouth": icon = aButtonIcon; break;
        //        case "dpad/up": icon = dpadUpIcon; break;
        //            //...
        //    }
        //}

        //// If you have an icon, display it instead of the text.
        //var text = m_RebindButton.GetComponentInChildren<Text>();
        //var image = m_RebindButton.GetComponentInChildren<Image>();
        //if (icon != null)
        //{
        //    // Display icon.
        //    text.gameObject.SetActive(false);
        //    image.gameObject.SetActive(true);
        //    image.sprite = icon;
        //}
        //else
        //{
        //    // Display text.
        //    text.gameObject.SetActive(true);
        //    image.gameObject.SetActive(false);
        //    text.text = bindingString;
        //}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This was supposed to be used for a toggle that would switch between easy and regualr mode
/// there was a unity bug that activated the toggle when the menu opened that caused this to not work
/// as intended so the easy mode button was implemented instead
/// </summary

public class SetToggle : MonoBehaviour {

    public Transform global;
    Toggle toggle;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        global = GameObject.FindWithTag("Global").transform;
        if(global.transform.GetComponent<GlobalVar>().easyMode == true)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OkButton : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject objpanel;
    private bool firstPanel = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            activateObjectives();
            Ok();
        }
    }

    public void Ok()
    {
        GameObject foundPane = GameObject.FindGameObjectWithTag("Panel");
        if (foundPane != null)
        {
            
            foundPane.SetActive(false);
        }
        
    }

    void activateObjectives()
    {
        if (!firstPanel)
        {
            firstPanel = true;
            objpanel.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    [SerializeField] private GameObject ActualFire;
    [SerializeField] private GameObject innerMonologue;
    private bool KilledFire = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Stove" && ActualFire.activeSelf)
        {
            //Debug.Log("trigger2");
            for (int i = 0; i < ActualFire.transform.childCount; i++)
            {ActualFire.transform.GetChild(i).gameObject.SetActive(false);}
            ActualFire.SetActive(false);
            innerMonologue.SetActive(true);
            KilledFire = true;
        }
    }

    public bool FireOut()
    { return KilledFire; }
}

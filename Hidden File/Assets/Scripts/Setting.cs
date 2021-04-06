using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Setting : MonoBehaviour
{
    [SerializeField] public static float volume;
    [SerializeField] public static bool MF;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        SetVolume(GameObject.FindGameObjectWithTag("SM").GetComponent<Volume>().GetVolume());
    }

    public void SetVolume(float new_vol)
    { volume = new_vol; }

    public float GetVoume()
    { return volume; }

    public void SetMF(bool b)
    {
        MF = b;
    }

    public bool GetGender()
    {
        return MF;
    }
}

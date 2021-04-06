using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Volume : MonoBehaviour
{
    [SerializeField] private AudioSource audio_source;
    [SerializeField] private float music_volume = 0.5f;
    [SerializeField] private Slider s;
    void Awake()
    { 
        audio_source = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name == "Main") //&& GameObject.FindGameObjectWithTag("Setting") != null)
        {
            if (GameObject.FindGameObjectWithTag("Setting") != null)
            {
                music_volume = Setting.volume; //GameObject.FindGameObjectWithTag("Setting").GetComponent<Setting>().GetVoume();
            }
            //music_volume = Setting.volume; //GameObject.FindGameObjectWithTag("Setting").GetComponent<Setting>().GetVoume();
            s.value = music_volume;
        }
        
    }
    void Update()
    {
        audio_source.volume = music_volume;
    }

    public void AdjustVolume()
    {
        float volume_level = s.value;
        music_volume = volume_level;
    }

    public float GetVolume()
    {
        return music_volume;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IntroMenu : MonoBehaviour
{
    private AudioSource s;
    [SerializeField] private AudioClip c1;
    [SerializeField] private AudioClip c2;
    [SerializeField] private GameObject select_screen;
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject title;
    private void Awake()
    {
        s = GameObject.FindGameObjectWithTag("SM").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("SideOP") != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Back();
                s.PlayOneShot(c2);
            }
        }
    }


    public void Play()
    {
        SceneManager.LoadScene("Main");
    }

    public void PlaySelect(bool b)
    {
        buttons.SetActive(!b);
        title.SetActive(!b);
        select_screen.SetActive(b);
    }

    public void Back()
    {
        foreach (GameObject sop in GameObject.FindGameObjectsWithTag("SideOP"))
        { sop.SetActive(false); }
        EnableButtons(true);
    }

    public void FullScreen()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }

    public void Windowed()
    {
        Screen.SetResolution(800, 600, false);
    }

    public void ClickButton(GameObject SideOP)
    {
        Back();
        SideOP.SetActive(true);
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Button"))
        {
            EnableButtons(false);
        }
    }

    public void EnableButtons(bool b)
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Button"))
        {
            g.GetComponent<Button>().enabled = b;
            g.GetComponent<EventTrigger>().enabled = b;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PlaySound(AudioClip c)
    {
        s.PlayOneShot(c);
    }
}

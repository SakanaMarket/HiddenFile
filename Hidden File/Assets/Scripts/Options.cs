using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    [SerializeField] private GameObject mainOP;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject objectExplaination;
    [SerializeField] private GameObject volume;
    [SerializeField] private Looking lookScript;
    [SerializeField] private Movements movements;
    [SerializeField] private CharacterController c;
    [SerializeField] private GameObject fade;
    [SerializeField] private AudioClip c1;
    [SerializeField] private AudioClip c2;

    private void Update()
    {
        if (fade.activeSelf)
        {
            StartCoroutine(WaitFade(fade.GetComponent<Animator>()));
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlaySound2();
            if (mainOP.activeSelf == false)
            { Back(); }
            else if (mainOP.activeSelf == true)
            { MainBack(); }
        }
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("SideOP"))
        { g.SetActive(false); }
        mainOP.SetActive(true);
        lookScript.enabled = false;
        movements.enabled = false;
        c.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Controls()
    {
        mainOP.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void Objectives()
    {
        mainOP.SetActive(false);
        objectExplaination.SetActive(true);
    }

    public void Volume()
    {
        mainOP.SetActive(false);
        volume.SetActive(true);
    }

    public void MainBack()
    {
        lookScript.enabled = true;
        movements.enabled = true;
        c.enabled = true;
        mainOP.SetActive(false);
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("SideOP"))
        { g.SetActive(false); }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

        IEnumerator WaitFade(Animator ani)
    {
        yield return new WaitForSeconds(ani.GetCurrentAnimatorStateInfo(0).length);
        fade.SetActive(false);
    }

    public void FullScreen()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }

    public void Windowed()
    {
        Screen.SetResolution(1366, 768, false);
    }

    public void PlaySound1()
    {
        GameObject.FindGameObjectWithTag("SM").GetComponent<AudioSource>().PlayOneShot(c1);
    }

    public void PlaySound2()
    {
        GameObject.FindGameObjectWithTag("SM").GetComponent<AudioSource>().PlayOneShot(c2);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

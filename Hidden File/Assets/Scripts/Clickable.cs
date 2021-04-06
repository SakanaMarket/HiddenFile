using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class Clickable : MonoBehaviour
{
    [SerializeField] private Material highlighted;
    [SerializeField] private Material unhighlighted;
    [SerializeField] private float raycastLength;
    private Transform _selected;
    [SerializeField] private List<GameObject> Panels;
    [SerializeField] private GameObject Lights;
    //[SerializeField] private GameObject File2;
    [SerializeField] private Manage m;
    [SerializeField] private List<GameObject> OFPanels;
    [SerializeField] private bool pass;
    private bool l;
    [SerializeField] private GameObject keycard;
    [SerializeField] private AudioSource a;
    [SerializeField] private AudioClip door_slide;
    [SerializeField] public AudioClip click_pop;
    [SerializeField] private AudioClip light_switch;
    [SerializeField] private AudioClip denied;
    [SerializeField] private AudioClip paper;
    [SerializeField] private AudioClip key;

    void Update()
    {
        if (_selected != null)
        {
            var selectionRenderer = _selected.GetComponent<Renderer>();
            selectionRenderer.material = unhighlighted;
            _selected = null;
        }
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, raycastLength))
        {
            var selection = hit.transform;
            var selectionRenderer = selection.GetComponent<Renderer>();

            if (!selection.CompareTag("Untagged")) { Highlight(selectionRenderer, selection); }

            GameObject checkP = GameObject.FindGameObjectWithTag("Panel");

            if (Input.GetKeyDown(KeyCode.Mouse0) && checkP == null)
            {
                if (selection.CompareTag("door"))
                { ClickedOnADoor(selection); }
                else if (selection.CompareTag("bossdoor"))
                {
                    if (pass)
                    { ClickedOnBossDoor(selection); }
                    else
                    { Panels[6].SetActive(true); a.PlayOneShot(denied, 0.5f); }
                }
                else if (selection.CompareTag("Breaker"))
                {
                    a.PlayOneShot(light_switch, 0.5f);
                    if (Lights.activeSelf) { Lights.SetActive(false); }
                    else { 
                        Lights.SetActive(true); CheckBoolList(2); 
                        if (!l) { Panels[2].SetActive(true); l = true; }
                    }
                    
                }
                else if (selection.CompareTag("File"))
                { Panels[1].SetActive(true); CheckBoolList(1); a.PlayOneShot(paper, 0.5f); }
                else if (Regex.IsMatch(selection.tag, "File[\\d]"))
                {
                    a.PlayOneShot(paper, 0.5f);
                    Panels[int.Parse(selection.tag.Substring(4)) + 1].SetActive(true);
                    if (selection.tag == "File2") { m.setF2(); CheckBoolList(3); }
                    else if (selection.tag == "File3") { m.setF3(); CheckBoolList(4); }
                    else if (selection.tag == "File4") { CheckBoolList(5); }
                }
                else if (Regex.IsMatch(selection.tag, "OF[\\d]"))
                {
                    a.PlayOneShot(paper, 0.5f);
                    OFPanels[int.Parse(selection.tag.Substring(2)) - 1].SetActive(true);
                    if (selection.tag == "OF10") { keycard.SetActive(true); CheckBoolList(7); }
                    else if (selection.tag == "OF11") { CheckBoolList(9); }
                }
                else if (selection.CompareTag("key"))
                { 
                    selection.gameObject.SetActive(false); 
                    pass = true; 
                    CheckBoolList(8);
                    a.PlayOneShot(key, 0.5f);
                }
                else if (selection.CompareTag("answer"))
                { OFPanels[OFPanels.Count-1].SetActive(true); a.PlayOneShot(key, 0.5f); }
            }
        }
        
    }

    void ClickedOnADoor(Transform Door)
    {
        Animator cDoor_ani = Door.GetComponent<Animator>();
        cDoor_ani.SetBool("ClickedDoor", true);
        a.PlayOneShot(door_slide, 0.5f);
        StartCoroutine(WaitDoorClose(cDoor_ani));
    }

    void ClickedOnBossDoor(Transform Door)
    {
        Animator cDoor_ani = Door.GetComponent<Animator>();
        cDoor_ani.SetBool("ClickedDoor", true);
        cDoor_ani.SetBool("Key", true);
        a.PlayOneShot(door_slide, 0.5f);
        StartCoroutine(WaitDoorClose(cDoor_ani));
    }

    IEnumerator WaitDoorClose(Animator ani)
    {
        yield return new WaitForSeconds(ani.GetCurrentAnimatorStateInfo(0).length);
        ani.SetBool("ClickedDoor", false);
        //a.PlayOneShot(door_slide, 0.5f);
    }

    private void Highlight(Renderer selectionRenderer, Transform selected)
    {
        unhighlighted = selectionRenderer.material;
        if (selectionRenderer != null)
        { selectionRenderer.material = highlighted; }
        _selected = selected;
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("Waiting...");
    }

    private void IfActiveSetAnotherActive(GameObject g, GameObject h)
    {
        if (g.activeSelf) { h.SetActive(true); }
        else { h.SetActive(false); }
    }

    public void CheckBoolList(int c)
    {
        int checkCount = GameObject.FindGameObjectWithTag("M").GetComponent<Manage>().ReturnCount();
        //Debug.Log(checkCount);
        if (checkCount == c)
        {
            //Debug.Log("checking index");
            bool checkBoolIndex = GameObject.FindGameObjectWithTag("M").GetComponent<Manage>().ReturnBoolList()[checkCount];
            bool checkBoolB4 = GameObject.FindGameObjectWithTag("M").GetComponent<Manage>().ReturnBoolList()[checkCount-1];
            if (!checkBoolIndex && checkBoolB4)
            {
                GameObject.FindGameObjectWithTag("M").GetComponent<Manage>().ChangeBoolList(checkCount);
                GameObject.FindGameObjectWithTag("M").GetComponent<Manage>().ChangeQuestText(checkCount);
                increaseCount();
                Debug.Log("increased count from " + checkCount + " to " + GameObject.FindGameObjectWithTag("M").GetComponent<Manage>().ReturnCount() );
            }
        }
        
    }
    public void increaseCount()
    {
        GameObject.FindGameObjectWithTag("M").GetComponent<Manage>().incCount();
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manage : MonoBehaviour
{
    [SerializeField] private GameObject light_switch;
    [SerializeField] private GameObject File2;
    private bool F2 = false;
    [SerializeField] private GameObject File3;
    private bool F3 = false;
    [SerializeField] private GameObject File4;
    private bool F4 = false;
    [SerializeField] private GameObject Fire;
    [SerializeField] private GameObject OfficeFiles;
    [SerializeField] private Text quest;
    [SerializeField] private List<bool> o;
    [SerializeField] private List<string> q;
    [SerializeField] private int count = 1;

    private void Awake()
    {
        o[0] = true;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        IfActiveSetAnotherActive(light_switch, File2);
        IfClickedSetActive(F2, File3);
        IfClickedSetActive(F3, File4);
        IfActiveSetAnotherActive(File4, Fire);
        FireOutSoSpawnOfficeNotes();
    }

    void IfActiveSetAnotherActive(GameObject g, GameObject h)
    {
        if (g.activeSelf)
        { h.SetActive(true); }
        else
        { h.SetActive(false); }
    }

    void IfClickedSetActive(bool clicked, GameObject g)
    {
        if (clicked)
        { g.SetActive(true); }
    }

    public void setF2()
    { F2 = true; }
    public void setF3()
    { F3 = true; }

    private void FireOutSoSpawnOfficeNotes()
    {
        GameObject FireExtinguisher = GameObject.FindGameObjectWithTag("Fire");
        if (FireExtinguisher != null && FireExtinguisher.GetComponent<FireExtinguisher>().FireOut())
        { OfficeFiles.SetActive(true); GameObject.FindGameObjectWithTag("CM").GetComponent<Clickable>().CheckBoolList(6); } 
    }

    public void ChangeQuestText(int c)
    { quest.text = q[c]; }

    public void ChangeBoolList(int c)
    {
        o[c] = true;
    }

    public List<bool> ReturnBoolList()
    {
        return o;
    }

    public int ReturnCount()
    {
        return count;
    }

    public void incCount()
    {
        count++;
    }
}

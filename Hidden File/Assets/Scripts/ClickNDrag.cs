using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickNDrag : MonoBehaviour
{
    [SerializeField] private Vector3 mouseOffset;
    [SerializeField] private float mouseZCoord;
    [SerializeField] private AudioClip pop;
    [SerializeField] private AudioSource a;

    private void Awake()
    {
        a = GameObject.FindGameObjectWithTag("SM").GetComponent<AudioSource>();
        pop = GameObject.FindGameObjectWithTag("CM").GetComponent<Clickable>().click_pop;
    }
    private void OnMouseDown()
    {
        mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseOffset = gameObject.transform.position - GetMouseWorldPos();
        a.PlayOneShot(pop, 0.5f);
    }
    private void OnMouseDrag()
    {
        //transform.position = new Vector3(GetMouseWorldPos().x + mouseOffset.x, transform.position.y, GetMouseWorldPos().z + mouseOffset.z);
        transform.position = GetMouseWorldPos() + mouseOffset;
    }

    private Vector3 GetMouseWorldPos()
    {
        // (x, y) on screen
        Vector3 mouseCoord = Input.mousePosition;

        // z on screen
        mouseCoord.z = mouseZCoord;

        return Camera.main.ScreenToWorldPoint(mouseCoord);
    }
   
}

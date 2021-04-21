using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class puzzle_difference : interactive_obj
{
    // Start is called before the first frame update
    public Canvas c;
    bool zoom;
    hint_script hint;
    GameObject itemcast;
    Camera thirdperson_camera;
    void Start()
    {
        found = 0;
        objname = "puzzle_difference";
        hinttext = "Press Q to interact with the paintings.";
        thirdperson_camera = Camera.main;
        hint = GameObject.Find("hint").GetComponent<hint_script>();
        itemcast = GameObject.Find("itemcast");
    }

    // Update is called once per frame
    void Update()
    {
        if (zoom && Input.GetKeyDown(KeyCode.F))
        {
            zoom = false;

            objcamera.gameObject.active = false;
            objcamera.enabled = false;


            thirdperson_camera.transform.parent.gameObject.active = true;
            thirdperson_camera.enabled = true;
            itemcast.active = true;
            EventSystem.current.SetSelectedGameObject(null);

            c.gameObject.active = false;
        }
    }

    public override void pickup()
    {
        Debug.Log("zhaobutong");
        Cursor.visible = true;
        zoom = true;
        hint.setMessege("Somewhere is different...");
        itemcast.active = false;
        c.gameObject.active = true;


        objcamera.gameObject.active = true;
        objcamera.enabled = true;

        thirdperson_camera.enabled = false;
        thirdperson_camera.transform.parent.gameObject.active = false;
    }

    int found;
    public void diffonclick() {

        found++;
        if (found == 5) {
            hint.setMessege("There are <b>five<b> difference.");
        }
    }


}

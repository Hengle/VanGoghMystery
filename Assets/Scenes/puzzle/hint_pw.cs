using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class hint_pw : interactive_obj
{
    public Canvas c;
    bool zoom;
    hint_script hint;
    GameObject itemcast;
    Camera thirdperson_camera;
    // Start is called before the first frame update
    void Start()
    {
        thirdperson_camera = Camera.main;
        objname = "letter";
        itemcast = GameObject.Find("itemcast");
        hint = GameObject.Find("hint").GetComponent<hint_script>();
        hinttext = "A letter?";
    }

    // Update is called once per frame
    void Update()
    {
        if (zoom && Input.GetKeyDown(KeyCode.F))
        {

            c.gameObject.active = false;
            zoom = false;
            objcamera.gameObject.active = false;
            objcamera.enabled = false;

            thirdperson_camera.enabled = true;
            thirdperson_camera.gameObject.active = true;
            itemcast.active = true;
            EventSystem.current.SetSelectedGameObject(null);
            Cursor.visible = false;
        }
    }
    public override void pickup()
    {
        Debug.Log("pickup");
        Cursor.visible = true;
        zoom = true;
        hint.setMessege("Pin code is _5_7?");
        itemcast.active = false;
        c.gameObject.active = true;

        thirdperson_camera = Camera.main;

        objcamera.gameObject.active = true;
        objcamera.enabled = true;

        thirdperson_camera.enabled = false;
        thirdperson_camera.gameObject.active = false;

    }

}

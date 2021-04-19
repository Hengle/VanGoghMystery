using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class table : interactive_obj
{
    // Start is called before the first frame update
    Camera thirdperson_camera;
    bool zoom;
    GameObject itemcast;
    hint_script hint;
    void Start()
    {
        thirdperson_camera = Camera.main;
        objname = "table";
        itemcast = GameObject.Find("itemcast");
        hint = GameObject.Find("hint").GetComponent<hint_script>();
        hinttext = "Press Q to zoom in table";
    }

    // Update is called once per frame
    void Update()
    {
        if (zoom && Input.GetKeyDown(KeyCode.F)) {
            zoom = false;
            objcamera.gameObject.active = false;
            objcamera.enabled = false;
            thirdperson_camera.enabled = true;
            thirdperson_camera.gameObject.active = true;
            itemcast.active = true;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public override void pickup()
    {
        Debug.Log("table pickup!");
        objcamera.gameObject.active = true;
        objcamera.enabled = true;
        thirdperson_camera.enabled = false;
        thirdperson_camera.gameObject.active = false;
        zoom = true;
        hint.setMessege("The vase is empty...");
        itemcast.active = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pick_manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int myLayerMask = 1 << LayerMask.NameToLayer("Default");
        RaycastHit cameraHit;
        Ray cameraAim = Camera.main.GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.SphereCast(cameraAim.origin, 1f, cameraAim.direction, out cameraHit, 100f, myLayerMask)){
            Debug.Log("hit!");
        }
    }
}

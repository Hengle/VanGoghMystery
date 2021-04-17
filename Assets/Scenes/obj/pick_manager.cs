using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class pick_manager : MonoBehaviour
{

    Button pickup;
    hint_script hint;
    public GameObject hint_obj;
    // Start is called before the first frame update
    void Start()
    {
        pickup = GameObject.Find("PICKUP").GetComponent<Button>();
        hint = hint_obj.GetComponent<hint_script>();
    }

    // Update is called once per frame
    void Update()
    {
        int myLayerMask = 1 << LayerMask.NameToLayer("interactive_object");
        RaycastHit cameraHit;
        Ray cameraAim = Camera.main.GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.SphereCast(cameraAim.origin, 0.5f, cameraAim.direction, out cameraHit, 1f, myLayerMask))
        {
            GameObject g = cameraHit.transform.gameObject;
            interactive_obj o = g.GetComponent<interactive_obj>();
            if (o != null)
            {
                pickup.interactable = true;
                pickup.Select();
                hint.setMessege("Press Q to explore object");
                hint.show = true;
                
            }
        }
        else {
            pickup.interactable = false;
            hint.show = false;
        }
    }
}

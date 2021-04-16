using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class Global : MonoBehaviourPunCallbacks
{
    public GameObject female;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerCamera = GameObject.Find("Main Camera");
        GameObject character;
        if (PhotonNetwork.IsMasterClient)
        {
            character = PhotonNetwork.Instantiate(this.female.name, new Vector3(0, 0, 0), Quaternion.identity, 0);
            
        }
        else
        {
            character = PhotonNetwork.Instantiate(this.female.name, new Vector3(0, 0, 3), Quaternion.identity, 0);
        }
        playerCamera.transform.SetParent(character.transform);
        playerCamera.transform.localPosition = new Vector3(0, 1.85f, -1.4f);
        playerCamera.transform.localRotation = Quaternion.Euler(new Vector3(19, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

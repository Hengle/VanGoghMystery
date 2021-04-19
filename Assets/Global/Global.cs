using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using Balrond3PersonMovements;

public class Global : MonoBehaviourPunCallbacks
{
    public GameObject female;
    public GameObject male;
    public GameObject playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        // disable mouse
        Cursor.visible = false;

        PhotonNetwork.PrefabPool.RegisterPrefab(female.name, female);
        PhotonNetwork.PrefabPool.RegisterPrefab(male.name, male);

        GameObject pc = Instantiate(playerCamera, new Vector3(0, 0, 0), Quaternion.identity);
        Balrond3pCameraFollow cameraFollow = pc.GetComponent<Balrond3pCameraFollow>();

        GameObject character;
        if (PhotonNetwork.IsMasterClient)
        {
            character = PhotonNetwork.Instantiate(female.name, new Vector3(0, 0, 0), Quaternion.identity, 0);
            
        }
        else
        {
            character = PhotonNetwork.Instantiate(male.name, new Vector3(-1, 0, -1), Quaternion.identity, 0);
        }
        cameraFollow.target = character.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

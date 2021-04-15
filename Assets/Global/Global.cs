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
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(this.female.name, new Vector3(0, 0, 0), Quaternion.identity, 0);
        }
        else
        {
            PhotonNetwork.Instantiate(this.female.name, new Vector3(0, 0, 3), Quaternion.identity, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

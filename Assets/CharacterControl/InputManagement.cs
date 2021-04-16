using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InputManagement : MonoBehaviourPunCallbacks
{
    public float rotSpeed = 150;
    public float moveSpeed = 2;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        v = v < 0 ? 0 : v;
        animator.SetFloat("Forward", v + h * h);

        transform.Rotate(new Vector3(0, h * rotSpeed * Time.deltaTime, 0), Space.Self);
        transform.Translate(v * transform.forward * moveSpeed * Time.deltaTime, Space.World);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InventorySystem : MonoBehaviourPunCallbacks, IPunObservable
{
    [Header("List of distinct items needed in this level")]
    public string[] items;

    [Header("Number of items")]
    public int[] count;

    [Header("Winning conditions")]
    public int[] winningCount;

    private Dictionary<string, int> index;
    
    // Start is called before the first frame update
    void Start()
    {
        if (count.Length != items.Length || count.Length != winningCount.Length)
        {
            Debug.LogWarning("Item length and winning coundition not consistent");
        }
        index = new Dictionary<string, int>();
        for (int i = 0; i < items.Length; i++)
        {
            index.Add(items[i], i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PhotonView pv = PhotonView.Get(this);
            pv.RPC("AddItem", RpcTarget.All, "apple");
        }
    }

    void CheckWinCondition()
    {
        bool win = true;
        for (int i = 0; i < winningCount.Length; i++)
        {
            if (count[i] < winningCount[i])
            {
                win = false;
                break;
            }
        }
        if (win)
        {
            Debug.Log("win");
        }
    }

    [PunRPC]
    public void AddItem(string name)
    {
        Debug.Log("AddItem " + name);
        count[index[name]]++;
        CheckWinCondition();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            for (int i = 0; i < count.Length; i++)
            {
                stream.SendNext(count[i]);
            }
        }
        else
        {
            for (int i = 0; i < count.Length; i++)
            {
                count[i] = (int)stream.ReceiveNext();
            }
        }
    }
}

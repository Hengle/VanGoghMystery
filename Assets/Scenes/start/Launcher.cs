using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
public class Launcher : MonoBehaviourPunCallbacks
{
    public GameObject roompanel;
    public GameObject waiting_text;
    public GameObject room_button;
    public GameObject start_button;
    public Animator button_animator;
    // Start is called before the first frame update
    void Start()
    {
        Connect();
    }

    // Update is called once per frame
    void Update()
    { }

    // #Critical
    //// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect()
    {
        string playerName = "233";
        PhotonNetwork.LocalPlayer.NickName = playerName;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnStartGameButtonClicked()
    {
        
        PhotonNetwork.JoinRandomRoom();
        start_button.GetComponent<Button>().interactable = false;
        roompanel.SetActive(true);
    }
    public void OnRoomGameButtonClicked()
    {

        Debug.Log("Room!");
        Application.LoadLevel("Scenes/bedroom/bedroom");
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to master\n");
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("join to random room failed, create one\n");
        string roomName = "Room " + Random.Range(1000, 10000);

        RoomOptions options = new RoomOptions { MaxPlayers = 2 };

        PhotonNetwork.CreateRoom(roomName, options, null);
    }
    public override void OnJoinedRoom()
    {

       
        // joining (or entering) a room invalidates any cached lobby room list (even if LeaveLobby was not called due to just joining a room)
        Debug.Log("join room!\n");

        foreach (Player p in PhotonNetwork.PlayerList) {
            Debug.Log(p.UserId);
        }
        if (CheckPlayersReady())
        {    }
        else {
            Debug.Log("Not enough people\n");
        }
        

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

        if (CheckPlayersReady()) {
            Debug.Log("We can start!\n");
            waiting_text.GetComponent<TextMeshProUGUI>().text = "Ready";
            room_button.GetComponent<Button>().interactable = true;
        }
    }

    private bool CheckPlayersReady()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Not master client!");
            return false;
        }

        if (PhotonNetwork.PlayerList.Length == 2)
        {
            Debug.Log("Ready!");
            return true;
        }
        else {
            return false;
        }

    }
}
    
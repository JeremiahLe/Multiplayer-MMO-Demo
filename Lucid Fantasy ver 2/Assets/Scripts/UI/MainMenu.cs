using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject findOpponentPanel = null;
        [SerializeField] private GameObject waitingStatusPanel = null;
        [SerializeField] private Text waitingStatusText = null;

        public GameObject Panel;

        private bool isConnecting = false;

        private const string GameVersion = "0.1";
        private const int MaxPlayersPerRoom = 2;

        private void Awake() => PhotonNetwork.AutomaticallySyncScene = true;

        public void FindOpponent()
        {
            isConnecting = true;

            findOpponentPanel.SetActive(false);
            waitingStatusPanel.SetActive(true);

            waitingStatusText.text = "Connecting...";

            if (PhotonNetwork.IsConnected)
            {
                //PhotonNetwork.JoinRandomRoom();
                PhotonNetwork.JoinRoom("room1");
            }
            else
            {
                PhotonNetwork.GameVersion = GameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

    public void ActivatePanel()
    {
        if (Panel.GetComponent<PlayerNameInput>().SearchOn)
        {
            Panel.SetActive(false);
            findOpponentPanel.SetActive(true);
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect to Master");

        if (isConnecting)
        {
            Debug.Log("Connected to Master");

            if (isConnecting)
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        waitingStatusPanel.SetActive(false);
        findOpponentPanel.SetActive(true);

        Debug.Log($"Disconnected due to:  {cause}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No clients are waiting for players, creating a new room");

        PhotonNetwork.CreateRoom("room1", new RoomOptions { MaxPlayers = MaxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Client successfully joined a room");

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if(playerCount != MaxPlayersPerRoom)
        {
            //waitingStatusText.text = "Connected...";
            //Debug.Log("Client is waiting for an opponent");
        //}
        //else
        //{
            waitingStatusText.text = "Joining game...";
            Debug.Log("Match is ready to begin");

            PhotonNetwork.LoadLevel("TestZone");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount < MaxPlayersPerRoom)
        {
            //PhotonNetwork.CurrentRoom.IsOpen = false;

            waitingStatusText.text = "Joining game...";
            Debug.Log("Match is ready to begin");

            PhotonNetwork.LoadLevel("TestZone");
        }
    }
}

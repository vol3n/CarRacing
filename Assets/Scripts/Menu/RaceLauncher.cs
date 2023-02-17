using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class RaceLauncher : MonoBehaviourPunCallbacks
{
    public InputField playerName;

    [SerializeField]
    byte maxPlayerNum = 4;

    bool isConnecting;
    public Text networkText;
    string gameVersion = "1";


    public void SetName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);

    }

    public void Awake()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            playerName.text = PlayerPrefs.GetString("PlayerName");
        }
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Track");
    }

    public void ConnectNetwork()
    {
        networkText.text = "";
        isConnecting = true;
        PhotonNetwork.NickName = playerName.text;

        if (PhotonNetwork.IsConnected)
        {
            networkText.text += "Joining Room...\n";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            networkText.text += "Connecting ...\n";
            PhotonNetwork.GameVersion += gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            networkText.text += "OnConnectToMaster...\n";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        networkText.text += "Failed to join random room\n";
        PhotonNetwork.CreateRoom(null,new RoomOptions {MaxPlayers=maxPlayerNum});
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        networkText.text += $"Disconnected because {cause}\n";
        isConnecting = false;
    }
    public override void OnJoinedRoom()
    {
        networkText.text += $"Joined Room with {PhotonNetwork.CurrentRoom.PlayerCount} players.";
        PhotonNetwork.LoadLevel("Track");
    }
}

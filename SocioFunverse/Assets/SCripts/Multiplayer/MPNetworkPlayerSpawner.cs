using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPNetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject spawnedPlayerPrefab;
    [SerializeField] AudioClip ting;
  

    public void GeneratePlayer(int _no = 0, bool _custmer = false)
    {
        //var randomNo = Random.Range(0, MetaManager.insta.playerPoz.Length);
        var randomNo = PhotonNetwork.PlayerList.Length - 1;
        Debug.Log("GeneratePlayer " + _no);
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("MPNetworkPlayer", MetaManager.insta.playerPoz[randomNo].position, MetaManager.insta.playerPoz[randomNo].rotation);
       

        // if (_custmer) spawnedPlayerPrefab.GetComponent<NetworkPlayer>().myNoIs = _no;
        //CoreManager.myDefaultStartPoz = _no;
        //PhotonView photonView = GetComponent<PhotonView>();
        //NetworkManager.insta.SendUserRole(PhotonNetwork.LocalPlayer.UserId);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
    }


    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("OnLeftRoom");
        if(spawnedPlayerPrefab) PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }

   
}

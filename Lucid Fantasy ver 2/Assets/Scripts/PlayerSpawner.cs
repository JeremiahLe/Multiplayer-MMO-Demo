using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab = null;

    // Start is called before the first frame update
    private void Start() => PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);

    // Update is called once per frame
    void Update()
    {
        
    }
}

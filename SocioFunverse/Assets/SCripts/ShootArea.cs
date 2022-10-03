using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArea : MonoBehaviour
{

    [Header("Balloon Spawn Properties")]
    [SerializeField] float spawn_delay;
    [SerializeField] float spawn_time = 0;
    [SerializeField] Vector3 spawn_balloon_location;
    [SerializeField] GameObject balloon_prefab;
    // Start is called before the first frame update
    void Start()
    {
        spawn_balloon_location = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        spawn_time += Time.deltaTime;
        if (PhotonNetwork.IsMasterClient) {
            if (spawn_time > spawn_delay)
            {
                spawn_time = 0;
                Vector3 offset = Random.insideUnitSphere * 10f;
                offset.y = 1.5f;
                offset.z = Mathf.Clamp(offset.x, -2, 2);
                PhotonNetwork.Instantiate(balloon_prefab.name, spawn_balloon_location +offset, balloon_prefab.transform.rotation);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;


public class Balloon : MonoBehaviourPun, IPunOwnershipCallbacks
{
    // Start is called before the first frame update
    [SerializeField] MeshRenderer renderer;
    [SerializeField] Material[] materials;
    [SerializeField] Photon.Pun.PhotonView pv;
    private void Awake()
    {
        renderer.material = materials[Random.Range(0, materials.Length)];
        LeanTween.scale(this.gameObject, Vector3.one * 0.5f, 0.5f);
        LeanTween.moveY(this.gameObject, this.transform.position.y + 10f, Random.Range(5f,8f)).setOnComplete(()=> {
            LeanTween.scale(this.gameObject, Vector3.zero, 0.5f).setOnComplete(() =>
            {
                Hit();
            });
        });

    }

    public void Hit()
    {
        LeanTween.cancel(this.gameObject);
        if (base.photonView != null)
        {
            if (pv && pv.IsMine)
            {
                Photon.Pun.PhotonNetwork.Destroy(pv);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Bullet"))
        {
            AudioManager.insta.playSound(14);
            LeanTween.cancel(this.gameObject);
            MetaManager.insta.myPlayer.GetComponent<MyCharacter>().HitBalloon(this.transform.position);
            if (base.photonView != null)
            {
                if (base.photonView.IsMine && gameObject != null)
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        PhotonNetwork.Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }

                else
                {
                    Destroy(this.gameObject);
                    base.photonView.RequestOwnership();
                }
            }
            //this.gameObject.SetActive(false);
            //Hit();
        }
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        // throw new System.NotImplementedException();
        Debug.Log("3");
        if (base.photonView != null && targetView.IsMine)
        {
            Debug.Log("4");
            if (targetView != base.photonView) return;

            Debug.Log("5");
            Destroy(gameObject);
            base.photonView.TransferOwnership(requestingPlayer);
        }

    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        if (base.photonView != null)
        {
            // throw new System.NotImplementedException();
            if (targetView != base.photonView) return;

            Debug.Log("1");
            if (!base.photonView.IsMine && gameObject != null)
            {
                Debug.Log("2");
                Destroy(gameObject);
            }
        }
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
       // throw new System.NotImplementedException();
    }
}

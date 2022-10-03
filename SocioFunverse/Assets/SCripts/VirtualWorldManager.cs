using System;
using System.Collections.Generic;
using UnityEngine;
public class VirtualWorldManager : MonoBehaviour
{


    [SerializeField]
    List<GameObject> userworldObj = new List<GameObject>();
    [SerializeField] Transform playerLocation;

    Vector3 playerPoz;
    Quaternion playerRot;

    Vector3 playerLastPoz;
    Quaternion playerLastRot;

    [SerializeField]
    GameObject homeBtn;

    [SerializeField]
    GameObject myWorldBtn;

    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        playerPoz = playerLocation.position;
        playerRot = playerLocation.rotation;

        homeBtn.SetActive(true);
        myWorldBtn.SetActive(false);

        playerLastPoz = MetaManager.insta.myPlayer.transform.position;
        playerLastRot = MetaManager.insta.myPlayer.transform.rotation;

        for (int i = 0; i < userworldObj.Count; i++)
        {
            userworldObj[i].SetActive(false);
            if (CovalentManager.isMyVirtualWorld)
            {


                for (int j = 0; j < CovalentManager.insta.myTokenID.Count; j++)
                {
                    if (Int32.Parse(CovalentManager.insta.myTokenID[j])-400 == i)
                    {
                        userworldObj[i].SetActive(true);
                    }
                }
            }
            else
            {
                for (int j = 0; j < CovalentManager.insta.otherTokenID.Count; j++)
                {
                    if (Int32.Parse(CovalentManager.insta.otherTokenID[j])-400 == i)
                    {
                        userworldObj[i].SetActive(true);
                    }
                }
            }
        }

        MetaManager.insta.myPlayer.GetComponent<CharacterController>().enabled = false;
        MetaManager.insta.myPlayer.transform.position = playerPoz;
        MetaManager.insta.myPlayer.transform.rotation = playerRot;
        MetaManager.insta.myPlayer.GetComponent<CharacterController>().enabled = true;
    }


    private void OnDisable()
    {
        MetaManager.insta.myPlayer.GetComponent<CharacterController>().enabled = false;
        //MetaManager.insta.myPlayer.transform.SetPositionAndRotation(playerLastLocation.position, playerLastLocation.rotation);
        MetaManager.insta.myPlayer.transform.position = playerLastPoz;
        MetaManager.insta.myPlayer.transform.rotation = playerLastRot;
        MetaManager.insta.myPlayer.GetComponent<CharacterController>().enabled = true;


        homeBtn.SetActive(false);
        myWorldBtn.SetActive(true);
    }
    public void GoToOpenWorld()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("OpenWorld");
    }


}

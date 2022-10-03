using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager insta;

    [Header("GameplayMenu")]
    public GameObject StartUI;
    public GameObject usernameUI;


    public TMP_Text usernameText;

    [SerializeField] TMP_InputField nameInput;
    [SerializeField] Button[] gender;

    [SerializeField] TMP_Text statusText;

    // fight manager
    [SerializeField] GameObject FightRequestUI;
    [SerializeField] TMP_Text fightRequestText;


    public static string username;
    public static int usergender;

    [Header("GameplayMenu")]
    public GameObject GameplayUI;
    [SerializeField] TMP_Text scoreTxt;
    [SerializeField] TMP_Text winCountTxt;
    [SerializeField] TMP_Text lostCountTxt;
    [SerializeField] Slider healthSlider;

    [SerializeField] FrostweepGames.WebGLPUNVoice.Recorder recorder;
    [SerializeField] FrostweepGames.WebGLPUNVoice.Listener lister;


    [Header("VoiceChat")]
    [SerializeField] Image recorderImg;
    [SerializeField] Image listenerImg;
    [SerializeField] Sprite[] recorderSprites; //0 on 1 off
    [SerializeField] Sprite[] listenerSprites; //0 on 1 off


    [Header("StoreAndCollection")]
    [SerializeField] GameObject myCollectionUI;


    [Header("Result")]
    [SerializeField] Image resultImg;
    [SerializeField] Sprite[] resultprites; //0 win 1 lose 2 tie

    [Header("Tutorial")]
    [SerializeField] GameObject TutorialUI;
    [SerializeField] int currentTutorial = 0;
    [SerializeField] GameObject[] tutorialObjects;

    public GameObject MyCollectionUIButton;

    public GameObject VirtualWorldObj;

    private void Awake()
    {
        insta = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        GameplayUI.SetActive(false);
        resultImg.gameObject.SetActive(false);
        StartUI.SetActive(true);
        usernameUI.SetActive(false);
        gender[0].interactable = false;
        gender[1].interactable = true;
        statusText.gameObject.SetActive(true);
        statusText.text = "";
        healthSlider.value = 1;

        //UpdatePlayerUIData(true, true);
        UpdateUserName(DatabaseManager.Instance.GetLocalData().name, SingletonDataManager.userethAdd);

        if (PlayerPrefs.GetInt("init", 0) == 0)
        {
            PlayerPrefs.SetInt("init", 1);
            EditUserProfile();
        }

        recorder.StopRecord();
    }


    public void ShowMyCollection(bool _show)
    {
        if (_show)
        {
            if (CovalentManager.insta.myTokenID.Count > 0)
                myCollectionUI.SetActive(true);
            else MessaeBox.insta.showMsg("Nothing in collection", true);
        }
        else
        {
            myCollectionUI.SetActive(false);
        }

    }
    public void ShowResult(int _no)
    {

        LeanTween.scale(resultImg.gameObject, Vector2.one, 1.5f).setFrom(Vector2.zero).setEaseOutBounce();
        StartCoroutine(gameResult(_no));

    }

    IEnumerator gameResult(int _no)
    {
        resultImg.gameObject.SetActive(true);
        resultImg.sprite = resultprites[_no];
        yield return new WaitForSeconds(3);
        resultImg.gameObject.SetActive(false);

        if (_no == 0)
        {
            if (CovalentManager.insta.myTokenID.Count == 0)
                MessaeBox.insta.showMsg("Get land for your virtual world from store", true);
            else MessaeBox.insta.showMsg("Earned 1 coin", true);
        }
    }

    public void VisitVirtualWorld(bool _show)
    {

        if (_show && !MetaManager.inVirtualWorld && !MetaManager.isFighting && !MetaManager.isShooting)
        {
            if (CovalentManager.insta.myTokenID.Count > 0)
            {
                CovalentManager.isMyVirtualWorld = true;
                VirtualWorldObj.SetActive(true);
                MetaManager.inVirtualWorld = true;
            }
            else
            {
                Debug.Log("No enough nft");
                MessaeBox.insta.showMsg("No virtual world item\nFight to earn coins and buy item", true);
            }
        }
        else
        {
            MetaManager.inVirtualWorld = false;
            VirtualWorldObj.SetActive(false);
        }

    }


    public void VisitOtherPlayerVirtualWorld()
    {

        if (CovalentManager.insta.otherTokenID.Count > 0)
        {
            Debug.Log("Virtual world available");
            CovalentManager.isMyVirtualWorld = false;
            VirtualWorldObj.SetActive(true);
            MetaManager.inVirtualWorld = true;
        }
        else
        {
            MetaManager.inVirtualWorld = false;
            Debug.Log("Virtual world NOT available");
            MessaeBox.insta.showMsg("No virtual world item", true);
        }
    }

    public void StartGame()
    {
        //StartUI.SetActive(false);
        if (PlayerPrefs.GetInt("tutorial", 0) == 0)
        {
            ShowTutorial();
        }
        else
        {
            MPNetworkManager.insta.OnConnectedToServer();
        }
    }
    #region Tutorial
    public void ShowTutorial()
    {
        TutorialUI.SetActive(true);
        for (int i = 0; i < tutorialObjects.Length; i++)
        {
            tutorialObjects[i].SetActive(false);
        }
        tutorialObjects[currentTutorial].SetActive(true);
    }
    public void NextTutorial()
    {
        tutorialObjects[currentTutorial].SetActive(false);
        currentTutorial++;
        if (currentTutorial >= tutorialObjects.Length)
        {
            SkipTutorial();
            return;
        }
        tutorialObjects[currentTutorial].SetActive(true);
    }
    public void SkipTutorial()
    {
        PlayerPrefs.SetInt("tutorial", 1);
        TutorialUI.SetActive(false);
        MPNetworkManager.insta.OnConnectedToServer();
    }


    #endregion

    public void UpdatePlayerUIData(bool _show,LocalData data, bool _init = false)
    {
        if (_show)
        {
            if (_init)
            {
                nameInput.text = data.name;
                SelectGender(data.characterNo);
            }

            scoreTxt.text = data.score.ToString();
            winCountTxt.text = data.gameWon.ToString();
            lostCountTxt.text = data.gameLoss.ToString();
            if (PhotonNetwork.LocalPlayer.CustomProperties["health"] != null) healthSlider.value = float.Parse(PhotonNetwork.LocalPlayer.CustomProperties["health"].ToString());
        }
        else
        {
            GameplayUI.SetActive(false);
        }
    }
    public void UpdatePlayerUIData(bool _show, bool _init = false)
    {
        if (_show)
        {           
            if (PhotonNetwork.LocalPlayer.CustomProperties["health"] != null) healthSlider.value = float.Parse(PhotonNetwork.LocalPlayer.CustomProperties["health"].ToString());
        }
        else
        {
            GameplayUI.SetActive(false);
        }
    }


    public TMP_Text txt_information;
    public void ShowBurnableNFTConfimation(int _id, string status)
    {
        txt_information.transform.parent.gameObject.SetActive(true);
        if (status.Equals("success"))
        {
            txt_information.text = "Coin Purchase of " + status + " successful";
        }
        else
        {
            txt_information.text = "Coin Purchase of " + status + " Failed";
        }

        StartCoroutine(disableTextInfo());
    }
    public void ShowCoinPurchaseStatus(TranscationInfo info)
    {
        txt_information.transform.parent.gameObject.SetActive(true);
        if (info.transactionStatus.Equals("success"))
        {
            txt_information.text = "Coin Purchase of " + info.coinAmount + " successful";
        }
        else
        {
            txt_information.text = "Coin Purchase of " + info.coinAmount + " Failed";
        }
        StartCoroutine(disableTextInfo());
    }
    IEnumerator disableTextInfo()
    {
        yield return new WaitForSeconds(3f);
        txt_information.transform.parent.gameObject.SetActive(false);
    }


    public void MuteUnmute()
    {
        if (recorder.recording)
        {
            recorder.recording = false;
            recorderImg.sprite = recorderSprites[1];
            recorder.StopRecord();
        }
        else
        {
            recorder.RefreshMicrophones();
            recorder.recording = true;
            recorder.StartRecord();
            recorderImg.sprite = recorderSprites[0];
        }
    }

    public void MuteUnmuteListner()
    {
        if (lister._listening)
        {
            lister._listening = false;
            listenerImg.sprite = listenerSprites[1];
        }
        else
        {
            lister._listening = true;
            listenerImg.sprite = listenerSprites[0];
        }
    }

    public void openMyWorld()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("VirtualWorld");
    }
    public void UpdateUserName(string _name, string _ethad = null)
    {
        if (_ethad != null)
        {
            usernameText.text = "Hi, " + _name + "\n Your crypto address is : " + _ethad;
            username = _name;
        }
        else usernameText.text = _name;
    }

    public void UpdateStatus(string _msg)
    {
        statusText.text = _msg;
        StartCoroutine(ResetUpdateText());
    }

    IEnumerator ResetUpdateText()
    {
        yield return new WaitForSeconds(2);
        statusText.text = "";
    }


    public void EditUserProfile()
    {
        if (DatabaseManager.Instance != null)
        {
            nameInput.text = DatabaseManager.Instance.GetLocalData().name;
        }

        usernameUI.SetActive(true);
        StartUI.SetActive(false);
    }
    public void GetName()
    {
        if (nameInput.text.Length > 0 && !nameInput.text.Contains("Enter")) username = nameInput.text;
        else username = "Player_" + Random.Range(11111, 99999);

        usernameUI.SetActive(false);

        LocalData data = DatabaseManager.Instance.GetLocalData();
        data.name = username;
        data.characterNo = usergender;
        DatabaseManager.Instance.UpdateData(data);
        StartUI.SetActive(true);
    }

    public void SelectGender(int _no)
    {
        if (_no == 0)
        {
            gender[0].interactable = false;
            gender[1].interactable = true;
        }
        else
        {
            gender[1].interactable = false;
            gender[0].interactable = true;
        }

        usergender = _no;
        //SingletonDataManager.userData.characterNo = _no;

        LocalData data = DatabaseManager.Instance.GetLocalData();
        //data.name = username;
        data.characterNo = usergender;
        DatabaseManager.Instance.UpdateData(data);

    }



    #region Mini game complete

    [Header("Game Complete")]
    [SerializeField] GameObject gameCompleteUI;
    [SerializeField] TMP_Text txt_gamestatus;
    [SerializeField] TMP_Text txt_gamename;
    [SerializeField] TMP_Text txt_scoreChange;
    public void OpenGameCompleteUI(bool won,int gameIndex)
    {
        switch (gameIndex)
        {
            case 0:
                {
                    txt_gamename.text = "Balloon Shoot Game";

                    if (won)
                    {
                        txt_gamestatus.text = "Won!";
                        txt_scoreChange.text = "Coins Change: +1";
                    }
                    else
                    {
                        txt_gamestatus.text = "Lose!";
                        txt_scoreChange.text = "Coins Change: 0";
                    }
                    break;
                }
            case 1:
                {
                    txt_gamename.text = "Cans Throw";

                    if (won)
                    {
                        txt_gamestatus.text = "Won!";
                        txt_scoreChange.text = "Coins Change: +1";
                    }
                    else
                    {
                        txt_gamestatus.text = "Lose!";
                        txt_scoreChange.text = "Coins Change: 0";
                    }
                    break;
                }
        }
        gameCompleteUI.SetActive(true);
        LeanTween.scale(gameCompleteUI.transform.GetChild(0).gameObject, Vector3.one, 0.5f).setFrom(Vector3.zero).setEase(LeanTweenType.easeInQuad);
        MetaManager.insta.myPlayer.GetComponent<MyCharacter>().ToggleMovement(false);
    }

    public void CloseCompleteUI()
    {
        LeanTween.scale(gameCompleteUI.transform.GetChild(0).gameObject, Vector3.zero, 0.3f).setFrom(Vector3.one).setEase(LeanTweenType.easeInQuad).setOnComplete(()=> {
            gameCompleteUI.SetActive(false);
        });
        MetaManager.insta.myPlayer.GetComponent<MyCharacter>().ToggleMovement(true);
    }
    #endregion


    #region FightREquest
    public void FightReq(string _userdata)
    {
        FightRequestUI.SetActive(true);
        fightRequestText.text = _userdata + " want to fight with you !";
    }

    public void FightReqAcion(bool _accept)
    {
        if (_accept)
        {

        }
        else
        {

        }
        MetaManager.insta.myPlayer.GetComponent<MyCharacter>().RequestFightAction(_accept);
        FightRequestUI.SetActive(false);
        Debug.Log("Fight Action " + _accept);
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("UpdateHealthMe", RpcTarget.All, PhotonNetwork.LocalPlayer.UserId);
    }
    [PunRPC]
    void UpdateHealthMe(string _uid)
    {
        Debug.Log("CheckID " + _uid);
    }

    #endregion
}

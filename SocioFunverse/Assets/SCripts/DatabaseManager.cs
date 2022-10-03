using Defective.JSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
public class DatabaseManager : MonoBehaviour
{
    #region Singleton
    public static DatabaseManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    
    #endregion



    private LocalData data=new LocalData();

    public static  List<MetaFunNFTLocal> allMetaDataServer = new List<MetaFunNFTLocal>();
    public LocalData GetLocalData()
    {
       
        return data;
    }


    private void Start()
    {
       StartCoroutine(getNFTAllData());
       // GetData();
    }
    IEnumerator updateProfile(int dataType, bool createnew = false)
    {

        JSONObject a = new JSONObject();
        JSONObject b = new JSONObject();
        JSONObject c = new JSONObject();
        //JSONObject d = new JSONObject();
        string url = ConstantManager.getProfile_api + PlayerPrefs.GetString("Account", "test").ToLower();
        switch (dataType)
        {
            case 0:
                if (!createnew) url += "?updateMask.fieldPaths=userdata";
                else
                {
                    data.name = "Player" + UnityEngine.Random.Range(0,99999).ToString();
                }

                c.AddField("stringValue", Newtonsoft.Json.JsonConvert.SerializeObject(data));
                b.AddField("userdata", c);
                break;
           /* case 3:
                if (!createnew) url += "?updateMask.fieldPaths=gamedata";
                c.AddField("stringValue", PlayerPrefs.GetString("data"));
                b.AddField("gamedata", c);
                break;*/
        }

        WWWForm form = new WWWForm();

        Debug.Log("TEST updateProfile");

        // Serialize body as a Json string
        //string requestBodyString = "";



        a.AddField("fields", b);

        Debug.Log(a.Print(true));

        // Convert Json body string into a byte array
        byte[] requestBodyData = System.Text.Encoding.UTF8.GetBytes(a.Print());

        using (UnityWebRequest www = UnityWebRequest.Put(url, requestBodyData))
        {
            www.method = "PATCH";

            // Set request headers i.e. conent type, authorization etc
            //www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Content-length", (requestBodyData.Length.ToString()));
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                //JSONObject obj = new JSONObject(www.downloadHandler.text);
                Debug.Log(www.downloadHandler.text);
                //Debug.Log(obj.GetField("fields").GetField("musedata").GetField("stringValue").stringValue);
                if (UIManager.insta)
                {
                    UIManager.insta.UpdatePlayerUIData(true, data);
                }
            }
        }
    }

    IEnumerator CheckProfile()
    {
        string url = ConstantManager.getProfile_api + PlayerPrefs.GetString("Account", "test").ToLower();

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            //www.method = "PATCH";

            // Set request headers i.e. conent type, authorization etc
            //www.SetRequestHeader("Content-Type", "application/json");
            // www.SetRequestHeader("Content-length", (requestBodyData.Length.ToString()));
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Profile not found " + www.downloadHandler.text);
                //Debug.Log(www.error);
                Debug.Log(www.downloadHandler.text);

                StartCoroutine(updateProfile(0, true));
            }
            else
            {
                JSONObject obj = new JSONObject(www.downloadHandler.text);
                Debug.Log(obj);
                //Debug.Log("CheckProfile " + www.downloadHandler.text);
                data = Newtonsoft.Json.JsonConvert.DeserializeObject<LocalData>(obj.GetField("fields").GetField("userdata").GetField("stringValue").stringValue);

                if (UIManager.insta) {
                    UIManager.username = data.name;
                   // UIManager.insta.UpdatePlayerUIData(true, data); 
                }

                //Debug.Log(obj.GetField("fields").GetField("musedata").GetField("stringValue").stringValue);
                if(data.transactionsInformation!=null && data.transactionsInformation.Count > 0)
                {
                    for (int i = 0; i < data.transactionsInformation.Count; i++)
                    {
                        if (data.transactionsInformation[i].transactionStatus.Equals("pending"))
                        {
                            Debug.Log("Pending Test 1");
                            EvmosManager.Instance.CheckDatabaseTransactionStatus(data.transactionsInformation[i].transactionId);                         
                        }
                    }
                }
            }
        }
    }

    public int GetUserGender()
    {
        return data.characterNo;
    }

    IEnumerator getNFTAllData()
    {
        
        using (UnityWebRequest www = UnityWebRequest.Get(ConstantManager.getgameNFTData_api))
        {
              www.timeout = 60;
              yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("getNFTAllData not found " + www.downloadHandler.text);
                //Debug.Log(www.error);
                Debug.Log(www.downloadHandler.text);

                //StartCoroutine(updateProfile(0, true));
            }
            else
            {
                Debug.Log("getNFTAllData  found " + www.downloadHandler.text);
                JSONObject obj = new JSONObject(www.downloadHandler.text);


                //data = Newtonsoft.Json.JsonConvert.DeserializeObject<LocalData>(obj.GetField("fields").GetField("data").GetField("stringValue").stringValue);
                Debug.Log("Data >>  " + obj);

                allMetaDataServer = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MetaFunNFTLocal>>(obj.GetField("fields").GetField("data").GetField("stringValue").stringValue);
                
                GetAllNFTImg();
            }
        }
    }
    public async UniTaskVoid GetAllNFTImg()
    {
        for (int i = 0; i < allMetaDataServer.Count; i++)
        {
            //StartCoroutine(GetTexture(allMetaDataServer[i].imageurl, i));
            await UniTask.Delay(UnityEngine.Random.Range(600, 1000));
            GetTexture(allMetaDataServer[i].imageurl, i);
        }

    }
    async UniTaskVoid GetTexture(string _url, int _index)
    {
        HERE:
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(_url);
        www.timeout = 20;
        await www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            await UniTask.Delay(UnityEngine.Random.Range(3000,6000));
            Debug.Log("Retry " + _url);
            goto HERE;
        }
        else
        {
            allMetaDataServer[_index].imageTexture = (((DownloadHandlerTexture)www.downloadHandler).texture);
        }
       // return true;
    }

    public Texture GetNFTTexture(int tokenId)
    {       
        MetaFunNFTLocal result = allMetaDataServer.Find(x => x.itemid == tokenId);
        return result.imageTexture;
    }

    public string GetNFTName(int tokenId)
    {
        MetaFunNFTLocal result = allMetaDataServer.Find(x => x.itemid ==tokenId);
        return result.name;
    }
    public MetaFunNFTLocal GetNFTMetaData(int tokenId)
    {
        MetaFunNFTLocal result = allMetaDataServer.Find(x => x.itemid == tokenId);
        return result;
    }


    public void GetData()
    {
        StartCoroutine(CheckProfile());
        //ConvertEpochToDatatime(1659504437);
    }

    public void UpdateData(LocalData localData)
    {
        data = localData;
        StartCoroutine(updateProfile(0));
    }
    async public void UpdateSpinData()
    {
        data.last_spin_time =(await GetCurrentTime()).ToString();
        StartCoroutine(updateProfile(0));
    }
   
  /*  public DateTime ConvertEpochToDatatime(long epochSeconds) {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(epochSeconds);
        DateTime dateTime = dateTimeOffset.DateTime;
        
        return dateTime;
    }
*/
    async public Task<long> GetCurrentTime()
    {
        
        string result =await EvmosManager.Instance.CheckTimeStatus();
       
        long currentEpoch;
        if (!string.IsNullOrEmpty(result))
        {
            currentEpoch=long.Parse(result);
        }
        else
        {
            currentEpoch = 1659504437;
        }
        // Get Current EPOCH Time
       // DateTime currentTime= ConvertEpochToDatatime(currentEpoch);
        return currentEpoch;
    }

    public void AddTransaction(string TransId, string status,int _shopId)
    {
        TranscationInfo info = new TranscationInfo(TransId, status);
        switch (_shopId)
        {
            case 0:
                {
                    info.coinAmount = 2;
                    break;
                }
            case 1:
                {
                    info.coinAmount = 5;
                    break;
                }
            case 2:
                {
                    info.coinAmount = 10;
                    break;
                }
            case 3:
                {
                    info.coinAmount = 20;
                    break;
                }
        }

        data.transactionsInformation.Add(info);
        UpdateData(data);
    }

    public void ChangeTransactionStatus(string transID, string txConfirmed)
    {
        Debug.Log("Changing Database " + transID + " " + txConfirmed);
        TranscationInfo trans_info = data.transactionsInformation.Find(x => x.transactionId == transID);
        if (trans_info != null)
        {
            int index = data.transactionsInformation.IndexOf(trans_info);
            trans_info.transactionStatus = txConfirmed;
            data.transactionsInformation[index] = trans_info;
            if (txConfirmed.Equals("success"))
            {
                data.score += trans_info.coinAmount;
                data.transactionsInformation.RemoveAt(index);
            }
            UIManager.insta.ShowCoinPurchaseStatus(trans_info);

            UpdateData(data);

            if (UIManager.insta)
            {
              ///  UIManager.insta.UpdatePlayerUIData(true, data);
            }
        }

       
    }
    public void ChangeGenderAndNameData(int user_gender, string username)
    {
        data.characterNo = user_gender;
        data.name = username;
        UpdateData(data);
       // UIManager.username = username;        
    }

    public string GetUserName()
    {
        if (data != null)
        {
            return data.name;
        }
        else
        {
            return "";
        }
    }
}
[System.Serializable]
public class LocalData
{
    public string name;
    public int gameWon = 0;
    public int gameLoss = 0;
    public int score = 0;
    public int characterNo = 0;
    public int level = 0;
    public int selected_road=0;
    public string last_spin_time= "0";
    public int xp = 0;
    public List<TranscationInfo> transactionsInformation = new List<TranscationInfo>();

    public LocalData()
    {
        name = "Player";
        gameWon = 0;
        gameLoss = 0;
        score = 0;
        characterNo = 0;
        characterNo = 0;
        xp = 0;
        level = 0;
        selected_road=0;
        last_spin_time = "0";
        transactionsInformation = new List<TranscationInfo>();
    }

}
[System.Serializable]
public class TranscationInfo
{
    public string transactionId;
    public string transactionStatus;
    public int coinAmount;
    public TranscationInfo(string Id, string status)
    {
        transactionId = Id;
        transactionStatus = status;        
    }
}
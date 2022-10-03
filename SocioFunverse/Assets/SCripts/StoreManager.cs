using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public static StoreManager insta;
    [SerializeField] GameObject itemPanelUI;
    [SerializeField] GameObject itemPurchaseUI;

    //item panel stuff
    //[SerializeField] Button[] itemButtons;


    //purcahse panel stuff
    [SerializeField] RawImage purchaseItemImg;
    [SerializeField] TMP_Text purchaseItemText;
    [SerializeField] TMP_Text purchaseItemCostText;

    int currentSelectedItem = -1;

    [SerializeField] Transform itemParent;
    //item panel stuff
    [SerializeField] GameObject itemButtonPrefab;

    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject loadingPanel;

    private void Awake()
    {
        insta = this;
    }


    
    private void OnEnable()
    {
        ClosePurchasePanel();

        mainPanel.SetActive(false);
        loadingPanel.SetActive(true);

       

        foreach (Transform child in itemParent)
        {
            Destroy(child.gameObject);
        }

        CovalentManager.insta.GetNFTUserBalance();

        StopCoroutine(instantiateShop());
        StartCoroutine(instantiateShop());

        //CovalentManager.insta.GetNFTUserBalance();

        
        //SingletonDataManager.insta.LoadPurchasedItems();
    }

    IEnumerator instantiateShop()
    {
        yield return new WaitUntil(()=> !CovalentManager.insta.loadingData);
        for (int i = 0; i < DatabaseManager.allMetaDataServer.Count; i++)
        {
            bool check = false;
            bool unlocked = false;

            Debug.Log(CovalentManager.insta.myTokenID.Count);

            for (int j = 0; j < CovalentManager.insta.myTokenID.Count; j++)
            {

                Debug.Log("Parse Value : " + Int32.Parse(CovalentManager.insta.myTokenID[j]) + " | " + CovalentManager.insta.myTokenID[j]);
                if (Int32.Parse(CovalentManager.insta.myTokenID[j]) == DatabaseManager.allMetaDataServer[i].itemid)
                {
                    check = true;
                    //break;
                    //continue;
                }
                if (Int32.Parse(CovalentManager.insta.myTokenID[j]) == 400) unlocked = true;
            }

            Debug.Log("check " + check + " | " + DatabaseManager.allMetaDataServer[i].itemid);
            if (!check)
            {
                var temp = Instantiate(itemButtonPrefab, itemParent);
                temp.GetComponent<RawImage>().texture = DatabaseManager.allMetaDataServer[i].imageTexture;
                temp.transform.GetChild(0).GetComponent<TMP_Text>().text = DatabaseManager.allMetaDataServer[i].cost.ToString();
                var tempNo = i;
                var tempTexture = DatabaseManager.allMetaDataServer[i].imageTexture;
                temp.GetComponent<Button>().onClick.AddListener(() => SelectItem(tempNo, tempTexture));

                if (!unlocked && i != 0) temp.GetComponent<Button>().interactable = false;
            }
        }
        loadingPanel.SetActive(false);
        mainPanel.SetActive(true);

    }

    public void SelectItem(int _no, Texture _texture)
    {
        Debug.Log("Selected item " + _no);
        currentSelectedItem = _no;
        itemPanelUI.SetActive(false);
        itemPurchaseUI.SetActive(true);
        purchaseItemImg.texture = _texture;// itemButtons[_no].GetComponent<RawImage>().texture;
        purchaseItemText.text = DatabaseManager.allMetaDataServer[_no].description;
        purchaseItemCostText.text = DatabaseManager.allMetaDataServer[_no].cost.ToString();


    }

    public void purchaseItem()
    {
        Debug.Log("purchaseItem");
        LocalData data = DatabaseManager.Instance.GetLocalData();
        MetadataNFT meta = new MetadataNFT();

        if (data.score >= DatabaseManager.allMetaDataServer[currentSelectedItem].cost)
        {
            meta.itemid = DatabaseManager.allMetaDataServer[currentSelectedItem].itemid;
            meta.name = DatabaseManager.allMetaDataServer[currentSelectedItem].name;
            meta.description = DatabaseManager.allMetaDataServer[currentSelectedItem].description;
            meta.image = DatabaseManager.allMetaDataServer[currentSelectedItem].imageurl;
            //meta.itemid = SingletonDataManager.metanftlocalData[currentSelectedItem].

            // NFTPurchaser.insta.StartCoroutine(NFTPurchaser.insta.UploadNFTMetadata(Newtonsoft.Json.JsonConvert.SerializeObject(meta), SingletonDataManager.metanftlocalData[currentSelectedItem].cost, SingletonDataManager.metanftlocalData[currentSelectedItem].itemid));
            EvmosManager.Instance.purchaseItem(currentSelectedItem,false);

            
        }
        else
        {
            Debug.Log("not enough money");
            MessaeBox.insta.showMsg("No enough coins\nFight to earn coins", true);
        }
    }

    public void DeductCoins(int _no) {
        LocalData data = DatabaseManager.Instance.GetLocalData();
        data.score -= DatabaseManager.allMetaDataServer[_no].cost;
        DatabaseManager.Instance.UpdateData(data);
    }

    public void ClosePurchasePanel()
    {
        itemPanelUI.SetActive(true);
        itemPurchaseUI.SetActive(false);
        
    }

    public void CloseItemPanel()
    {
        itemPanelUI.SetActive(false);
        itemPurchaseUI.SetActive(false);
        Debug.Log("close");
        ///if (!CovalentManager.loadingData) CovalentManager.insta.GetNFTUserBalance();
        foreach (Transform child in itemParent)
        {
            Destroy(child.gameObject);
        }
        gameObject.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyNFTCollection : MonoBehaviour
{
    public static MyNFTCollection insta;
    [SerializeField] GameObject itemPanelUI;
    [SerializeField] GameObject itemPurchaseUI;

    [SerializeField] Transform itemParent;
    //item panel stuff
    [SerializeField] GameObject itemButtonPrefab;


    //purcahse panel stuff
    [SerializeField] RawImage purchaseItemImg;
    [SerializeField] TMP_Text purchaseItemText;


    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject loadingPanel;

    int currentSelectedItem = -1;

    private void Awake()
    {
        insta = this;
        this.gameObject.SetActive(false);
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


      
       
    }
    IEnumerator instantiateShop()
    {
        yield return new WaitUntil(() => !CovalentManager.insta.loadingData);

        for (int i = 0; i < CovalentManager.insta.myTokenID.Count; i++)
        {
            var temp = Instantiate(itemButtonPrefab, itemParent);
            temp.GetComponent<RawImage>().texture = DatabaseManager.allMetaDataServer[Int32.Parse(CovalentManager.insta.myTokenID[i]) - 400].imageTexture;
            var tempNo = Int32.Parse(CovalentManager.insta.myTokenID[i]) - 400;
            var tempTexture = temp.GetComponent<RawImage>().texture;
            temp.GetComponent<Button>().onClick.AddListener(() => SelectItem(tempNo, tempTexture));
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
       /// if (!CovalentManager.loadingData) CovalentManager.insta.GetNFTUserBalance();
        foreach (Transform child in itemParent)
        {
            Destroy(child.gameObject);
        }
        gameObject.SetActive(false);
    }
}

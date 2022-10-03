using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InAppManager : MonoBehaviour
{
    public static InAppManager insta;
    [SerializeField] GameObject itemPanelUI;
    [SerializeField] GameObject itemPurchaseUI;

    //item panel stuff
    //[SerializeField] Button[] itemButtons;


    //purcahse panel stuff
    [SerializeField] RawImage purchaseItemImg;
    [SerializeField] TMP_Text purchaseItemText;
    [SerializeField] TMP_Text purchaseItemCostText;

    [SerializeField] TMP_Text userBalance;

    int currentSelectedItem = -1;

    [SerializeField] Transform itemParent;
    //item panel stuff
    [SerializeField] GameObject itemButtonPrefab;

    private void Awake()
    {
        insta = this;
        this.gameObject.SetActive(false);

        EvmosManager.Instance.CheckUserBalance();
    }

    private void OnEnable()
    {
        EvmosManager.Instance.CheckUserBalance();
    }

    public void SelectItem(int _no, Texture _texture)
    {
        Debug.Log("Selected item " + _no);
        currentSelectedItem = _no;
        itemPanelUI.SetActive(false);
        itemPurchaseUI.SetActive(true);
        purchaseItemImg.texture = _texture;// itemButtons[_no].GetComponent<RawImage>().texture;
        purchaseItemText.text = SingletonDataManager.metanftlocalData[_no].description;
        purchaseItemCostText.text = SingletonDataManager.metanftlocalData[_no].cost.ToString();


    }

    public void UpdateBalance() {
        userBalance.text = "Balance : " + EvmosManager.userBalance;
    }

    public void purchaseItem(int index)
    {
        EvmosManager.Instance.CoinBuyOnSendContract(index);
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

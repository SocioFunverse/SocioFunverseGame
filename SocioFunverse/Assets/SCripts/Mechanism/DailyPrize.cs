using TMPro;
using UnityEngine;

public class DailyPrize : MonoBehaviour
{

    public static DailyPrize insta;

    [SerializeField] TMP_Text tokenBalText;
    [SerializeField] GameObject DailyUI;
    private void Awake()
    {
        insta = this;
       CoinExManager.getTokenBalance();
        DailyShowUI(false);
    }

    private void Start()
    {
        
    }

    public void RedeemToken()
    {
        CoinExManager.getDailyToken();
        DailyShowUI(false);
    }

    public void DailyShowUI(bool _show)
    {
        if (_show)
        {
            if (CoinExManager.tokenAvailable)
                DailyUI.SetActive(true);
        }
        else
        {
            DailyUI.SetActive(false);
        }
    }

    public void UpdateTokenBalance()
    {
        tokenBalText.text = CoinExManager.tokenBalance;
    }


}

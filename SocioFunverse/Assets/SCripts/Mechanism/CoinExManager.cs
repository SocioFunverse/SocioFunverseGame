using Cysharp.Threading.Tasks;
using Defective.JSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
public class CoinExManager : MonoBehaviour
{
    #region Singleton
    public static CoinExManager Instance;
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

    public const string abi = "[{\"inputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"bool\",\"name\":\"approved\",\"type\":\"bool\"}],\"name\":\"ApprovalForAll\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"previousOwner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"OwnershipTransferred\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256[]\",\"name\":\"ids\",\"type\":\"uint256[]\"},{\"indexed\":false,\"internalType\":\"uint256[]\",\"name\":\"values\",\"type\":\"uint256[]\"}],\"name\":\"TransferBatch\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"TransferSingle\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"string\",\"name\":\"value\",\"type\":\"string\"},{\"indexed\":true,\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"}],\"name\":\"URI\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"_add\",\"type\":\"address\"}],\"name\":\"GetAllUserToken\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"}],\"name\":\"balanceOf\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address[]\",\"name\":\"accounts\",\"type\":\"address[]\"},{\"internalType\":\"uint256[]\",\"name\":\"ids\",\"type\":\"uint256[]\"}],\"name\":\"balanceOfBatch\",\"outputs\":[{\"internalType\":\"uint256[]\",\"name\":\"\",\"type\":\"uint256[]\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"_itemId\",\"type\":\"uint256\"}],\"name\":\"buyCoins\",\"outputs\":[],\"stateMutability\":\"payable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"_tokenId\",\"type\":\"uint256\"},{\"internalType\":\"string\",\"name\":\"_tokenUrl\",\"type\":\"string\"}],\"name\":\"buyNonBurnItem\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"getCurrentTime\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"_result\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"}],\"name\":\"isApprovedForAll\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"name\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"renounceOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256[]\",\"name\":\"ids\",\"type\":\"uint256[]\"},{\"internalType\":\"uint256[]\",\"name\":\"amounts\",\"type\":\"uint256[]\"},{\"internalType\":\"bytes\",\"name\":\"data\",\"type\":\"bytes\"}],\"name\":\"safeBatchTransferFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"},{\"internalType\":\"bytes\",\"name\":\"data\",\"type\":\"bytes\"}],\"name\":\"safeTransferFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"internalType\":\"bool\",\"name\":\"approved\",\"type\":\"bool\"}],\"name\":\"setApprovalForAll\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bytes4\",\"name\":\"interfaceId\",\"type\":\"bytes4\"}],\"name\":\"supportsInterface\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"transferOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"}],\"name\":\"uri\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"_recipient\",\"type\":\"address\"}],\"name\":\"withdraw\",\"outputs\":[],\"stateMutability\":\"payable\",\"type\":\"function\"}]";

    public const string contract = "0xca39deE8Bc83d428aA8C87a26D93798ed92BE243";

    const string abiERC20 = "[{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"initialSupply\",\"type\":\"uint256\"}],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"Approval\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"Transfer\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"name\":\"_LastPrizeTime\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"}],\"name\":\"allowance\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"approve\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"}],\"name\":\"balanceOf\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"dailyPrize\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"decimals\",\"outputs\":[{\"internalType\":\"uint8\",\"name\":\"\",\"type\":\"uint8\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"subtractedValue\",\"type\":\"uint256\"}],\"name\":\"decreaseAllowance\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"getSmartContractBalance\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"_account\",\"type\":\"address\"}],\"name\":\"getuserBalance\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"_account\",\"type\":\"address\"}],\"name\":\"getuserTokeStatus\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"addedValue\",\"type\":\"uint256\"}],\"name\":\"increaseAllowance\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"name\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"symbol\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"totalSupply\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"totalSupply_\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"transfer\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"transferFrom\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"_another\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"_amount\",\"type\":\"uint256\"}],\"name\":\"withdrawErc20\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";

    // address of contract
    const string contractERC20 = "0x90f1D5fdACb1a3ce35AbE6F360A87CF34676a838";

    const string chain = "CoinEx";
    // set network mainnet, testnet
    const string network = "testnet";
    const string chainId = "53";

    const string networkRPC = "https://testnet-rpc.coinex.net";



    float[] coinCost = { 0.25f, 0.50f, 0.75f, 1.00f};

    public static float userBalance = 0;

    [DllImport("__Internal")]
    private static extern void Web3Connect();

    [DllImport("__Internal")]
    private static extern string ConnectAccount();

    [DllImport("__Internal")]
    private static extern void SetConnectAccount(string value);

    private int expirationTime;
    private string account;

    [SerializeField] TMP_Text _status;
    [SerializeField] GameObject playBTN;
    [SerializeField] GameObject loginBTN;


    public static bool tokenAvailable = false;
    public static string tokenBalance = "";

 

    private void Start()
    {
        //LoginWallet();
    }
    public async void LoginWallet()
    {
        _status.text = "Connecting...";
#if !UNITY_EDITOR
        Web3Connect();
        OnConnected();
#else
        // get current timestamp
        int timestamp = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        // set expiration time
        int expirationTime = timestamp + 60;
        // set message
        string message = "Socio Funverse\nAuthentication\n"+ expirationTime.ToString();
        // sign message
        string signature = await Web3Wallet.Sign(message);
        // verify account
        string account = await EVM.Verify(message, signature);
        int now = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        // validate
        if (account.Length == 42 && expirationTime >= now)
        {
            // save account
            PlayerPrefs.SetString("Account", account);

            print("Account: " + account);
            _status.text = "connected : " + account;
            CheckUserBalance();



            if (DatabaseManager.Instance)
            {
                DatabaseManager.Instance.GetData();
            }
            // load next scene
        }
        playBTN.SetActive(true);
        loginBTN.SetActive(false);
        SingletonDataManager.userethAdd = account;
        CovalentManager.insta.GetNFTUserBalance();
        // Debug.Log("Balace " + await CheckNFTBalance());

        // getDailyToken();
        //getTokenBalance();
        checkTokenGetStatus();
        getTokenBalance();
        //

#endif

    }

    async private void OnConnected()
    {
        account = ConnectAccount();
        while (account == "")
        {
            await new WaitForSecondsRealtime(2f);
            account = ConnectAccount();
        };
        account = account.ToLower();
        // save account for next scene
        PlayerPrefs.SetString("Account", account);
        _status.text = "connected : " + account;
        // reset login message
        SetConnectAccount("");
        CheckUserBalance();

        if (DatabaseManager.Instance)
        {
            DatabaseManager.Instance.GetData();
        }
        // load next scene
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SingletonDataManager.userethAdd = account;
        playBTN.SetActive(true);
        loginBTN.SetActive(false);
        CovalentManager.insta.GetNFTUserBalance();
        //CoinBuyOnSendContract(0);

        checkTokenGetStatus();
        getTokenBalance();
    }

    #region ERC20
    async public static void getDailyToken()
    {
        Debug.Log("Redeem started");
        object[] inputParams = { };
        string method = "dailyPrize"; // buyBurnItem";// "buyCoins";

        // array of arguments for contract
        string args = Newtonsoft.Json.JsonConvert.SerializeObject(inputParams);
        // value in wei
        string value = "";// Convert.ToDecimal(wei).ToString();
        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";
        string response = "";
        // connects to user's browser wallet (metamask) to update contract state
        try
        {

#if !UNITY_EDITOR
                response = await Web3GL.SendContract(method, abiERC20, contractERC20, args, value, gasLimit, gasPrice);
                Debug.Log(response);
#else
            string data = await EVM.CreateContractData(abiERC20, method, args);
            response = await Web3Wallet.SendTransaction(chainId, contractERC20, "0", data, gasLimit, gasPrice);
            Debug.Log(response);
#endif

        }
        catch (Exception e)
        {
            Debug.Log(e);
            if (MessaeBox.insta) MessaeBox.insta.showMsg("Server Error", true);
        }

        if (!string.IsNullOrEmpty(response))
        {
            Debug.Log("In check");
            if (await CheckTransactionStatusWithTransID(response))
            {
                Debug.Log("Pass Bal");
                getTokenBalance();
            }
            else
            {
                Debug.Log("Failed Bal");
            }
        }

    }

    async public static void getTokenBalance()
    {
        // smart contract method to call
        string method = "getuserBalance";
        // array of arguments for contract
        object[] inputParams = { PlayerPrefs.GetString("Account") };
        string args = Newtonsoft.Json.JsonConvert.SerializeObject(inputParams);
        try
        {
            string response = await EVM.Call(chain, network, contractERC20, abiERC20, method, args, networkRPC);
            Debug.Log(response);
            float wei = float.Parse(response);
            float decimals = 1000000000000000000; // 18 decimals
            float eth = wei / decimals;
            // print(Convert.ToDecimal(eth).ToString());
            tokenBalance = Convert.ToDecimal(eth).ToString();
            Debug.Log("Token Bal : " + Convert.ToDecimal(eth).ToString() + " | " + response);

            if (DailyPrize.insta && UIManager.insta)
            {
                DailyPrize.insta.DailyShowUI(false);
                DailyPrize.insta.UpdateTokenBalance();
                if (UIManager.insta.GameplayUI.activeSelf && !UIManager.insta.StartUI.activeSelf)
                {
                    if (MessaeBox.insta) MessaeBox.insta.showMsg("Token redeemed", true);
                }
            }

        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }

    async public void checkTokenGetStatus()
    {
        // smart contract method to call
        string method = "getuserTokeStatus";
        // array of arguments for contract
        object[] inputParams = { PlayerPrefs.GetString("Account", account) };
        string args = Newtonsoft.Json.JsonConvert.SerializeObject(inputParams);
        try
        {
            string response = await EVM.Call(chain, network, contractERC20, abiERC20, method, args, networkRPC);
            Debug.Log(bool.Parse(response));
            // print(Convert.ToDecimal(eth).ToString());
            Debug.Log("Token checkTokenGetStatus : " + response);

            if (bool.Parse(response))
            {
                //getDailyToken();
                tokenAvailable = true;
            }
            else
            {
                tokenAvailable = false;
            }

        }
        catch (Exception e)
        {
            Debug.Log(e, this);
        }

    }
    #endregion

    #region BuyCoins
    async public void CoinBuyOnSendContract(int _pack)
    {
        if (MessaeBox.insta) MessaeBox.insta.showMsg("Coin purchase process started\nThis can up to minute", false);

        object[] inputParams = { _pack };

        float _amount = coinCost[_pack];
        float decimals = 1000000000000000000; // 18 decimals
        float wei = _amount * decimals;
        print(Convert.ToDecimal(wei).ToString() + " " + inputParams.ToString() + " + " + Newtonsoft.Json.JsonConvert.SerializeObject(inputParams));
        // smart contract method to call
        string method = "buyCoins";

        // array of arguments for contract
        string args = Newtonsoft.Json.JsonConvert.SerializeObject(inputParams);
        // value in wei
        string value = Convert.ToDecimal(wei).ToString();
        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";
        // connects to user's browser wallet (metamask) to update contract state
        try
        {


#if !UNITY_EDITOR
            string response = await Web3GL.SendContract(method, abi, contract, args, value, gasLimit, gasPrice);
            Debug.Log(response);
#else
            // string response = await EVM.c(method, abi, contract, args, value, gasLimit, gasPrice);
            // Debug.Log(response);
            string data = await EVM.CreateContractData(abi, method, args);
            string response = await Web3Wallet.SendTransaction(chainId, contract, value, data, gasLimit, gasPrice);


            Debug.Log(response);
#endif

            if (!string.IsNullOrEmpty(response))
            {
                transID = response;
                //InvokeRepeating("CheckTransactionStatus", 1, 5);
                CheckTransactionStatus(response);
                if (MessaeBox.insta) MessaeBox.insta.showMsg("Your Transaction has been recieved\nCoins will reflect to your account once it is completed!", true);
                CheckUserBalance();
            }

            if (DatabaseManager.Instance)
            {
                DatabaseManager.Instance.AddTransaction(response, "pending", _pack);
            }


        }
        catch (Exception e)
        {
            if (MessaeBox.insta) MessaeBox.insta.showMsg("Transaction Has Been Failed", true);
            Debug.Log(e, this);
        }
    }
    #endregion

    #region NonBurnNFTBuy
    async public void NonBurnNFTBuyContract(int _no, string _uri)
    {


        //string uri = "ipfs://bafyreifebcra6gmbytecmxvmro3rjbxs6oqosw3eyuldcwf2qe53gbrpxy/metadata.json";

        object[] inputParams = { _no, _uri };

        string method = "buyNonBurnItem"; // buyBurnItem";// "buyCoins";

        // array of arguments for contract
        string args = Newtonsoft.Json.JsonConvert.SerializeObject(inputParams);
        // value in wei
        string value = "";// Convert.ToDecimal(wei).ToString();
        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";
        // connects to user's browser wallet (metamask) to update contract state
        string response = "";
        try
        {

#if !UNITY_EDITOR
               response = await Web3GL.SendContract(method, abi, contract, args, value, gasLimit, gasPrice);
                Debug.Log(response);
#else
            //string response = await EVM.Call(chain, network, contract, abi, args, method, args);
            //Debug.Log(response);
            string data = await EVM.CreateContractData(abi, method, args);
            response = await Web3Wallet.SendTransaction(chainId, contract, "0", data, gasLimit, gasPrice);
            Debug.Log(response);

#endif

            if (!string.IsNullOrEmpty(response))
            {
                if (StoreManager.insta) StoreManager.insta.DeductCoins(_no);

                CheckUserBalance();
            }
            if (CovalentManager.insta)
            {
                CovalentManager.insta.GetNFTUserBalance();
            }

            if (MessaeBox.insta) MessaeBox.insta.showMsg("Your Transaction has been recieved\nIt will reflect to your account once it is completed!", true);
        }
        catch (Exception e)
        {
            Debug.Log(e, this);
            if (MessaeBox.insta) MessaeBox.insta.showMsg("Server Error", true);
        }
    }
    #endregion


    #region CheckTime
    public async UniTask<string> CheckTimeStatus()
    {
        // smart contract method to call
        string method = "getCurrentTime";
        // array of arguments for contract
        object[] inputParams = { };
        string args = Newtonsoft.Json.JsonConvert.SerializeObject(inputParams);
        try
        {
            string response = await EVM.Call(chain, network, contract, abi, method, args);
            Debug.Log(response);
            return response;

        }
        catch (Exception e)
        {
            Debug.Log(e, this);
            return "";
        }
    }
    #endregion

    #region CheckNFTBalance
    async public Task<string> CheckNFTBalance()
    {
        int first = 500;
        int skip = 0;
        try
        {
            string response = await EVM.AllErc1155(chain, network, PlayerPrefs.GetString("Account"), contract, first, skip);
            // string response = await EVM.BalanceOf(chain, network, PlayerPrefs.GetString("Account"), contract, first, skip);
            Debug.Log(response);
            return response;
        }
        catch (Exception e)
        {
            Debug.Log(e, this);
            return null;
        }
    }

    public async UniTaskVoid GetNFTList()
    {
        CovalentManager.insta.loadingData = true;
        // smart contract method to call
        //List<string> nftList = new List<string>();
        //nftList.Clear();
        CovalentManager.insta.myTokenID.Clear();
        string method = "GetAllUserToken";
        // array of arguments for contract
        object[] inputParams = { PlayerPrefs.GetString("Account") };
        string args = Newtonsoft.Json.JsonConvert.SerializeObject(inputParams);
        Debug.Log("CheckPuzzleList ===================");
        try
        {
            string response = await EVM.Call(chain, network, contract, abi, method, args, networkRPC);
            Debug.Log("CheckPuzzleList =================== Now");
            Debug.Log(response);
            string[] splitArray = response.Split(char.Parse(",")); //return one word for each string in the array
                                                                   //here, splitArray[0] = Give; splitArray[1] = me etc...

            for (int i = 0; i < splitArray.Length; i++)
            {
                if (string.IsNullOrEmpty(splitArray[i])) continue;
                CovalentManager.insta.myTokenID.Add(splitArray[i]);
            }

            //if (MetaManager.insta) MetaManager.insta.UpdatePlayerWorldProperties();
           // return nftList;

        }
        catch (Exception e)
        {
            Debug.Log(e, this);
           // return nftList;
        }
        CovalentManager.insta.loadingData = false;
    }
    #endregion

    #region CheckUserBalance
    async public void CheckUserBalance()
    {
        try
        {

            string response = await EVM.BalanceOf(chain, network, PlayerPrefs.GetString("Account"), networkRPC);
            if (!string.IsNullOrEmpty(response))
            {
                float wei = float.Parse(response);
                float decimals = 1000000000000000000; // 18 decimals
                float eth = wei / decimals;
                // print(Convert.ToDecimal(eth).ToString());
                Debug.Log(Convert.ToDecimal(eth).ToString());
                userBalance = float.Parse(Convert.ToDecimal(eth).ToString());

                if (InAppManager.insta) InAppManager.insta.UpdateBalance();
            }
        }
        catch (Exception e)
        {
            Debug.Log(e, this);
        }
    }
    #endregion

    #region CheckTRansactionStatus
    private string transID;
    async void CheckTransactionStatus(string _trxID)
    {
        Debug.Log("Check CheckTransactionStatus ");
        int _counter = 0;
        HERE:
        Debug.Log("Check Transaction " + _counter);
        _counter++;
        try
        {
            string txConfirmed = await EVM.TxStatus("", "", _trxID, networkRPC);
            Debug.Log(txConfirmed); // success, fail, pending

            if (txConfirmed.Equals("success"))
            {
                Debug.Log("success sent");
                if (DatabaseManager.Instance)
                {
                    DatabaseManager.Instance.ChangeTransactionStatus(transID, txConfirmed);
                }
            }
            else
            {
                Debug.Log("failed sent");
                if (_counter > 15)
                {
                    return;
                }
                else
                {
                    await UniTask.Delay(5000);
                    goto HERE;
                }
            }

        }
        catch (Exception e)
        {
            Debug.Log(e, this);
        }
    }

    async public static UniTask<bool> CheckTransactionStatusWithTransID(string _trxID)
    {
        Debug.Log("Check CheckTransactionStatusWithTransID ");
        int _counter = 0;
        HERE:
        Debug.Log("Check Transaction " + _counter);
        _counter++;
        try
        {
            string txConfirmed = await EVM.TxStatus("", "", _trxID, networkRPC);
            Debug.Log(txConfirmed); // success, fail, pending

            if (txConfirmed.Equals("success"))
            {
                Debug.Log("success sent");
                return true;
            }
            else
            {
                Debug.Log("failed sent");
                if (_counter > 15)
                {
                    return false;
                }
                else
                {
                    await UniTask.Delay(5000);
                    goto HERE;
                }
            }

        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        return false;
    }
    async public void CheckDatabaseTransactionStatus(string Id)
    {
        try
        {
            string txConfirmed = await EVM.TxStatus(chain, network, Id);
            print(txConfirmed); // success, fail, pending
            if (txConfirmed.Equals("success") || txConfirmed.Equals("fail"))
            {
                //CancelInvoke("CheckTransactionStatus");
                if (DatabaseManager.Instance)
                {
                    DatabaseManager.Instance.ChangeTransactionStatus(Id, txConfirmed);
                }
            }



        }
        catch (Exception e)
        {
            Debug.Log(e, this);
        }
    }

    #endregion

    #region getMetaData
    async public void getMetaData()
    {

        try
        {
            string response = await ERC1155.URI(chain, network, contract, "400", networkRPC);
            Debug.Log(response);
        }
        catch (Exception e)
        {
            Debug.Log(e, this);
        }
    }
    #endregion

    #region NFTUploaded


    public void purchaseItem(int _id, bool _skin)
    {
        Debug.Log("purchaseItem");

        MetadataNFT meta = new MetadataNFT();


        meta.itemid = DatabaseManager.allMetaDataServer[_id].itemid;
        meta.name = DatabaseManager.allMetaDataServer[_id].name;
        meta.description = DatabaseManager.allMetaDataServer[_id].description;
        meta.image = DatabaseManager.allMetaDataServer[_id].imageurl;

        StartCoroutine(Upload(Newtonsoft.Json.JsonConvert.SerializeObject(meta), _id, _skin));

    }

    IEnumerator Upload(string _metadata, int _id, bool _skin)
    {

        if (MessaeBox.insta) MessaeBox.insta.showMsg("NFT purchase process started\nThis can up to minute", false);
        Debug.Log("UploadNFTMetadata uploading " + _metadata);
        var form = new WWWForm();
        form.AddBinaryData("file", System.Text.Encoding.UTF8.GetBytes(_metadata), "metadata.json", "application/json");
        using (UnityWebRequest www = UnityWebRequest.Post("https://api.nft.storage/upload", form))
        {
            www.SetRequestHeader("Authorization", "Bearer " + ConstantManager.nftStorage_key);
            www.timeout = 60;
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError || www.isNetworkError)
            {
                Debug.Log(www.error);
                Debug.Log("UploadNFTMetadata upload error " + www.downloadHandler.text);
                if (MessaeBox.insta) MessaeBox.insta.showMsg("Server error\nPlease try again", true);


            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log("UploadNFTMetadata upload complete! " + www.downloadHandler.text);

                JSONObject j = new JSONObject(www.downloadHandler.text);
                if (j.HasField("ok"))
                {
                    if (j.GetField("ok").boolValue)
                    {
                        if (!string.IsNullOrEmpty(j.GetField("value").GetField("cid").stringValue))
                        {
                            SingletonDataManager.nftmetaCDI = @"ipfs://" + j.GetField("value").GetField("cid").stringValue + "/metadata.json";

                            Debug.Log("Metadata saved successfully " + _id + " | " + SingletonDataManager.nftmetaCDI);
                            if (!_skin) NonBurnNFTBuyContract(_id, SingletonDataManager.nftmetaCDI);
                            //if (!_skin) NonBurnNFTBuyContract(_id, j.GetField("value").GetField("url").stringValue);
                            yield break;
                        }
                    }
                }


                if (MessaeBox.insta) MessaeBox.insta.showMsg("Server error\nPlease try again", true);


            }
        }
    }

    IEnumerator UploadNFTMetadata(string _metadata, int _id, bool _skin)
    {
        if (MessaeBox.insta) MessaeBox.insta.showMsg("NFT purchase process started\nThis can up to minute", false);
        Debug.Log("Creating and saving metadata to IPFS..." + _metadata);
        WWWForm form = new WWWForm();
        form.AddField("meta", _metadata);

        using (UnityWebRequest www = UnityWebRequest.Post("https://api.nft.storage/store", form))
        {
            www.SetRequestHeader("Authorization", "Bearer " + ConstantManager.nftStorage_key);
            www.timeout = 40;
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                Debug.Log("UploadNFTMetadata upload error " + www.downloadHandler.text);

                if (_skin)
                {
                    if (MessaeBox.insta)
                    {
                        /// MessaeBox.insta.ShowRetryPopup(_id);
                    }
                }
                else
                {
                    if (MessaeBox.insta) MessaeBox.insta.showMsg("Server error\nPlease try again", true);
                }
                www.Abort();
                www.Dispose();
            }
            else
            {
                Debug.Log("UploadNFTMetadata upload complete! " + www.downloadHandler.text);

                JSONObject j = new JSONObject(www.downloadHandler.text);
                if (j.HasField("value"))
                {
                    //Debug.Log("Predata " + j.GetField("value").GetField("ipnft").stringValue);
                    SingletonDataManager.nftmetaCDI = j.GetField("value").GetField("url").stringValue; //ipnft
                    //SingletonDataManager.tokenID = j.GetField("value").GetField("ipnft").stringValue; //ipnft
                    Debug.Log("Metadata saved successfully");
                    //PurchaseItem(cost, _id);
                    if (!_skin) NonBurnNFTBuyContract(_id, j.GetField("value").GetField("url").stringValue);
                }
            }
        }
    }
    #endregion
}

using System;
using UnityEngine;

public class MissionManager : MonoBehaviour
{

    [SerializeField] Transform targetObjs;

    public int Counter;
    public int maxTarget;
    [SerializeField] float maxTime;
    float remainingTime;
    [SerializeField] int coinsOnWin;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        UIManager.insta.timerText.text = "";
        Counter = 0;
        foreach (Transform t in targetObjs)
        {
            t.gameObject.SetActive(true);
        }

        MyCharacter.missionMan = this;
        remainingTime = maxTime;
    }

    private void OnDisable()
    {
        MetaManager.isMission = false;
        MyCharacter.missionMan = null;
        foreach (Transform t in targetObjs)
        {
            t.gameObject.SetActive(false);
        }
        UIManager.insta.timerText.text = "";


    }

    // Update is called once per frame
    void Update()
    {

        if (remainingTime > 0 && Counter < maxTarget)
        {
            remainingTime -= Time.deltaTime;
            Debug.Log("Time" + (Mathf.RoundToInt(remainingTime)));
            UIManager.insta.timerText.text =  "Time Left : " + FormatTime(remainingTime) + "\n" + "Collected : " + Counter +"/" + maxTarget;
        }
        else if(Counter < maxTarget)
        {
            Debug.Log("Game Over");
            gameObject.SetActive(false);
            //MessaeBox.insta.showMsg("Time over", true);
            UIManager.insta.OpenGameCompleteUI(false, 2);
            return;
        }
        if (Counter >= maxTarget)
        {
            Debug.Log("GameWin");
            UIManager.insta.OpenGameCompleteUI(true, 2);
            if (DatabaseManager.Instance)
            {
                LocalData data = DatabaseManager.Instance.GetLocalData();
                data.score += coinsOnWin; ;
                DatabaseManager.Instance.GetLocalData();
                DatabaseManager.Instance.UpdateData(data);
                UIManager.insta.UpdatePlayerUIData(true, data);
            }
            gameObject.SetActive(false);
        }
        //if (maxTime % 1 == 0) {

        // }
    }

    public string FormatTime(float time)
    {
        // var ts = TimeSpan.FromSeconds(time);
        //return string.Format("{0:00}:{1:00}", ts.TotalMinutes, ts.Seconds);
        time = time * 1000;
        int minutes = (int)time / 60000;
        int seconds = (int)time / 1000 - 60 * minutes;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

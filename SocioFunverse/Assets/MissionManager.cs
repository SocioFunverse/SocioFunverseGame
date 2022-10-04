using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{

    [SerializeField] GameObject targetObjs;

    public static int Counter;
    public int maxTarget;
    [SerializeField] float maxTime;
    float remainingTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        remainingTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0) {
            remainingTime -= Time.deltaTime;
            Debug.Log("Time" + (Mathf.RoundToInt(remainingTime)));
        }
        //if (maxTime % 1 == 0) {
            
       // }
    }
}

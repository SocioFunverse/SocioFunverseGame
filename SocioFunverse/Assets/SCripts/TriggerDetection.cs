using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
   // [SerializeField] MiniGame mini_game;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("granade_ball"))
        {
            /*mini_game.wonGame = true;
            mini_game.FinishMiniGame();*/
        }
    }
}

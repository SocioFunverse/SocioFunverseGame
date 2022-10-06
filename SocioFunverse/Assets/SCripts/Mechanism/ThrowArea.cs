using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowArea : MonoBehaviour
{
    #region Singleton
    public static ThrowArea Instance;
    private void Awake()
    {   
         Instance = this;        
    }
    #endregion

    [SerializeField] int counter;
    [Header("TrashCan Properties")]
    [SerializeField] GameObject[] all_trashcans;
    [SerializeField] float min_height;    
    [SerializeField] int to_complete_counter;

    [SerializeField] Vector3[] start_pos_trashcans;

    // Start is called before the first frame update
    void Start()
    {
        start_pos_trashcans = new Vector3[all_trashcans.Length];
        for (int i = 0; i < start_pos_trashcans.Length; i++)
        {
            start_pos_trashcans[i] = all_trashcans[i].transform.position;
        }
    }

    public void ResetGame()
    {

       
                    for (int i = 0; i < all_trashcans.Length; i++)
                    {
                        Debug.Log("RESET GAME");
                        Rigidbody rb = all_trashcans[i].GetComponent<Rigidbody>();

                        rb.isKinematic = true;
                        rb.useGravity = false;

                        rb.velocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                        all_trashcans[i].transform.position = start_pos_trashcans[i];
                        all_trashcans[i].transform.rotation = Quaternion.identity;

                        rb.isKinematic = false;
                        rb.useGravity = true;
                    }
                   
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public bool CheckMiniGame()
    {
        counter = 0;

        for (int i = 0; i < all_trashcans.Length; i++)
        {
            if (all_trashcans[i].transform.position.y < min_height)
            {
                counter++;
            }
        }
        if (counter >= to_complete_counter)
        {
            return true;
        }

        return false;

    }
}

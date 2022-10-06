using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float time;
    void Start()
    {
        Destroy(this.gameObject, time);
    }
   
}

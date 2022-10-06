using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunparkAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int anima = 0;
    void Start()
    {
       if(anima==0) LeanTween.rotateAround(gameObject, Vector3.up, 360, Random.Range(5,15)).setLoopClamp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

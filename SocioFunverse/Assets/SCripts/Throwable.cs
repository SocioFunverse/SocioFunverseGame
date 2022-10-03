using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] LayerMask AffectedObjectLayer;

    public float radius = 3f;
    public float explodeForce= 700f;
    [SerializeField] GameObject Explode_VFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Explode(float delay=3f)
    {
        StartCoroutine(explode(delay));
    }
    IEnumerator explode(float delay)
    {
        yield return new WaitForSeconds(delay);

        AudioManager.insta.playSound(18);
        //Instantiate Explode Particle Effect
        GameObject particle;
        particle = PhotonNetwork.Instantiate(Explode_VFX.name, this.transform.position, Explode_VFX.transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, AffectedObjectLayer);
        foreach (Collider obj in colliders)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explodeForce,transform.position,radius);
            }
        }
        this.gameObject.SetActive(false);

        yield return new WaitForSeconds(3f);
        if (particle != null)
        {
            PhotonNetwork.Destroy(particle.GetComponent<PhotonView>());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name.Equals("can tall"))
        {
            AudioManager.insta.playSound(20);
        }
    }
}

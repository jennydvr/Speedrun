using UnityEngine;
using System.Collections;

public class HoleTrigger : MonoBehaviour
{
    public ManagerPlanet mPlanet;
    // Use this for initialization
    void Start()
    {
        mPlanet = (ManagerPlanet)GameObject.FindObjectOfType(typeof(ManagerPlanet));
    }
    // Update is called once per frame
    void Update()
    {
	
    }

    void OnTriggerEnter(Collider other){
    
        Debug.Log(other.gameObject);
        if (other.tag.Equals("Planet"))
        {
            Debug.Log("0");
           // mPlanet.RemoverPlanet(other.transform.parent.gameObject);
           
        }
    }
}


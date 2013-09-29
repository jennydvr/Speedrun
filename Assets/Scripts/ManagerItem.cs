using UnityEngine;
using System.Collections.Generic;

public class ManagerItem : MonoBehaviour {

    private List<PowerActivator> Powers;

	void Start () {
        Powers = new List<PowerActivator> ();
        Powers.Add (new Bomb ());
        Powers.Add (new Speed ());
        Powers.Add (new Tapon ());
	}

	void Update () {
	
	}

    public void TryUsingBomb() {
        if (Powers[0].Unlocked()) {
            Debug.Log ("Bomba dice: TOMEN SAPOS");
            Powers [0].Use ();
        }
    }    

    public void TryUsingTapon() {
        if (Powers[2].Unlocked()) {
            Debug.Log ("Tapon dice: GANEEEEEEEEEE");
            Powers [2].Use ();
        }
    }    

    public void TryUsingSpeed() {
        if (Powers[1].Unlocked()) {
            Debug.Log ("Speed dice: MUEVELA GORDA");
            Powers [1].Use ();
        }
    }

}

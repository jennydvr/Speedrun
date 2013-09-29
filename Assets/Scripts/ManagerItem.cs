using UnityEngine;
using System.Collections.Generic;

public class ManagerItem : MonoBehaviour {

    private List<PowerActivator> Powers;


    public int CostGrassTapon = 1;
    public int CostFireTapon = 1;
    public int CostEarthTapon = 1;
    public int CostWindTapon = 1;
    public UILabel FireCountTapon;
    public UILabel WindCountTapon;
    public UILabel GrassCountTapon;
    public UILabel EarthCountTapon;
    
    public int CostGrassSpeed = 1;
    public int CostWindSpeed = 1;
    public UILabel WindCountSpeed;
    public UILabel GrassCountSpeed;

    public int CostFireBomb = 1;
    public int CostEarthBomb = 1;
    public UILabel FireCountBomb;
    public UILabel EarthCountBomb;

	void Start () {
        Powers = new List<PowerActivator> ();
        Powers.Add (new Bomb (CostEarthBomb,CostFireBomb));
        FireCountBomb.text = "x "+CostFireBomb.ToString();
        EarthCountBomb.text = "x "+CostEarthBomb.ToString();

        Powers.Add (new Speed (CostWindSpeed,CostGrassSpeed));
        WindCountSpeed.text = "x "+CostWindSpeed.ToString();
        GrassCountSpeed.text = "x "+CostGrassSpeed.ToString();

        Powers.Add (new Tapon (CostEarthTapon,CostFireTapon,CostWindTapon,CostGrassTapon));
        FireCountTapon.text = "x "+CostFireTapon.ToString();
        WindCountTapon.text = "x "+CostWindTapon.ToString();
        EarthCountTapon.text = "x "+CostEarthTapon.ToString();
        GrassCountTapon.text = "x "+CostGrassTapon.ToString();
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

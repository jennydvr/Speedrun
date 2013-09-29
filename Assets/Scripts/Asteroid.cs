using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

    public ManagerAsteroids master;
    public Vector3 HoleCenter;
    public float MinDistanceToHole = 1.0f;

	void Start () {
        master = (ManagerAsteroids)GameObject.FindObjectOfType(typeof(ManagerAsteroids));
	}

	void Update () {
        if ( Vector3.Distance( HoleCenter,new Vector3(transform.position.x,transform.position.y,0)) < MinDistanceToHole)
        {
            master.RemoverAsteroid();

        }
	}
}

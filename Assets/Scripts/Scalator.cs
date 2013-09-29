using UnityEngine;
using System.Collections;

public class Scalator : MonoBehaviour {

    public float Speed = 4.0f;

    public Vector3 InitialPoint;
    public Vector3 EndPoint;

    private float InvMaxDistance;
    private Transform Me;

	void Start () {
        Me = transform;
        InvMaxDistance = 1 / (EndPoint - InitialPoint).sqrMagnitude;
	}

	void Update () {
        float factor = (Me.position - EndPoint).sqrMagnitude * InvMaxDistance * Speed;

        // Escalo el factor
        if (factor > 1)
            factor = 1;
        else if (factor < 0.25f)
            factor = 0.25f;

        Me.localScale = Vector3.one * factor;
	}
}

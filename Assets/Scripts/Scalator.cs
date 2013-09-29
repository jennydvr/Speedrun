using UnityEngine;
using System.Collections.Generic;

public class Scalator : MonoBehaviour {

    public float Speed = 4.0f;
    public float MaxSize = 1.25f;
    public float MinSize = 0.25f;

    public Vector3 InitialPoint;
    public Vector3 EndPoint;

    public List<ParticleSystem> Particles;
    public List<Vector2> Limits;

    private float InvMaxDistance;
    private Transform Me;

	void Start () {
        Me = transform;
        InvMaxDistance = 1 / (EndPoint - InitialPoint).sqrMagnitude;
	}

	void LateUpdate () {
        float factor = (Me.position - EndPoint).sqrMagnitude * InvMaxDistance * Speed;

        // Escalo el factor
        if (factor > MaxSize)
            factor = MaxSize;
        else if (factor < MinSize)
            factor = MinSize;

        // Me escalo a mi mismo
        Me.localScale = Vector3.one * factor;

        // Escalo mis particulas
        for (int i = 0; i != Particles.Count; ++i) {
            // Normalizo el factor a [0, 1]
            float fn = (factor - MinSize) / (MaxSize - MinSize);

            // Llevo el startLifetime dentro de los limites
            Particles[i].startLifetime = fn * (Limits[i].y - Limits[i].x) + Limits[i].x;
        }
	}
}

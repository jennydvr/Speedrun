using UnityEngine;
using System.Collections;

public enum PlanetType : byte {
    Wind,
    Fire,
    Earth,
    Plant,
    None
}

public class ResourcesCount : MonoBehaviour {

    #region Variables

    public static float HarvestRate = 0.5f;

    public static float Aire = 0;
    public static float Fuego = 0;
    public static float Tierra = 0;
    public static float Planta = 0;
    
    public float Wind;
    public float Fire;
    public float Earth;
    public float Plant;

    #endregion

    #region Unity

	void Update () {
        Wind = Mathf.CeilToInt(Aire);
        Fire = Mathf.CeilToInt(Fuego);
        Earth = Mathf.CeilToInt(Tierra);
        Plant = Mathf.CeilToInt(Planta);
	}

    #endregion

    #region Static Functions

    public static void GatherResource(float amount, PlanetType type) {
        switch (type) {
        case PlanetType.Earth:
            Tierra += amount;
            break;
        case PlanetType.Wind:
            Aire += amount;
            break;
        case PlanetType.Fire:
            Fuego += amount;
            break;
        case PlanetType.Plant:
            Planta += amount;
            break;
        }
    }

    #endregion

}

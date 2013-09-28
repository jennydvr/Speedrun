using UnityEngine;
using System.Collections;

public class Joystick : MonoBehaviour {

    #region Variables

    /// <summary>
    /// Plano para hacer calculos de la posicion del mouse
    /// </summary>
    private Plane ScreenPlane;

    #endregion

    #region Unity

    /// <summary>
    /// Inicializacion
    /// </summary>
    private void Start () {
        ScreenPlane = new Plane(Vector3.up, 20.0f);
	}

	/// <summary>
    /// Actualizacion
    /// </summary>
	private void Update () {

        // Boton izquierdo pulsado
        if (Input.GetMouseButtonDown(0)) {

            // Lanzo un rayo para ver con que choco
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Si toco un collider y es un planeta, seleccionar
            if (Planet.SelectedPlanets.Count < 2 && Physics.Raycast(ray, out hit)) {
                Planet planet = hit.collider.gameObject.GetComponent<Planet>();
                planet.PlanetSelection(true);

                if (Planet.SelectedPlanets.Count == 2)
                    Planet.Transfer();

            } else {
                // Deseleccionar planeta seleccionado
                if (Planet.SelectedPlanets.Count == 1)
                {
                    Planet.SelectedPlanets[0].PlanetSelection(false);
                }

                // Mover reina
                float ent;

                if (ScreenPlane.Raycast(ray, out ent))
                {
                    Vector3 hitPoint = ray.GetPoint(ent);
                    hitPoint.z = 0;
                }
            }
        }
	}

    #endregion

}

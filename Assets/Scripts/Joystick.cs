using UnityEngine;
using System.Collections;

public class Joystick : MonoBehaviour {

    #region Variables

    public static bool CanMoveQueen = true;
    public static Ray ScreenRay;
    public LayerMask Planets = -1;
    #endregion

    #region Unity

	private void Update () {

        // Boton izquierdo pulsado
        if (Input.GetMouseButtonDown(0)) {
            // Lanzo un rayo para ver con que choco
            ScreenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Si toco un collider y es un planeta, seleccionar
            if (Planet.CanSelectPlanet() && Physics.Raycast(ScreenRay, out hit,100.0f,Planets)) {
                // No puedo mover a la reina
                CanMoveQueen = false;

                // Reviso si estoy seleccionando un planeta
                Planet.IsPlanetSelected(hit.collider.GetComponent<Planet>());
            } else {
                // Puedo mover a la reina de acuerdo a si estoy o no deseleccionando un planeta
                CanMoveQueen = Planet.DeselectingLastPlanet();
            }
        }
	}

    #endregion

}

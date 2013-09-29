using UnityEngine;
using System.Collections;

public class Joystick : MonoBehaviour {

    #region Variables

    public static bool TryingToSelect = false;
    public static bool CanMoveQueen = true;
    public static Ray ScreenRay;
    public static Plane ScreenPlane;

    public LayerMask Planets = -1;
    public LineRenderer MouseLine;

    #endregion

    #region Unity

    private void Start() {
        ScreenPlane = new Plane(Vector3.forward,new Vector3(0, 0, 11));
        MouseLine.enabled = false;
    }

    private void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        CheckGameplayActions(ray);
        DrawMouseTrail(ray);
	}

    #endregion

    #region Private Methods

    private void CheckGameplayActions(Ray ray) {
        // Boton izquierdo pulsado
        if (Input.GetMouseButtonDown(0)) {
            // Lanzo un rayo para ver con que choco
            ScreenRay = ray;
            RaycastHit hit;

            // Si toco un collider y es un planeta, seleccionar
            if (Planet.CanSelectPlanet() && Physics.Raycast(ScreenRay, out hit, 100.0f, Planets)) {
                // No puedo mover a la reina
                CanMoveQueen = false;

                // Trato de seleccionar
                Planet.TrySelecting(hit.collider.GetComponent<Planet>());
            } else {
                // Puedo mover a la reina de acuerdo a si estoy o no deseleccionando un planeta
                CanMoveQueen = Planet.DeselectingLastPlanet();
            }
        }
    }

    private void DrawMouseTrail(Ray ray) {
        // El fin de la linea esta siempre en la posicion del mouse
        float ent;
        if (ScreenPlane.Raycast(ray, out ent)) {
            Vector3 hitPoint = ray.GetPoint(ent);
            MouseLine.SetPosition(1, hitPoint);
        }

        Vector3 pos = Planet.OnePlanetSelected();
        if (pos != Vector3.one * float.PositiveInfinity) {
            pos.z = 11;
            MouseLine.SetPosition(0, pos);
            MouseLine.enabled = true;
        } else {
            MouseLine.enabled = false;
        }
    }

    #endregion

}

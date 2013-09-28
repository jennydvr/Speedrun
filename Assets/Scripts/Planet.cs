using UnityEngine;
using System.Collections.Generic;

public class Planet : MonoBehaviour {

    #region Static Variables

    private static List<Planet> SelectedPlanets = new List<Planet>();

    public static float TransferTime = 1.5f;

    public LayerMask QueenLayer = -1, ObstaclesLayer = -1;

    #endregion

    #region Instance Variables

    private Transform Me;

    private bool Selected = false;

    private bool Transfering = false;

    private float StartTransferTime;

    #endregion

    #region Unity

	private void Start() {
        Me = transform;
	}

    private void Update() {
        if (Transfering && Time.time - StartTransferTime >= TransferTime) {
            EndTransfer();
        }
    }

    #endregion

    #region Public Methods

    public void PlanetSelection(bool select) {
        // No seleccionamos al transferir
        if (Transfering || Selected == select)
            return;

        Selected = select;
        renderer.material.color = select ? Color.red : Color.white;

        if (Selected)
            SelectedPlanets.Add(this);
        else
            SelectedPlanets.Remove(this);
    }

    public void StartTransfer() {
        Transfering = true;
        renderer.material.color = Color.blue;
        StartTransferTime = Time.time;
    }

    public void EndTransfer() {
        Transfering = false;
        PlanetSelection(false);
    }

    #endregion

    #region Static Functions

    public static bool CanSelectPlanet() {
        return Planet.SelectedPlanets.Count < 2;
    }

    public static bool DeselectingLastPlanet() {
        if (SelectedPlanets.Count == 1) {
            SelectedPlanets[0].PlanetSelection(false);
            return false;
        }

        return true;
    }

    public static void IsPlanetSelected(Planet planet) {
        // Si no hay componente planeta o ya se han seleccionado dos planetas
        if (planet == null || SelectedPlanets.Count == 2)
            return;

        // Selecciono el planeta
        planet.PlanetSelection(true);

        // Si ya hay dos planetas, hacer transferencia
        if (Planet.SelectedPlanets.Count == 2)
            Planet.Transfer();
    }

    public static void Transfer() {
        Planet one = SelectedPlanets[0];
        Planet two = SelectedPlanets[1];

        // Chequear que la reina se encuentre de por medio
        Vector3 onePos = one.transform.position;
        Vector3 twoPos = two.transform.position;
        Vector3 diff = twoPos - onePos;

        RaycastHit hit;

        if (Physics.Raycast(onePos, diff, out hit, diff.magnitude, one.QueenLayer)) {
            // Iniciar transferencia
            one.StartTransfer();
            two.StartTransfer();

        } else {
            // Deselecciono los planetas
            one.PlanetSelection(false);
            two.PlanetSelection(false);
        }
    }

    #endregion

}

using UnityEngine;
using System.Collections.Generic;

public class Planet : MonoBehaviour {

    #region Static Variables

    /// <summary>
    /// Lista de planetas en seleccion
    /// </summary>
    public static List<Planet> SelectedPlanets = new List<Planet>();

    /// <summary>
    /// Tiempo de transferencia
    /// </summary>
    public static float TransferTime = 1.5f;

    #endregion

    #region Private Variables

    /// <summary>
    /// Mi transform
    /// </summary>
    private Transform Me;

    /// <summary>
    /// Indica si el planeta se encuentra seleccionado
    /// </summary>
    private bool Selected = false;
    
    /// <summary>
    /// Indica si estamos transfiriendo
    /// </summary>
    private bool Transfering = false;

    /// <summary>
    /// Tiempo inicial de transferencia
    /// </summary>
    private float StartTransferTime;

    #endregion

    #region Unity

    /// <summary>
    /// Inicializacion
    /// </summary>
	private void Start() {
        Me = transform;
	}

    /// <summary>
    /// Actualizacion
    /// </summary>
    private void Update() {
        if (Transfering && Time.time - StartTransferTime >= TransferTime) {
            EndTransfer();
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Selecciona / deselecciona un planeta
    /// </summary>
    /// <param name="select">Seleccionar si esta en <c>true</c></param>
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

    /// <summary>
    /// Iniciar transferencia
    /// </summary>
    public void StartTransfer() {
        Transfering = true;
        renderer.material.color = Color.blue;
        StartTransferTime = Time.time;
    }

    /// <summary>
    /// Termina la transferencia
    /// </summary>
    public void EndTransfer() {
        Transfering = false;
        PlanetSelection(false);
    }

    #endregion

    #region Static Functions

    /// <summary>
    /// Transfiere cosas de un planeta a otro
    /// </summary>
    public static void Transfer() {
        SelectedPlanets[0].StartTransfer();
        SelectedPlanets[1].StartTransfer();
    }

    #endregion

}

using UnityEngine;
using System.Collections.Generic;

public class Planet : MonoBehaviour {

    #region Static Variables

    public static float TransferSpeed = 1.0f;
    public LayerMask QueenLayer = -1, ObstaclesLayer = -1;
    public static LineRenderer TransferLine;
    
    private static List<Planet> SelectedPlanets = new List<Planet>();

    #endregion

    #region Instance Variables

    public int BeesCount = 0;

    private bool TransferOrigin = false;
    private Transform Me;
    private bool Selected = false;
    private bool Transfering = false;
    private float TransferStartTime;
    private float TransferRate;
    private Planet TransferPlanet;

    #endregion

    public ManagerPlanet mPlanet;
    public Vector3 HoleCenter;
    public float MinDistanceToHole = 10.0f;
    #region Unity

	private void Start() {
        Me = transform;

        if (!TransferLine)
            TransferLine = GameObject.Find("TransferLine").GetComponent<LineRenderer>();
        mPlanet = (ManagerPlanet)GameObject.FindObjectOfType(typeof(ManagerPlanet));
	}

    private void Update() {
        if (Transfering && TransferOrigin) {

            // Transferir abejas
            if (Time.time - TransferStartTime >= TransferRate) {
                BeesCount -= 1;
                TransferPlanet.BeesCount += 1;
                TransferStartTime = Time.time;
            }

            // Revisar si la transferencia termino sin problemas
            if (BeesCount == 0) {
                EndTransfer();
                TransferPlanet.EndTransfer();
            }
            // Chequear si se interrumpe porque la abeja madre no esta
            else if (!IsQueenBeeBetween(TransferPlanet)) {
                EndTransfer();
                TransferPlanet.EndTransfer();
            }
            // Chequear si hay obstaculos de por medio
            else if (AreObstaclesBetween(TransferPlanet)) {
                // Elimino las abejas del planeta con menos abejas
                if (TransferPlanet.BeesCount > BeesCount) {
                    BeesCount = 0;
                } else {
                    TransferPlanet.BeesCount = 0;
                }

                EndTransfer();
                TransferPlanet.EndTransfer();
            }

        }
        if ( Vector3.Distance( HoleCenter,new Vector3(transform.position.x,transform.position.y,0)) < MinDistanceToHole)
        {
            mPlanet.RemoverPlanet();

        }


    }

    #endregion

    #region Private Methods

    private bool TransferEnded()
    {
        return TransferOrigin && BeesCount == 0;
    }

    private bool IsQueenBeeBetween(Planet planet) {
        Vector3 onePos = Me.position;
        Vector3 twoPos = planet.transform.position;
        Vector3 diff = twoPos - onePos;

        return Physics.Raycast(onePos, diff, diff.magnitude, QueenLayer);
    }

    private bool AreObstaclesBetween(Planet planet) {
        return false;

        /*Vector3 onePos = Me.position;
        Vector3 twoPos = planet.transform.position;
        Vector3 diff = twoPos - onePos;

        return Physics.Raycast(onePos, diff, diff.magnitude, ObstaclesLayer);*/
    }

    #endregion

    #region Public Methods

    public void PlanetSelection(bool select) {
        // No seleccionamos al transferir
        if (Transfering || Selected == select)
            return;

        Selected = select;
        //renderer.material.color = select ? Color.red : Color.white;

        if (Selected)
            SelectedPlanets.Add(this);
        else
            SelectedPlanets.Remove(this);
    }

    public void StartTransfer(Planet other, bool origin) {
        Transfering = true;
        //renderer.material.color = Color.blue;
        TransferStartTime = Time.time;
        TransferRate = TransferSpeed / (other.transform.position - Me.position).magnitude;

        TransferPlanet = other;
        TransferOrigin = origin;
    }

    public void EndTransfer() {
        Transfering = false;
        PlanetSelection(false);
        TransferLine.enabled = false;
    }

    #endregion

    #region Static Functions

    public static Vector3 OnePlanetSelected() {
        return SelectedPlanets.Count == 1 ? SelectedPlanets[0].Me.position : Vector3.one * float.PositiveInfinity;
    }

    public static bool CanSelectPlanet() {
        return SelectedPlanets.Count < 2;
    }

    public static bool DeselectingLastPlanet() {
        if (SelectedPlanets.Count == 1) {
            SelectedPlanets[0].PlanetSelection(false);
            return false;
        }

        return true;
    }

    public static bool TrySelecting(Planet planet) {
        // Si no hay componente planeta o ya se han seleccionado dos planetas
        if (planet == null || SelectedPlanets.Count == 2)
            return false;

        // Selecciono el planeta
        planet.PlanetSelection(true);

        // Si ya hay dos planetas, hacer transferencia
        if (Planet.SelectedPlanets.Count == 2)
            Planet.Transfer();

        return true;
    }

    public static void Transfer() {
        Planet one = SelectedPlanets[0];
        Planet two = SelectedPlanets[1];

        // Chequear que la transferencia sea posible
        if (one.BeesCount > 0 && one.IsQueenBeeBetween(two) && !one.AreObstaclesBetween(two)) {
            DrawTransferTrail(one.Me.position, two.Me.position);

            one.StartTransfer(two, true);
            two.StartTransfer(one, false);
        } else {
            // Deselecciono los planetas
            one.PlanetSelection(false);
            two.PlanetSelection(false);
        }
    }

    private static void DrawTransferTrail(Vector3 one, Vector3 two) {
        one.z = two.z = 11;

        TransferLine.SetPosition(0, one);
        TransferLine.SetPosition(1, two);

        TransferLine.enabled = true;
    }

    #endregion

}

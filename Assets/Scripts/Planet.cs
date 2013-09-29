using UnityEngine;
using System.Collections.Generic;

public class Planet : MonoBehaviour {

    #region Static Variables

    public static float TransferSpeed = 0.01f;
    public LayerMask QueenLayer = -1, ObstaclesLayer = -1, PlanetsLayer = -1;
    public static LineRenderer TransferLine;
    
    private static List<Planet> SelectedPlanets = new List<Planet>();

    #endregion

    #region Instance Variables

    public int BeesCount = 0;

    private bool HadBees = false;
    private bool TransferOrigin = false;
    private Transform Me;
    private bool Selected = false;
    private bool Transfering = false;
    private float TransferStartTime;
    private float TransferRate;
    private Planet TransferPlanet;

    public ResourcesID Type = ResourcesID.None;
    public ManagerPlanet mPlanet;
    public Vector3 HoleCenter;
    public float MinDistanceToHole = 10.0f;


    public ManagerResources mResources;
    public float minRandomResources = 20;
    public float maxRandomResources = 30;
    protected float maxResources;
    protected float currentResources = 0;
    #endregion

    #region Unity

	private void Start() {
        Me = transform;

        if (!TransferLine)
            TransferLine = GameObject.Find("TransferLine").GetComponent<LineRenderer>();
        mPlanet = (ManagerPlanet)GameObject.FindObjectOfType(typeof(ManagerPlanet));
        mResources = (ManagerResources)GameObject.FindObjectOfType(typeof(ManagerResources));
        maxResources = Random.Range(minRandomResources, maxRandomResources);
	}

    private void Update() {
        // HadBees cambia si el planeta nunca ha tenido abejas, pero la cuenta indica que si
        if (!HadBees && BeesCount > 0)
            HadBees = true;

        // Actualizo transferencia
        if (Transfering && TransferOrigin) {
            UpdateTransfer ();
        }
        // Si no estamos transfiriendo y hay abejas, harvestear
        else if (!Transfering && BeesCount > 0 && currentResources <= maxResources) {
            Harvest (Time.deltaTime);
        }

        // Si la distancia minima se alcanza, eliminar planeta
        if ( Vector3.Distance( HoleCenter,new Vector3(transform.position.x,transform.position.y,0)) < MinDistanceToHole)
        {
            mPlanet.RemoverPlanet();

        }


    }

    #endregion

    #region Private Methods

    private void Harvest(float time) {
        float nowResour = BeesCount * ManagerResources.HarvestRate * time;
        currentResources += nowResour;
      
        if(currentResources <= maxResources){
            mResources.GatherResource (nowResour , Type);
        }
    }

    private void UpdateTransfer() {
        // Actualizo el trail
        DrawTransferTrail(Me.position, TransferPlanet.Me.position);

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
            TransferPlanet.BeesCount = 0;

            EndTransfer();
            TransferPlanet.EndTransfer();
        }
    }

    private bool TransferEnded() {
        return TransferOrigin && BeesCount == 0;
    }

    private bool IsQueenBeeBetween(Planet planet) {
        Vector3 onePos = Me.position;
        Vector3 twoPos = planet.transform.position;
        Vector3 diff = twoPos - onePos;

        return Physics.Raycast(onePos, diff, diff.magnitude, QueenLayer);
    }

    private bool AreObstaclesBetween(Planet planet) {
        Vector3 onePos = Me.position;
        Vector3 twoPos = planet.transform.position;
        Vector3 diff = twoPos - onePos;

        bool ObstaclesBetween = false;

        // Revisar primero que no hayan asteroides
        ObstaclesBetween = Physics.Raycast(onePos, diff, diff.magnitude, ObstaclesLayer);

        // Revisar ahora que no hayan planetas
        RaycastHit hit;
        if (Physics.Raycast(onePos, diff, out hit, diff.magnitude, PlanetsLayer)) {
            ObstaclesBetween |= hit.collider.gameObject.GetInstanceID() != planet.gameObject.GetInstanceID();
        }

        return ObstaclesBetween;
    }

    #endregion

    #region Public Methods

    public void PlanetSelection(bool select) {
        // No seleccionamos al transferir
        if (Transfering || Selected == select)
            return;

        Selected = select;

        if (Selected)
            SelectedPlanets.Add(this);
        else
            SelectedPlanets.Remove(this);
    }

    public void StartTransfer(Planet other, bool origin) {
        Transfering = true;
        TransferStartTime = Time.time;
        TransferRate = TransferSpeed * (other.transform.position - Me.position).magnitude;
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
        if (one.BeesCount > 0 && (!two.HadBees || two.BeesCount > 0) && one.IsQueenBeeBetween(two) && !one.AreObstaclesBetween(two)) {
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

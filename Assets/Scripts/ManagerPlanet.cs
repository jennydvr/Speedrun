using UnityEngine;
using System.Collections;

public class ManagerPlanet : MonoBehaviour
{

    public ArrayList planetas;
    public Vector3 InitialPosition;
    public GameObject Prefab;

    public float MinTimeSpawn = 1.0f;
    public float MaxTimeSpawn = 3.0f;
    protected bool CanSpawn = true;
    // Use this for initialization
    void Start()
    {
        planetas = new ArrayList();
        AddPlanet();
        Invoke("ReadySpawn", Random.Range(MinTimeSpawn, MaxTimeSpawn));

    }


    protected void AddPlanet(){
        CanSpawn = false;

        planetas.Add(Instantiate(Prefab, InitialPosition, Prefab.transform.rotation) as GameObject);
        //((GameObject)planetas[planetas.Count -1]).GetComponent<SplineController>().InitialPos = 10;
    }
    protected void SpawnPlanet(){

       
        int pos = planetas.Count - 1;
        if (pos < 0)
            pos = 0;
        GameObject plan = ((GameObject)planetas[pos]);
        plan.SetActive(true);
        plan.GetComponent<SplineInterpolator>().Reset();
       // ((GameObject)planetas[pos]).GetComponent<SplineController>().DisableTransforms();
        plan.GetComponent<SplineController>().FollowSpline();
    }

    protected void ReadySpawn(){
        CanSpawn = true;

    }


    public void RemoverPlanet(){
      
        GameObject pla = (GameObject)planetas[0];
        planetas.RemoveAt(0);
        Destroy(pla);
    }


    // Update is called once per frame
    void Update()
    {
        if (CanSpawn)
        {
            SpawnPlanet();
            AddPlanet();
            Invoke("ReadySpawn", Random.Range(MinTimeSpawn, MaxTimeSpawn));
        }


    }
}


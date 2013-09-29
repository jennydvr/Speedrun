using UnityEngine;
using System.Collections;

public class ManagerPlanet : MonoBehaviour
{

    public ArrayList planetas;
    public Vector3 InitialPosition;
    public GameObject[] Prefab;
    public int[] PrefabProba;

    public float MinTimeSpawn = 1.0f;
    public float MaxTimeSpawn = 3.0f;
    protected bool CanSpawn = true;

    public int MinBee = 10;
    public int MaxBee = 20;
    // Use this for initialization
    void Awake(){
        planetas = new ArrayList();

        AddPlanet(Random.Range(29,32));
        AddPlanet(Random.Range(26,29));
        AddPlanet(Random.Range(23,26));
        AddPlanet(Random.Range(20,23));
        AddPlanet(Random.Range(17,20));
        AddPlanet(Random.Range(15,17));
        AddPlanet(Random.Range(13,15));
        AddPlanet(Random.Range(11,13));
        AddPlanet(Random.Range(9,11));
        AddPlanet(Random.Range(7,9));
        AddPlanet(Random.Range(5,7));
    }
    void Start()
    {
        SpawnPlanet(0);
        SpawnPlanet(1);
        SpawnPlanet(2);
        SpawnPlanet(3);
        SpawnPlanet(4);
        SpawnPlanet(5);
        SpawnPlanet(6);
        SpawnPlanet(7);
        SpawnPlanet(8);
        SpawnPlanet(9);
        SpawnPlanet(10);

        ((Planet) ( (GameObject) planetas[Random.Range(2, 7)]).GetComponentInChildren(typeof(Planet))).BeesCount =Random.Range(MinBee, MaxBee) ;



        AddPlanet();
        Invoke("ReadySpawn", Random.Range(MinTimeSpawn, MaxTimeSpawn));
       
    }


    protected void AddPlanet(){
        CanSpawn = false;
        int pos =  Random.Range(0,100);
        int SumProb = 0;
        for (int i=0; i<PrefabProba.Length; i++)
        {
            SumProb += PrefabProba[i];
            if (SumProb > pos)
            {
                pos = i;
                break;
            }
        }
       

        planetas.Add(Instantiate(Prefab[pos], InitialPosition, Prefab[pos].transform.rotation) as GameObject);
        //((GameObject)planetas[planetas.Count -1]).GetComponent<SplineController>().InitialPos = 10;
    }
    protected void AddPlanet(int initialpos){
        CanSpawn = false;
        int pos =  Random.Range(0,100);
        int SumProb = 0;
        for (int i=0; i<PrefabProba.Length; i++)
        {
            SumProb += PrefabProba[i];
            if (SumProb > pos)
            {
                pos = i;
                break;
            }
        }


        planetas.Add(Instantiate(Prefab[pos], InitialPosition, Prefab[pos].transform.rotation) as GameObject);
        ((GameObject)planetas[planetas.Count -1]).GetComponent<SplineController>().InitialPos = initialpos;
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
    protected void SpawnPlanet(int pos){


   
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


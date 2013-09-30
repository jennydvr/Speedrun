using UnityEngine;
using System.Collections;

public class ManagerAsteroids : MonoBehaviour {

    
    public ArrayList asteroides;
    public Vector3 InitialPosition;
    public GameObject[] Prefab;
    public int[] PrefabProba;

    public float MinTimeSpawn = 1.0f;
    public float MaxTimeSpawn = 3.0f;
    protected bool CanSpawn = true;

    // Use this for initialization
    void Awake(){
        asteroides = new ArrayList();

        AddAsteroid(Random.Range(29,32));
        AddAsteroid(Random.Range(26,29));
        AddAsteroid(Random.Range(23,26));
        AddAsteroid(Random.Range(20,23));
        AddAsteroid(Random.Range(17,20));
        AddAsteroid(Random.Range(15,17));
        AddAsteroid(Random.Range(13,15));
        AddAsteroid(Random.Range(11,13));
        AddAsteroid(Random.Range(9,11));
        AddAsteroid(Random.Range(7,9));
        AddAsteroid(Random.Range(5,7));
    }
    void Start()
    {
        SpawnAsteroid(0);
        SpawnAsteroid(1);
        SpawnAsteroid(2);
        SpawnAsteroid(3);
        SpawnAsteroid(4);
        SpawnAsteroid(5);
        SpawnAsteroid(6);
        SpawnAsteroid(7);
        SpawnAsteroid(8);
        SpawnAsteroid(9);
        SpawnAsteroid(10);

        AddAsteroid();
        Invoke("ReadySpawn", Random.Range(MinTimeSpawn, MaxTimeSpawn));

    }


    protected void AddAsteroid(){
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


        asteroides.Add(Instantiate(Prefab[pos], InitialPosition, Prefab[pos].transform.rotation) as GameObject);
        //((GameObject)planetas[planetas.Count -1]).GetComponent<SplineController>().InitialPos = 10;
    }
    protected void AddAsteroid(int initialpos){
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


        asteroides.Add(Instantiate(Prefab[pos], InitialPosition, Prefab[pos].transform.rotation) as GameObject);
        ((GameObject)asteroides[asteroides.Count -1]).GetComponent<SplineController>().InitialPos = initialpos;
    }
    protected void SpawnAsteroid(){

        if (asteroides.Count == 0)
            return;

        int pos = asteroides.Count - 1;
        if (pos < 0)
            pos = 0;
        GameObject plan = ((GameObject)asteroides[pos]);
        plan.SetActive(true);
        plan.GetComponent<SplineInterpolator>().Reset();
        // ((GameObject)planetas[pos]).GetComponent<SplineController>().DisableTransforms();
        plan.GetComponent<SplineController>().FollowSpline();
    }
    protected void SpawnAsteroid(int pos){



        GameObject plan = ((GameObject)asteroides[pos]);
        plan.SetActive(true);
        plan.GetComponent<SplineInterpolator>().Reset();
        // ((GameObject)planetas[pos]).GetComponent<SplineController>().DisableTransforms();
        plan.GetComponent<SplineController>().FollowSpline();
    }

    protected void ReadySpawn(){
        CanSpawn = true;

    }


    public void RemoverAsteroid(){

        GameObject pla = (GameObject)asteroides[0];
        asteroides.RemoveAt(0);
        Destroy(pla);
    }
    public void DestroyAll(){
    
        while (asteroides.Count > 0)
        {
            RemoverAsteroid();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (CanSpawn)
        {
            SpawnAsteroid();
            AddAsteroid();
            Invoke("ReadySpawn", Random.Range(MinTimeSpawn, MaxTimeSpawn));
        }


    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ResourcesID {None, Fire, Wind, Earth, Grass};

public class ManagerResources : MonoBehaviour
{


    protected Dictionary<ResourcesID,int> Resources;
    // Use this for initialization
    void Start()
    {
        Resources = new Dictionary<ResourcesID,int>();
        Resources.Add(ResourcesID.Fire,0);
        Resources.Add(ResourcesID.Wind,0);
        Resources.Add(ResourcesID.Earth,0);
        Resources.Add(ResourcesID.Grass,0);
    }


    public bool ReachGoal(ResourcesID[] GoalResourcesID,int[] GoalResources ){
        bool win = true;
        for (int i =0; i<GoalResourcesID.Length; i++)
        {
            if (Resources[GoalResourcesID[i]] < GoalResources[i])
            {
                win = false;
                break; 
            }
        }

        return win;
    }

    public void AddResources(ResourcesID resor,int newR){
        Resources[resor] += newR;
    }
}


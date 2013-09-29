using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ResourcesID : byte {
    Wind,
    Fire,
    Earth,
    Grass,
    None
}

public class ManagerResources : MonoBehaviour
{

    public static float HarvestRate = 0.5f;
    protected static Dictionary<ResourcesID,int> Resources;

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
        for (int i =0; i != GoalResourcesID.Length; ++i)
        {
            if (Resources[GoalResourcesID[i]] < GoalResources[i])
            {
                win = false;
                break; 
            }
        }

        return win;
    }

    public static void GatherResource(float amount, ResourcesID type) {
        Resources [type] += Mathf.CeilToInt(amount);
    }

}


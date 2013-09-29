using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ResourcesID  {
    Wind,
    Fire,
    Earth,
    Grass,
    None
}

public class ManagerResources : MonoBehaviour
{
    public float MaxResources = 99;
    public static float HarvestRate = 0.5f;
    protected  Dictionary<ResourcesID,float> Resources;
    public UILabel FireCount;
    public UILabel WindCount;
    public UILabel GrassCount;
    public UILabel EarthCount;
    void Start()
    {
        Resources = new Dictionary<ResourcesID,float>();
        Resources.Add(ResourcesID.Fire,0);
        Resources.Add(ResourcesID.Wind,0);
        Resources.Add(ResourcesID.Earth,0);
        Resources.Add(ResourcesID.Grass,0);
    }

    public bool ReachGoal(ResourcesID[] GoalResourcesID,float[] GoalResources ){
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

    public  void GatherResource(float amount, ResourcesID type) {
        Resources [type] += (amount);
   

        if (Resources[type] < 0)
        {
            Resources[type] = 0;
        }
        else if (Resources[type] > MaxResources)
        {
            Resources[type] = MaxResources;
        }


        switch (type)
        {
            case ResourcesID.Fire:
                FireCount.text = "x " + Mathf.CeilToInt(Resources[type]).ToString();
                break;
            case ResourcesID.Earth:
                EarthCount.text = "x " + Mathf.CeilToInt(Resources[type]).ToString();
                break;
            case ResourcesID.Grass:
                GrassCount.text = "x " + Mathf.CeilToInt(Resources[type]).ToString();
                break;
            case ResourcesID.Wind:
                WindCount.text = "x " + Mathf.CeilToInt(Resources[type]).ToString();
                break;
        }
    }

}


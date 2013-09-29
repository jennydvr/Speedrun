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
    public static float HarvestRate = 0.3f;
    protected  Dictionary<ResourcesID, float> Resources;
    public UILabel FireCount;
    public UILabel WindCount;
    public UILabel GrassCount;
    public UILabel EarthCount;

    public float MinHR = 0.2f;
    public float MaxHR = 1.5f;

    void Start()
    {
        Resources = new Dictionary<ResourcesID,float>();
        Resources.Add(ResourcesID.Fire,0);
        Resources.Add(ResourcesID.Wind,0);
        Resources.Add(ResourcesID.Earth,0);
        Resources.Add(ResourcesID.Grass,0);
    }

    public bool HaveEnoughOf(ResourcesID resource, float amount) {
        return Resources[resource] >= amount;
    }

    public bool ReachGoal(ResourcesID[] GoalResourcesID, float[] GoalResources) {
        for (int i = 0; i != GoalResourcesID.Length; ++i)
        {
            if (Resources[GoalResourcesID[i]] < GoalResources[i])
            {
                return false;
            }
        }

        return true;
    }

    public void GatherResource(float amount, ResourcesID type) {
        if (amount < 0) return;
        Resources [type] += amount;

        if (Resources[type] > MaxResources)
        {
            Resources[type] = MaxResources;
        }


        UpdateText (type);
    }

    public void UseResource(float amount, ResourcesID type) {
        if (amount < 0) return;

        Resources [type] -= (amount);

        if (Resources[type] < 0)
        {
            Resources[type] = 0;
        }

        
        UpdateText (type);
    }

    private void UpdateText(ResourcesID type) {
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

    public void IncreaseSpeed() {
        HarvestRate = MaxHR;
        Invoke ("DecreaseSpeed", 5.0f);
    }

    public void DecreaseSpeed() {
        HarvestRate = MinHR;
    }

}


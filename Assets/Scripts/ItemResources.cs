using UnityEngine;
using System.Collections;

public class ItemResources : MonoBehaviour
{

    public ManagerResources mResources;
    public ResourcesID[] GoalResourcesID;
    public float[] GoalResources;

    public bool Unlock = false;

    protected virtual void Start()
    {
        mResources = (ManagerResources)GameObject.FindObjectOfType(typeof(ManagerResources));
    }

    protected virtual void Update()
    {
        Unlock = mResources.ReachGoal(GoalResourcesID, GoalResources);
    }

}


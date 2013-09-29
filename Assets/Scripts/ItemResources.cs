using UnityEngine;
using System.Collections;

public class ItemResources : MonoBehaviour
{
    public ManagerResources mResources;
    public ResourcesID[] GoalResourcesID;
    public int[] GoalResources;

    public bool Unlock = false;
    // Use this for initialization
    protected virtual void Start()
    {
        mResources = (ManagerResources)GameObject.FindObjectOfType(typeof(ManagerResources));
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        Unlock = mResources.ReachGoal(GoalResourcesID,GoalResources);
    }


}


﻿using UnityEngine;
using System.Collections.Generic;

public class PowerActivator {

    public static ManagerResources mResources;
    public Dictionary<ResourcesID, float> Goals;

    public PowerActivator() {
        if (mResources == null)
            mResources = (ManagerResources)GameObject.FindObjectOfType(typeof(ManagerResources));

        Goals = new Dictionary<ResourcesID, float> ();
    }

    public void AddGoal(ResourcesID type, float amount) {
        Goals.Add(type, amount);
    }
	
    public bool Unlocked() {
        foreach (ResourcesID resource in Goals.Keys) {
            if (!mResources.HaveEnoughOf(resource, Goals[resource])) {
                return false;
            }
        }

        return true;
	}

    public virtual void Use() {
        foreach (ResourcesID resource in Goals.Keys) {
            mResources.UseResource (Goals [resource], resource);
        }
    }

}

public class Bomb : PowerActivator {
    
    // Bomba: T x 00 + F x 00
    public Bomb() {
        AddGoal (ResourcesID.Earth, 35);
        AddGoal (ResourcesID.Fire, 60);
    }

    public override void Use ()
    {

        base.Use ();
    }

}

public class Speed : PowerActivator {
    
    // Speed: V x 00 + G x 00
    public Speed() {
        AddGoal (ResourcesID.Wind, 40);
        AddGoal (ResourcesID.Grass, 25);
    }

    public override void Use ()
    {
        mResources.IncreaseSpeed ();
        base.Use ();
    }

}

public class Tapon : PowerActivator {
    
    // Tapon: ??
    public Tapon() {
        AddGoal (ResourcesID.Earth, 120);
        AddGoal (ResourcesID.Fire, 90);
        AddGoal (ResourcesID.Wind, 75);
        AddGoal (ResourcesID.Grass, 60);
    }    

    public override void Use ()
    {

        base.Use ();
    }

}


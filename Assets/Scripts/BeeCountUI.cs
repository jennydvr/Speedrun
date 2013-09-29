using UnityEngine;
using System.Collections;

public class BeeCountUI : MonoBehaviour
{
    public UILabel label;
    public Planet planet;
    protected int oldCount = 0;
    // Use this for initialization
    void Start()
    {
        label = gameObject.GetComponent<UILabel>();

    }
    // Update is called once per frame
    void Update()
    {
        if (oldCount != planet.BeesCount)
        {
            label.text = planet.BeesCount.ToString();
            oldCount = planet.BeesCount;
        }
    }
}


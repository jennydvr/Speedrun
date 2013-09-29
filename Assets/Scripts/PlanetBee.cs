using UnityEngine;
using System.Collections;

public class PlanetBee : MonoBehaviour
{
    public GameObject PlanetMesh;
    public GameObject BeePanalMesh;
    public GameObject PanalSecoMesh;

    public GameObject GlowMesh;
    public GameObject GlowBeeMesh;

    protected Planet planet;
    protected bool ChangueFirst = false;
    protected bool ChangueSecond = false;
    // Use this for initialization
    void Start()
    {
        planet = gameObject.GetComponent<Planet>();

    }
    // Update is called once per frame
    void Update()
    {
        if (planet.BeesCount > 0 && !ChangueFirst)
        {
            PlanetMesh.SetActive(false);
            BeePanalMesh.SetActive(true);
            GlowMesh.SetActive(false);
            GlowBeeMesh.SetActive(true);
            ChangueFirst = true;
        }
        else if (ChangueFirst && !ChangueSecond)
        {
            if (planet.BeesCount <= 0)
            {
                BeePanalMesh.SetActive(false);
                PanalSecoMesh.SetActive(true);
                GlowBeeMesh.SetActive(false);
                ChangueSecond = true;
            }
        }
    }
}


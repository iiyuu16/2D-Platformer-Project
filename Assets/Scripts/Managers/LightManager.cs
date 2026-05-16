using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    public static LightManager Instance;
    [SerializeField] private Light2D globalLight;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        
    }

    public void SetLightIntensity(float intensity)
    {
        globalLight.intensity = intensity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

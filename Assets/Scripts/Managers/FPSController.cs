using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public static FPSController Instance { get; private set; }

    private List<float> fps = new List<float>();
    private float _timer, _firstTimer;
    private bool isFirst;


    public float GetAverage()
    {
        if (fps.Count < 10) return 0;

        return fps.Average();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    private void Update()
    {
        if (!Globals.IsInitiated || Globals.IsLowFPS) return;

        if (_timer > 0.1f)
        {
            
            _timer = 0;
            if (EasyFpsCounter.EasyFps != null)
            {
                fps.Add(EasyFpsCounter.EasyFps.FPS);
                if (fps.Count > 50)
                {
                    fps.Remove(fps[0]);
                }

                float ave = GetAverage();

                if (fps.Count > 20 && ave > 5 && ave < 45)
                {
                    Globals.IsLowFPS = true;
                    Globals.MainPlayerData.IsLowFPS = true;
                    QualitySettings.shadows = ShadowQuality.Disable;
                }                
            }           

        }
        else
        {
            _timer += Time.deltaTime;
        }
    }
}

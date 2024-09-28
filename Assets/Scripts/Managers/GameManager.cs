using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Globals.IsMobile = Globals.IsMobileChecker();

        if (Globals.IsMobile)
        {
            QualitySettings.antiAliasing = 2;

            if (Globals.IsLowFPS)
            {
                QualitySettings.shadows = ShadowQuality.Disable;
            }
            else
            {
                QualitySettings.shadows = ShadowQuality.HardOnly;
                QualitySettings.shadowResolution = ShadowResolution.Medium;
            }
        }
        else
        {
            QualitySettings.antiAliasing = 4;
            QualitySettings.shadows = ShadowQuality.All;
            QualitySettings.shadowResolution = ShadowResolution.Medium;
        }

        if (YandexGame.Instance != null) YandexGame.StickyAdActivity(true);
    }
}

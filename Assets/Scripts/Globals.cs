using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public static PlayerData MainPlayerData;
    public static bool IsSoundOn;
    public static bool IsMusicOn;
    public static bool IsInitiated;
    public static bool IsMainMenuTutorial;
    public static string CurrentLanguage;
    public static Translation Language;

    public static int CurrentLevel;

    public const int MAIN_PLAYER_TEAM = 1;
    public const int ENEMIES_TEAM = -1;


    public static DateTime TimeWhenStartedPlaying;
    public static DateTime TimeWhenLastInterstitialWas;
    public static DateTime TimeWhenLastRewardedWas;
    public const float REWARDED_COOLDOWN = 120;
    public const float INTERSTITIAL_COOLDOWN = 70;

    public static bool IsMobile;
    public static bool IsOptions;
    public static bool IsLowFPS;

    public const float SCREEN_SAVER_AWAIT = 0.75f;

    public const float PLAYER_UPDATE_JOYSTICK_COOLDOWN = 0.1f;
    public const float PLAYER_BASE_MAXSPEED = 6f;
    public const float PLAYER_DEATH_WAIT_ANIMATION = 1.5f;
    public const float COOLDOWN_CHECK_DEAD_PLAYERS = 0.3f;
    public const float COOLDOWN_CHECK_DEAD_NPC = 0.3f;
    public const float COOLDOWN_UPDATE_ATTACK_NPC = 0.1f;
    public const float COOLDOWN_UPDATE_ATTACK_PLAYER = 0.1f;


    public static bool IsMobileChecker()
    {
        //return true;

        if (Application.isMobilePlatform)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
                
}

public interface IPlayer
{
    int TeamID { get; }
    Character Character { get; }
    void ReceiveHit(IPlayer enemy);
    Transform Transform { get; }
}

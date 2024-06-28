using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {

        if (Globals.MainPlayerData == null)
        {
            Globals.MainPlayerData = new PlayerData();            
            Globals.IsMobile = Globals.IsMobileChecker();
            Globals.IsSoundOn = true;
            Globals.IsMusicOn = true;
            Globals.IsInitiated = true;
            Globals.Language = Localization.GetInstanse("ru").GetCurrentTranslation();
            Globals.CurrentLevel = Globals.MainPlayerData.Rating;
        }


        builder.RegisterComponentInHierarchy<Joystick>();
        builder.RegisterComponentInHierarchy<Musics>();
        builder.RegisterComponentInHierarchy<Sounds>();
        builder.RegisterComponentInHierarchy<Joystick>();
        builder.RegisterComponentInHierarchy<Camera>();

        builder.RegisterComponentInHierarchy<MainPlayerControl>();
        builder.RegisterComponentInHierarchy<InputControl>();                
        builder.RegisterComponentInHierarchy<GameManager>();
        builder.RegisterComponentInHierarchy<CameraControl>();

    }
}

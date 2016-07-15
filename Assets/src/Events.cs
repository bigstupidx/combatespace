using UnityEngine;
using System.Collections;

public static class Events
{
    public static System.Action OnGameSettings = delegate { };
    public static System.Action<PlayerData> OnUpdatePlayerData = delegate { };
    public static System.Action<bool> OnLoadingShow = delegate { };
    public static System.Action OnRegisterPopup = delegate { };
    public static System.Action<string, string> OnGenericPopup = delegate { };
    public static System.Action<bool> OnShowBackButton = delegate { };
    public static System.Action OnBackButtonPressed = delegate { };

    public static System.Action<Fight> OnSaveNewPelea = delegate { };
    public static System.Action<string, Peleas> OnSavePelea = delegate { };
    public static System.Action<Stats> OnSaveStats = delegate { };
    public static System.Action<bool> OnFightEnd = delegate { };   
    

    public static System.Action OnRoundComplete = delegate { };
    public static System.Action OnAllRoundsComplete = delegate { };

    public static System.Action<bool> OnAvatarFall = delegate { };
    public static System.Action<int> OnTutorialReady = delegate { };

    public static System.Action<bool> OnKO = delegate { };
    public static System.Action<bool> OnAvatarStandUp = delegate { };

    public static System.Action<CharacterActions.actions> OnCharacterChangeAction = delegate { };
    public static System.Action<CharacterActions.actions> OnAICharacterDefense = delegate { };
    public static System.Action<CharacterActions.actions> OnAICharacterAttack = delegate { };
    public static System.Action<CharacterActions.actions> OnCheckHeroHitted = delegate { };

    public static System.Action<HeroActions.actions> OnHeroAction = delegate { };
    public static System.Action<HeroActions.actions> OnHeroSound = delegate { };
    public static System.Action<HeroActions.actions> OnCheckCharacterHitted = delegate { };    
    public static System.Action<HeroActions.actions> OnCharacterBlockPunch = delegate { };

	public static System.Action<CharacterActions.actions> OnHeroBlockPunch = delegate { };
	public static System.Action<float> OnHeroAguanteStatus = delegate { };
    

    //accion y combo
    public static System.Action<HeroActions.actions> OnComputeCharacterPunched = delegate { };
    public static System.Action<CharacterActions.actions> OnComputeHeroPunched = delegate { };


    public static System.Action<float> OnChangeStatusHero = delegate { };
    public static System.Action<float> OnChangeStatusCharacter = delegate { };

    //true ganaste, false perdiste:
   // public static System.Action<bool> OnKO = delegate { };
    public static System.Action OnRoundStart = delegate { };
    
}
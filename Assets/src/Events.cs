using UnityEngine;
using System.Collections;

public static class Events
{

    public static System.Action OnRoundComplete = delegate { };
    public static System.Action OnAllRoundsComplete = delegate { };
    public static System.Action<int> OnTutorialReady = delegate { };
    public static System.Action<CharacterActions.actions> OnCharacterChangeAction = delegate { };
    public static System.Action<CharacterActions.actions> OnAICharacterDefense = delegate { };
    public static System.Action<CharacterActions.actions> OnAICharacterAttack = delegate { };
    public static System.Action<CharacterActions.actions> OnCheckHeroHitted = delegate { };

    public static System.Action<HeroActions.actions> OnHeroAction = delegate { };
    public static System.Action<HeroActions.actions> OnHeroSound = delegate { };
    public static System.Action<HeroActions.actions> OnCheckCharacterHitted = delegate { };    
    public static System.Action<HeroActions.actions> OnCharacterBlockPunch = delegate { };
	public static System.Action<CharacterActions.actions> OnHeroBlockPunch = delegate { };

    public static System.Action<CharacterActions.actions> OnComputeHeroPunched = delegate { };
    public static System.Action<HeroActions.actions> OnComputeCharacterPunched = delegate { };

    public static System.Action<float> OnChangeStatusHero = delegate { };
    public static System.Action<float> OnChangeStatusCharacter = delegate { };

    //true ganaste, false perdiste:
    public static System.Action<bool> OnKO = delegate { };
    public static System.Action OnRoundStart = delegate { };
    
}
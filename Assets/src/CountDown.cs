using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDown : MonoBehaviour {

    public Text field;
    public GameObject panel;
    private int sec;
    private bool isHero;
    public int probabilityToStandAgain;
    private FightStatus fightStatus;
    public ProgressBar progressBar;
    public float hero_progress;
    private bool ready;
    public Animation anim;
    public Image GlowImage;
    public HeroActions heroActions;

	void Start () {
        fightStatus = Game.Instance.fightStatus;
        SetOff();
        Events.OnAvatarFall += OnAvatarFall;
        GlowImage.enabled = false;
	}
    void OnDestroy()
    {
        Events.OnAvatarFall -= OnAvatarFall;
    }
    void OnAvatarFall(bool isHero)
    {
        raising = false;
        ready = false;
        this.isHero = isHero;
        progressBar.gameObject.SetActive(false);
        Invoke("OnAvatarFallDelay", 0.1f);
    }
    void OnAvatarFallDelay()
    {
        hero_progress = 0;
        if (isHero)
        {
            //if (fightStatus.caidas_hero >= 3)
            //{
            //    Events.OnKO(true);
            //    return;
            //}
            
            progressBar.gameObject.SetActive(true);
            LoopHero();
            
        } else
        {            
            probabilityToStandAgain = ((100 - (int)(fightStatus.cansancio_hero * 50)) / 2) - (fightStatus.caidas_character*10);
            //if (fightStatus.caidas_character >= 3)
            //{
            //    Events.OnKO(false);
            //    SetOff();
            //    return;
            //}
        }
        sec = 0;
        panel.SetActive(true);
        
        if (isHero)
            anim.Play("countDown");
        else
            anim.Play("countDownOff");

        Loop();
    }
    void Loop()
    {
        if (ready) return;
        sec++;
        if (sec == 10)
        {
            Events.OnKO(isHero);
            SetOff();
        }
        else
        {
            if (!isHero && sec>2 && Random.Range(0, 100) < probabilityToStandAgain)
            {
                Events.OnAvatarStandUp(isHero);
                SetOff();
                return;
            }
            field.text = sec.ToString();
            Invoke("Loop", 0.75f);
        }
    }
    void SetOff()
    {
        panel.SetActive(false);
    }
    bool raising;
    void LoopHero()
    {
        hero_progress -= Time.deltaTime;
        if (hero_progress <= 0)
        {
            hero_progress = 0;
            if (raising)
            {
                raising = false;
                heroActions.TryToRaiseButFails();
            }
        }
        else if (hero_progress > 0.1f && !raising)
        {
            raising = true;
            heroActions.TryToRaise();
        }
        else if (hero_progress >= 1)
        {
            ready = true;
            Events.OnAvatarStandUp(true);
            SetOff();
            hero_progress = 0;
            return;
        }
        progressBar.SetProgress(hero_progress);
        Invoke("LoopHero", 0.1f);
    }
    public void OnClik()
    {
        float caidas_suma = (float)Game.Instance.fightStatus.caidas_hero/25;
        float cansancio_suma = 1-(fightStatus.cansancio_hero);
        float total_para_sumar = (0.2f * cansancio_suma) - caidas_suma;
        if (total_para_sumar < 0.05f) total_para_sumar = 0.05f;
      //  print("caidas_suma " + caidas_suma + " cansancio_suma: " + cansancio_suma + " total_para_sumar: " + total_para_sumar);

        hero_progress += total_para_sumar;
        GlowImage.enabled = true;
        Invoke("SetGlowOff", 0.1f);
    }
    void SetGlowOff()
    {
        GlowImage.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CanvasManager : MonoBehaviour
{
	[SerializeField]private CanvasGroup VictoryScreen;
	[SerializeField]private CanvasGroup LossScreen;
	[SerializeField]private CanvasGroup BlackScreen;

	[SerializeField]private float FadeDuration;


	void Start() => FadeScreen(BlackScreen, 0, false);


    void Update()
    {
        if(GameManager.CurrentGameState == GameState.Victory)
        {
        	VictoryScreen.gameObject.SetActive(true);
        	FadeScreen(VictoryScreen, 1, false);
        }

        if(GameManager.CurrentGameState == GameState.Loss)
        {
        	LossScreen.gameObject.SetActive(true);
        	FadeScreen(LossScreen, 1, false);
        }
    }

    public void PlayRestartAnimation() => FadeScreen(BlackScreen, 1, true);

    void FadeScreen(CanvasGroup canvas, float targetAlpha, bool shouldRestartLevel)
    {
    	if(shouldRestartLevel)
        	canvas.DOFade(targetAlpha, FadeDuration).OnComplete(() => GameManager.RestartLevel());

        else
        	canvas.DOFade(targetAlpha, FadeDuration);
    }
}

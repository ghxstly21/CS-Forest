using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameSequence : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject creditScreen;
    public GameObject endText;

    public CanvasGroup winGroup;
    public CanvasGroup creditsGroup;
    public CanvasGroup endGroup;

    public TextMeshProUGUI creditText;
    public float scrollSpeed = 20f;
    public float winDelay = 1f;
    public float creditsDelay = 3f;
    public float endDelay = 10f;

    private Vector3 startCreditsPos;

    void Start()
    {
        winGroup.alpha = 0;
        creditsGroup.alpha = 0;
        endGroup.alpha = 0;
        creditScreen.SetActive(false);
        endText.SetActive(false);
        startCreditsPos = creditText.rectTransform.localPosition;
    }

    public void TriggerWin()
    {
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        winScreen.SetActive(true);
        yield return FadeIn(winGroup);

        yield return new WaitForSeconds(creditsDelay);
        creditScreen.SetActive(true);
        yield return FadeIn(creditsGroup);

        yield return StartCoroutine(ScrollCredits());

        yield return new WaitForSeconds(1f);
        endText.SetActive(true);
        yield return FadeIn(endGroup);
    }

    IEnumerator FadeIn(CanvasGroup group)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            group.alpha = t;
            yield return null;
        }
        group.alpha = 1;
    }

    IEnumerator ScrollCredits()
    {
        float duration = endDelay;
        float elapsed = 0f;

        creditText.rectTransform.localPosition = startCreditsPos;

        Vector3 endPos = startCreditsPos + new Vector3(0, 600, 0);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            creditText.rectTransform.localPosition = Vector3.Lerp(startCreditsPos, endPos, elapsed / duration);
            yield return null;
        }
    }
}

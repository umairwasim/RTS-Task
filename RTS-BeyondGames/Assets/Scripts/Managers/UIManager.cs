using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Gameplay UI")]
    public TMP_Text cooldownText;
    public Image cooldownImage;

    private void Awake() => Instance = this;

    private void Start() => HideCoolDownText();

    public void ShowCoolDownText() => cooldownText.DOFade(1f, 0.5f);

    public void HideCoolDownText() => cooldownText.DOFade(0f, 0.5f);

    public void UpdateCooldownText(float cooldownDuration)
    {
        cooldownText.text = "Cooldown: " + cooldownDuration.ToString("0");
    }

}

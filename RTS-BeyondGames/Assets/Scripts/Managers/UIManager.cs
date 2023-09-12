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

    public void ShowCoolDownText() => cooldownText.alpha = 1;

    public void HideCoolDownText() => cooldownText.alpha = 0;

}

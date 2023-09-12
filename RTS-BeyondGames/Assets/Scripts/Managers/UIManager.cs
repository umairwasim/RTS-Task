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

    private readonly float scaleDuration = 1f;

    private void Awake() => Instance = this;

    private void Start()
    {
        cooldownImage.DOFade(0, 5f).OnComplete(() =>
        {
            cooldownImage.gameObject.SetActive(false);
        });
    }

}

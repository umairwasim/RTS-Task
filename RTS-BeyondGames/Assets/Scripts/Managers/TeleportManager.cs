using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager Instance;

    [SerializeField] private GameObject teleporterPrefab;

    private NavMeshAgent agent;

    private bool isCoolingDown;
    private float cooldownDuration;

    private const float COOLDOWN_TIMER = 10f;
    private const float SCALE_MULTIPLIER = 0.25f;

    private readonly List<Teleporter> teleportersList = new();

    public bool GetCanSpawnTeleporter() => teleportersList.Count < 2;

    private void Awake() => Instance = this;

    public void AddTeleporter(Teleporter teleporter) => teleportersList.Add(teleporter);

    //set the selected agent
    public void SetAgent(NavMeshAgent newAgent) => agent = newAgent;

    public void CreateTeleporter(Vector3 position)
    {
        // Instantiate the teleporter.
        Transform teleporterTransform = Instantiate(teleporterPrefab.transform, position, Quaternion.identity, transform);
        teleporterTransform.DOPunchScale(Vector3.one * SCALE_MULTIPLIER, 0.25f);

        if (teleporterTransform.TryGetComponent(out Teleporter newTeleporter))
            teleportersList.Add(newTeleporter);

        // Check if we have two teleporters, and if so, set their references.
        if (teleportersList.Count == 2)
        {
            SetTeleporterReferences();
            //Start the Cool down timer
            StartCoroutine(StartCooldowRoutine());
            UIManager.Instance.ShowCoolDownText();
        }
    }

    private void SetTeleporterReferences()
    {
        Teleporter teleporter1 = teleportersList[0].GetComponent<Teleporter>();
        Teleporter teleporter2 = teleportersList[1].GetComponent<Teleporter>();

        if (teleporter1 != null && teleporter2 != null)
        {
            teleporter1.SetOtherTeleporter(teleportersList[1]);
            teleporter2.SetOtherTeleporter(teleportersList[0]);
        }
    }

    private IEnumerator StartCooldowRoutine()
    {
        isCoolingDown = true;
        cooldownDuration = COOLDOWN_TIMER;

        while (cooldownDuration > 0)
        {
            // Update UI Manager to display cooldown timer.
            UIManager.Instance.UpdateCooldownText(cooldownDuration);
            yield return new WaitForSeconds(1f);
            cooldownDuration--;
        }

        // Cooldown is over.
        UIManager.Instance.UpdateCooldownText(0);
        isCoolingDown = false;

        //Fade out Cooldown text
        UIManager.Instance.HideCoolDownText();

        // Clear the list of teleporters for next spawning.
        ClearTeleportersList();
    }


    private void ClearTeleportersList()
    {
        foreach (var teleporter in teleportersList)
        {
            teleporter.transform
                .DOScale(Vector3.zero, SCALE_MULTIPLIER * 2)
                .SetEase(Ease.InBack)
                .OnComplete(() => Destroy(teleporter.gameObject));
        }

        teleportersList.Clear();
    }

}

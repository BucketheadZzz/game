using System.Collections;
using Assets.Scripts.Player;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup exitBackgroundImageCanvasGroup;

    [SerializeField]
    private CanvasGroup dieBackgroundImageCanvasGroup;

    [SerializeField]
    private bool isDeadZone;

    private bool isPlayerInZone;

    private void Start()
    {
        PlayerBroadcaster.Instance.PlayerDied += OnPlayerDied;
    }

    private void OnPlayerDied(object info)
    {
        EndLevel(dieBackgroundImageCanvasGroup);
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayerInZone = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInZone)
        {
            var canvas = isDeadZone ? dieBackgroundImageCanvasGroup : exitBackgroundImageCanvasGroup;
            EndLevel(canvas);
        }
    }

    private void EndLevel(CanvasGroup imageCanvasGroup)
    {
        imageCanvasGroup.alpha = 1;
        StartCoroutine(EndGameCoroutine());
    }


    private IEnumerator EndGameCoroutine()
    {
        yield return new WaitForSeconds(3);

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
         Application.Quit();
        #endif
    }
}

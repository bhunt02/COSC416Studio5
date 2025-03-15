using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI current;
  [SerializeField] private TextMeshProUGUI toUpdate;
  [SerializeField] private Transform scoreTextContainer;
  [SerializeField] private float duration;

  private float containerInitPosition;
  private float moveAmount;

  void Start()
  {
    Canvas.ForceUpdateCanvases();
    current.SetText("0");
    toUpdate.SetText("0");
    containerInitPosition = scoreTextContainer.localPosition.y;
    moveAmount = current.rectTransform.rect.height;
  }

  public void UpdateScore(int score)
  {
    toUpdate.SetText($"{score}");
    scoreTextContainer.DOLocalMoveY(containerInitPosition + moveAmount, duration);
    StartCoroutine(ResetScoreCounter(score));
  }

  private IEnumerator ResetScoreCounter(int score)
  {
    yield return new WaitForSeconds(duration);
    current.SetText($"{score}");
    Vector3 localPosition = scoreTextContainer.localPosition;
    scoreTextContainer.localPosition = new Vector3(localPosition.x, containerInitPosition, localPosition.z);
  }
}

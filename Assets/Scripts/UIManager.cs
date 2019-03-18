using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private int maxScore;
    private int score;

    private void Start() => Messenger<int>.AddListener(GameEvent.SCORE_EARNED, ChangeScore);
    private void OnDestroy() => Messenger<int>.RemoveListener(GameEvent.SCORE_EARNED, ChangeScore);

    private void ChangeScore(int scoreDelta)
    {
        score += scoreDelta;
        scoreLabel.text = $"Score: {score} / {maxScore}";
    }
}
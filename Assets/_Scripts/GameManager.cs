using System.Collections;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{ 
    private static int maxLives = 3;
    private static int currentLives;
    public static int score;
    
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeStrength;

    [SerializeField] private LifeCanvasManager lifeCanvasManager;
    [SerializeField] private ScoreCounter scoreCounter;
    
    private int currentBrickCount;
    private int totalBrickCount;

    private void Start()
    {
        lifeCanvasManager.PopulateLifeIcons();
        scoreCounter.UpdateScore(score);
    }
    
    private void OnEnable()
    {
        InputHandler.Instance.OnFire.AddListener(FireBall);
        ball.ResetBall();
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnFire.RemoveListener(FireBall);
    }

    private void FireBall()
    {
        if (currentLives <= 0) return;
        ball.FireBall();
    }

    public void OnBrickDestroyed(Vector3 position)
    {
        // fire audio here
        // implement particle effect here
        CameraShake.Shake(shakeDuration, shakeStrength);
        currentBrickCount--;
        IncreaseScore();
        Debug.Log($"Destroyed Brick at {position}, {currentBrickCount}/{totalBrickCount} remaining");
        if (currentBrickCount == 0) SceneHandler.Instance.LoadNextScene();
    }

    public void KillBall()
    {
        if (currentLives <= 0) return;
        
        currentLives--;
            
        StartCoroutine(HandleLivesChanged());
            
        ball.ResetBall();
    }

    private IEnumerator HandleLivesChanged()
    {
        // update lives on HUD here
        yield return lifeCanvasManager.UpdateLifeIcons(currentLives);
        // game over UI if maxLives < 0, then exit to main menu after delay
        if (currentLives <= 0)
        {
            SceneHandler.Instance.LoadGameOverScene();
        }
    }

    public static void ResetLives()
    {
        currentLives = maxLives;
    }

    public static int GetMaxLives()
    {
        return maxLives;
    }

    public static int GetCurrentLives()
    {
        return currentLives;
    }
    
    private void IncreaseScore()
    {
        score++;
        scoreCounter.UpdateScore(score);
    }
    
    public static void ResetScore()
    {
        score = 0;
    }
}

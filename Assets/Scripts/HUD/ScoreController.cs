using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<CycleController>();
        GameCountdownReset = GameCountdownTime;
        PlayerOriginalScore = PlayerController.PlayerScore;
    }

    private void Update()
    {
        if (GameController.IsStartGame)
        {
            GameCountdownTime -= Time.deltaTime;
            GameCountDown.text = GameCountdownTime.ToString("0.00");

            if (GameCountdownTime <= GameCountdownReset / 2)
            {
                GameCountDown.color = RedColor;
            }
        }
    }

    public void SetPlayerScore()
    {
        PlayerScore.text = PlayerController.PlayerScore.ToString();
        if(PlayerController.PlayerScore >= PlayerController.PlayerMaxScore)
        {
            PlayerMaxScore.text = PlayerController.PlayerScore.ToString();
        }
    }

    public void AddTimeGameCountdown()
    {
        GameCountdownTime += TimeAdd;
    }

    public void ResetValues()
    {
        PlayerScore.text = PlayerOriginalScore.ToString();
        GameCountdownTime = GameCountdownReset;

        GameCountDown.text = GameCountdownTime.ToString("0.00");
        GameCountDown.color = BlueColor;
    }

    //IEnumerator SetAnimationStopDelay()
    //{
    //    yield return new WaitForSeconds(.2f);
    //    pulseComboAnim.gameObject.GetComponent<Animator>().SetBool("Play", false);
    //}



    private int PlayerOriginalScore;

    private GameController GameController;

    public float GameCountdownTime { get; private set; } = 20f;
    private float GameCountdownReset;

    [SerializeField] private Text PlayerScore = null;
    [SerializeField] private Text PlayerMaxScore = null;
    [SerializeField] private Text GameCountDown = null;

    [SerializeField] private Color BlueColor = new Color();
    [SerializeField] private Color RedColor = new Color();

    private CycleController PlayerController;

    private readonly float TimeAdd = 6f;
}
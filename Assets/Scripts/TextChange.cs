using UnityEngine;
using UnityEngine.UI;
public class TextChange : MonoBehaviour {
    public void SetTextChange () {
        timerCount--;
        if (timerCount < 1) {
            gameObject.GetComponent<Text> ().text = "GO!";
            GameController.IsStartGame = true;
            StopAnimation ();
        } else {
            gameObject.GetComponent<Text> ().text = timerCount.ToString ();
        }
    }
    public void StopAnimation () {
        GetComponent<Animator> ().SetBool ("Play", false);
    }
    public void StartAnimation () {
        GetComponent<Animator> ().SetBool ("Play", true);
    }
    public void ResetText () {
        gameObject.GetComponent<Text> ().text = timerCount.ToString ();
    }
    [SerializeField] private GameController GameController = null;
    static public int timerCount = 3;

}
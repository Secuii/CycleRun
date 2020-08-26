using UnityEngine;

public class CubeFloorSc : MonoBehaviour
{
    public void AnimationCube(string _nameAnim, bool _active)
    {
        transform.GetComponent<Animator>().SetBool(_nameAnim, _active);
    }

    private void StopAnimationEvent()
    {
        transform.GetComponent<Animator>().SetBool("startAnimation", false);
        transform.GetComponent<Animator>().SetBool("isPlayerDead", false);

    }

    public void Sound1()
    {
        Soun1.Play();
    }
    public void Sound2()
    {

        Soun2.Play();
    }

    [SerializeField] private AudioSource Soun1 = null;
    [SerializeField] private AudioSource Soun2 = null;

}

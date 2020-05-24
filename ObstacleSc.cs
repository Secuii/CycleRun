using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSc : MonoBehaviour
{
    public float _destroyPosition;

    GameManager _gameControllerSC;

    [HideInInspector]
    public float _timeCounter;
    [HideInInspector]
    public AudioSource sound;

    void Start()
    {
        _gameControllerSC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }


    void Update()
    {
        //if the game start
        if (_gameControllerSC.gameStart)
        {
            // just if the time counter is less than 12 units it will increase the units
            if(_timeCounter <= 12f)
            {
                _timeCounter = _gameControllerSC._kmValueInt / 30f;
            }
            //the object will be moved to the left
            this.transform.Translate(Vector3.left * Time.deltaTime * (10f + _timeCounter));
            // when the object get to the destroy position will be destroyed
            if (this.transform.position.x <= _destroyPosition)
            {
                Destroy(this.gameObject);
            }
        }
    }
    // sound to play when the player collision with it
    public void PlaySoundOfItem()
    {

        sound.Play();

    }
}

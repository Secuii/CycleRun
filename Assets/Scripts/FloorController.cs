using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FloorController : MonoBehaviour
{
    public void CallStart()
    {
        //ChangeLevelDifficult(arrayLength);
        FindFloorObjects();
        if (cubesSelected[0] != null)
        {
            for (int i = 0; i < arrayLength; i++)
            {
                cubesSelected[i].transform.GetChild(0).GetComponent<CubeFloorSc>().AnimationCube("isPlayerDead", false);
            }
        }
    }

    public void ChangeLevelDifficult(int _arrayLe)
    {
        arrayLength += _arrayLe;
    }

    public void FindFloorObjects()
    {
        savedCubes = new GameObject[arrayLength];
        cubesSelected = new GameObject[arrayLength];

        if (cubesSelected[0] != null)
        {
            for (int i = 0; i < arrayLength; i++)
            {
                cubesSelected[i].transform.GetChild(0).GetComponent<CubeFloorSc>().AnimationCube("startAnimation", false);
            }
        }

        for (int i = 0; i < arrayLength; i++)
        {
            cubesSelected[i] = null;
            savedCubes[i] = null;
        }

        for (int i = 0; i < arrayLength; i++)
        {
            int floorChild = Random.Range(0, transform.childCount);
            bool isRepeated = false;
            bool ceroDoned = false;

            for (int j = 0; j < arrayLength; j++)
            {
                if (transform.GetChild(floorChild).gameObject == savedCubes[j])
                {
                    if (ceroDoned)
                    {
                        break;
                    }
                    else
                    {
                        if (floorChild == 0)
                        {
                            ceroDoned = true;
                            continue;
                        }
                    }
                    isRepeated = true;
                    break;
                }
            }

            if (isRepeated)
            {
                i--;
            }
            else
            {
                cubesSelected[i] = transform.GetChild(floorChild).gameObject;
                savedCubes[i] = cubesSelected[i];
            }
            cubesSelected[i].transform.GetChild(0).GetComponent<CubeFloorSc>().AnimationCube("startAnimation", true);
        }
    }

    public void StopAnim()
    {
        try
        {
            if (cubesSelected[0] != null)
            {
                for (int i = 0; i < arrayLength; i++)
                {
                    cubesSelected[i].transform.GetChild(0).GetComponent<CubeFloorSc>().AnimationCube("isPlayerDead", true);
                    cubesSelected[i].transform.GetChild(0).GetComponent<CubeFloorSc>().AnimationCube("startAnimation", false);
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex);

        }
    }

    private GameObject[] savedCubes;
    private GameObject[] cubesSelected;
    private int arrayLength = 5;
}

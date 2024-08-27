using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyLevel : MonoBehaviour
{
    private void Start() 
    {
        SetDifficultyLevel(1);
    }

    public void SetDifficultyLevel(int index)
    {
        GameManager.Instance.SetDifficultyLevel(index);
    }

}

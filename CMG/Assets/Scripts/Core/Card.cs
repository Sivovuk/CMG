using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/New Card", order = 1)]
public class Card : ScriptableObject
{
    public int CardID;

    public GameObject CardPrefab;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ListOfAI", menuName = "AI/ListOfAI")]
public class ListOfAI : ScriptableObject
{
    public AIStateManager[] listOfAI;
}

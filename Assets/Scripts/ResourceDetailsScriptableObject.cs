using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "World Resource Object Details", menuName = "ScriptableObjects/World Resource Data Object", order = 1)]
public class ResourceDetailsScriptableObject : ScriptableObject
{
    public string Name = "RESOURCE DATA NAME NOT SET";
    public string Description = "RESOURCE DATA DESCRIPTION NOT SET";
    public bool IsInfinite = true;
    public int Amount;
}

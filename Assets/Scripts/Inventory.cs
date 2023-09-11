using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> collectedObjects = new List<string>();

    public void AddObject(string objectName)
    {
        collectedObjects.Add(objectName);
    }
}
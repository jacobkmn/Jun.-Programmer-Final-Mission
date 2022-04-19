using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Door Data", menuName = "Door Data")]
public class DoorData : ScriptableObject
{
    public enum DoorPosition { left, middle, right };
    public DoorPosition doorPosition;
}

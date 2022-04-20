using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public static PlayerPosition instance;

    Vector3 startPosition;

    bool isMoving;
    public bool IsMoving
    {
        get { return isMoving; }
        set { isMoving = value; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        startPosition = Camera.main.transform.position;
    }

    public bool AtStartPosition()
    {
        if (Camera.main.transform.position == startPosition)
        {
            return true;
        }
        else return false; 
    }
}

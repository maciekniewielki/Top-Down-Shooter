using UnityEngine;
public class MoveInformation
{
    public RectTransform whatToMove;
    public Vector2 destination;
    public float timeToReach;

    public MoveInformation(RectTransform whatToMove, Vector2 destination, float timeToReach)
    {
        this.whatToMove = whatToMove;
        this.destination = destination;
        this.timeToReach = timeToReach;
    }
}

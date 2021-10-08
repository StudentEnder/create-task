using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpiralPiece : MonoBehaviour
{
    public Quaternion rotation; 
    public Transform topPoint;
    public Transform bottomPoint;
    [SerializeField]
    private int depthCountdown;

    // Start is called before the first frame update
    void Start()
    {


        Debug.Log("created first component of spiral");
    }

    public void SetDepthCountdown(int countdown)
    {
        depthCountdown = countdown;
    }

    public void Propogate()
    {
        Vector3 top = topPoint.position;
        Vector3 bottom = bottomPoint.position;

        int newCountdown = depthCountdown--;
        if (newCountdown > 0)
        {
            BasicSpiralPiece piece = Instantiate(gameObject, top, rotation, transform).GetComponent<BasicSpiralPiece>();
            piece.SetDepthCountdown(newCountdown);
        }
    }
}

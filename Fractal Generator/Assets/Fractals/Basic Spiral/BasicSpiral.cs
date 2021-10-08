using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpiral : MonoBehaviour
{

    public int maxDepth = 1;
    public GameObject fractalObject;


    // Start is called before the first frame update
    void Start()
    {
        GameObject fractal = Instantiate(fractalObject, transform);
        BasicSpiralPiece piece = fractal.GetComponent<BasicSpiralPiece>();
        piece.SetDepthCountdown(maxDepth);

    }
}

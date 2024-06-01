using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshfont : MonoBehaviour
{
    public string layerName;
    public int order;

    private MeshRenderer rend;
    void Awake()
    {
        rend = GetComponent<MeshRenderer>();
        rend.sortingLayerName = layerName;
        rend.sortingOrder = order;
    }
}

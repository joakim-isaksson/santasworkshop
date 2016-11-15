using UnityEngine;
using System.Collections;

public class ColoredSide : MonoBehaviour
{
    [HideInInspector]
    public bool isHighlighted;

    public SphereColor SideColor;

    private Material material;
    private Color initialColor;
    private Color highlightColor;

    void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        highlightColor = material.color;
        initialColor = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 0.40f);
    }
    
    void Update()
    {
        material.color = isHighlighted ? highlightColor : initialColor;
    }
}

using UnityEngine;
using System.Collections;

public class DropZoneRenderHelper : MonoBehaviour
{
	void Start()
	{
		Material material = GetComponent<MeshRenderer>().material;
		material.color = new Color(1f, 0f, 0f, 0.2f);
	}
}

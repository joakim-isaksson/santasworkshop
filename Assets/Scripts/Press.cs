using UnityEngine;
using System.Collections;
using System;

public class Press : MonoBehaviour 
{
    public float CooldownTime;
    public float SetLidDelay;

    Animator anim;
    DropZone dropZone;

    bool onCooldown;

	void Awake() 
	{
        anim = GetComponent<Animator>();
        dropZone = GetComponentInChildren<DropZone>();
    }
    
    void Update() 
    {
        if (onCooldown) return;

        if (dropZone.ContainsCube)
        {
            PresentCube cube = dropZone.ContainedCube;
            if (cube.Stationary && !cube.HasLid)
            {
				GameObject.Find("Tutorial").GetComponent<Tutorial>().UpdateProgress("Press");
                anim.SetTrigger("Fire");
				transform.Find("Audio").GetComponent<AudioSource>().Play();
                StartCoroutine(SetLid());
                StartCoroutine(Cooldown());
            }
        }
    }

    IEnumerator SetLid()
    {
        yield return new WaitForSeconds(SetLidDelay);
        if (dropZone.ContainsCube)
        {
            PresentCube present = dropZone.ContainedCube;
            if (present.Stationary && !present.HasLid)
            {
                present.HasLid = true;
            }
        }
    }

    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(CooldownTime);
        onCooldown = false;
    }
}

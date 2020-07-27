using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Animator))]
public abstract class SM_BaseAnimatorController : MonoBehaviour
{
	public bool UseUpdateFrequency = false;
	[ShowIf("UseUpdateFrequency")] 
	public float UpdateFrequency = 0.1f;
	float lastTimeUpdate;

	[ShowInInspector] [ReadOnly]
	protected Animator Animator;

	protected virtual void Awake()
	{
		Animator = GetComponent<Animator>();
	}
	protected virtual void Update()
	{
		if (UseUpdateFrequency)
			CheckFrequency();
	}

	protected abstract void CheckTransitions();
	protected abstract void UpdateTransitions();

	void CheckFrequency()
	{
		if (Time.time >= lastTimeUpdate + UpdateFrequency)
		{
			CheckTransitions();
			UpdateTransitions();
			lastTimeUpdate = Time.time;
		}
	}
}
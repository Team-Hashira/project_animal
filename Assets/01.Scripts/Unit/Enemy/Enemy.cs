using UnityEngine;
using Crogen.CrogenPooling;

public class Enemy : Unit, IPoolingObject
{
	public string OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }

	public void OnPop()
	{
	}

	public void OnPush()
	{

	}
}

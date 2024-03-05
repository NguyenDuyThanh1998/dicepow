using Lean.Pool;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
	public float setDespawnTime;

	private float despawnTime;

	private void OnEnable()
	{
		despawnTime = setDespawnTime;
	}

	private void Update()
	{
		despawnTime -= Time.deltaTime;
		if (despawnTime <= 0f)
		{
			LeanPool.Despawn(base.gameObject);
		}
	}
}

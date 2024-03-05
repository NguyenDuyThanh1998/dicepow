using DG.Tweening;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraingBullet : Bullet
{
	[SerializeField, ReadOnly] protected Vector2 direction;

	[SerializeField] protected Rigidbody2D rb;
	[SerializeField] protected Transform rotateObject;


#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, (Vector2)transform.position + direction * .5f);
	}
#endif

	private void OnEnable()
	{
		if (rotateObject)
		{
			rotateObject.DOKill();
			rotateObject.DORotate(new Vector3(0, 0, 360), .3f, RotateMode.FastBeyond360).SetRelative(true).SetLoops(-1);
		}
	}

	private void OnDisable()
	{
		if (rotateObject)
		{
			rotateObject.DOKill();
		}
	}

	public override void SetDirection(float angleInDeg)
    {
		var angleInRad = Mathf.Deg2Rad * angleInDeg;
		direction.x = Mathf.Cos(angleInRad);
		direction.y = Mathf.Sin(angleInRad);

		SetChildDirection(angleInDeg);
	}

	public override void SetDirection(Vector2 direction)
    {
		this.direction = direction;

		SetChildDirection(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg); base.SetDirection(direction);
    }

    public override void Launch()
    {
		rb.velocity = direction * speed;
    }

    public override void Inittialize()
    {
		if (!rb)
			rb = GetComponent<Rigidbody2D>();
	}
}

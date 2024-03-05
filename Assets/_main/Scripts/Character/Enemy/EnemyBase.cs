using Lean.Pool;
using DG.Tweening;

using UnityEngine;
using System;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Base enemy -----------------")]
    
    [Header("# Renderer")]
    [SerializeField, ReadOnly] private bool isFaceRight;
    [SerializeField, ReadOnly] private bool isActive = true;
    [SerializeField] private SpriteRenderer sprRender;
    
    [Header("# Base information")]
    private Transform targetInGame;
    private EnemyDmg enemyDmg;
    private Rigidbody2D rb;
    private Animator animator;

    public Vector2 direction => (targetInGame.position - transform.position).normalized;
    protected Vector2 velo => rb?.velocity ?? Vector2.zero;
    public Transform Target => targetInGame;
    protected Vector2 targetPosition => targetInGame.position;

    [Header("# Movement")]
    [SerializeField, ReadOnly] protected float moveSpeed;

    [Header("- Keep Distance")]
    [SerializeField] protected float minDistance;
    [SerializeField] protected float maxDistance;
    [SerializeField] protected float multiplier;

    [Header("- Move Towards")]
    [SerializeField] protected float minMoveSpeed;
    [SerializeField] protected float maxMoveSpeed;

    protected virtual void Start()
    {
        targetInGame = GameObject.FindGameObjectWithTag("Player").transform;
        enemyDmg = GetComponent<EnemyDmg>();
        rb = GetComponent<Rigidbody2D>();
        animator = sprRender.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (targetInGame == null && enemyDmg.dead)
            return;
        if (isActive)
            PerFixedUpdate();
    }

    protected abstract void PerFixedUpdate();

    public virtual void Initialize() { }

    protected void UpdateDataEnemy()
    {
        if (targetInGame != null && !enemyDmg.dead)
        {
            Vector2 vector = targetInGame.position - base.transform.position;
            Quaternion b = Quaternion.AngleAxis(Mathf.Atan2(vector.y, vector.x) * 57.29578f, Vector3.forward);
            base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, 10f * Time.deltaTime);
        }
    }

    #region Facing direction
    protected void FacingToDirectionMove()
    {
        if (!sprRender)
            return;

        sprRender.flipX = velo.x < 0 ^ isFaceRight;

    }

    protected void FacingToTarget()
    {
        if (!sprRender)
            return;

        sprRender.flipX = direction.x < 0 ^ isFaceRight;
    }
    #endregion

    #region Movement
    protected void SetDirectionMove(Vector2 velo)
    {
        if (!rb)
            return;

        rb.velocity = velo.GetVectorInWorld();
    }

    protected float TargetSpacing()
    {
        // Move to keep a certain distance from player.
        float f = transform.position.x - targetPosition.x;
        float f2 = transform.position.y - targetPosition.y;
        float num = Mathf.Sqrt(Mathf.Pow(f, 2f) + Mathf.Pow(f2, 2f));
        if (num <= minDistance)
        {
            moveSpeed = -1f * multiplier;
        }
        if (num < maxDistance && num > minDistance)
        {
            moveSpeed = 0f;
        }
        if (num >= maxDistance)
        {
            moveSpeed = 1f * multiplier;
        }

        return moveSpeed;
    }

    protected float TargetOrbit()
    {
        // Move to orbit around player.
        return 0;
    }

    protected float MoveToTarget()
    {
        // Moves directly at player.
        return moveSpeed = UnityEngine.Random.Range(minMoveSpeed, maxMoveSpeed);
    }
    #endregion

    #region Bullet spawning
    protected void SpawnBullet(Bullet b, float angle)
    {
        var buttletSpawn = LeanPool.Spawn(b, transform.position, Quaternion.identity);

        buttletSpawn.SetTarget(targetInGame);
        buttletSpawn.SetDirection(angle);
        buttletSpawn.Launch();
    }

    protected void SpawnBullet(Bullet b, Vector2 direction)
    {
        var buttletSpawn = LeanPool.Spawn(b, transform.position, Quaternion.identity);

        buttletSpawn.SetTarget(targetInGame);
        buttletSpawn.SetDirection(direction);
        buttletSpawn.Launch();
    }

    protected void SpawnBullet(Bullet b, float direction, float barrel)
    {
        var buttletSpawn = LeanPool.Spawn(b, transform.position, Quaternion.identity);

        buttletSpawn.SetTarget(targetInGame);
        buttletSpawn.SetDirection(direction, barrel);
        buttletSpawn.Launch();
    }
    #endregion

    #region Animation
    protected void PlayAnim(string nameAnim)
    {
        animator.Play(nameAnim);
    }

    protected void PlayAnim(int hashAnim)
    {
        animator.Play(hashAnim);
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Doesn't deal damage
    /// </summary>
    /// <param name="isHarmless"></param>
    public void Harmless(bool isHarmless)
    {
        isActive = !isHarmless;
        enemyDmg.coll.enabled = !isHarmless;
    }

    /// <summary>
    /// Doesn't take damage
    /// </summary>
    /// <param name="isImmune"></param>
    public void Immnue(bool isImmune, int fadeValue = 0, float duration = .2f, int loops = 5)
    {
        //sprRender.color = isImmune ? Color.black : Color.white;
        if (isImmune)
        {
            sprRender.DOFade(fadeValue, duration)
                    .SetLoops(loops, LoopType.Yoyo);
        }
        enemyDmg.enabled = !isImmune;
    }

    /// <summary>
    /// Blinking effect.
    /// </summary>
    public void Blinking(Action action, float duration = .2f, int loops = 5)
    {
        sprRender.DOColor(Color.grey, duration)
                .SetLoops(loops, LoopType.Yoyo)
                .OnComplete(action.Invoke);
    }

    /// <summary>
    /// Respawns at random position within map limit.
    /// </summary>
    public Vector2 RandomPosition()
    {
        var limitVertical = SpawnManager.Instance.LimitVertical;
        var limitHorizontal = SpawnManager.Instance.LimitHorizonal;
        return new Vector2(limitHorizontal.RandomInRange(), limitVertical.RandomInRange());
    }

    /// <summary>
    /// Mimics the player by performing a jump smash.
    /// </summary>
    public void BossJump(Vector2 destination, float duration, float jumpPower = 1f, int jumpNum = 1)
    {
        transform.DOJump(destination, jumpPower, jumpNum, duration);
    }
    #endregion
}

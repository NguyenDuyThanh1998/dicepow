using DG.Tweening;


using UnityEngine;

public class Charger : EnemyBase
{
    [Header("Charger modifier -----------")]
    [SerializeField] private float speedCharge;
    [SerializeField] private float timeCharge = 1f;
    [SerializeField] private Transform arrow;

    private float timer;
    private bool isCharge = false;
    private Vector2 point;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(point, .5f);
    }

    protected override void Start()
    {
        base.Start();
        PrepareCharge();
    }

    protected override void PerFixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (timer > 1.5f && !isCharge)
        {
            InitCharge();
        }
        else if(!isCharge)
        {
            IndicatorToTarget();
        }
    }

    private void IndicatorToTarget()
    {
        arrow.localPosition = direction;

        arrow.rotation = Quaternion.Euler(0, 0, direction.AngleInDeg());
    }

    private void InitCharge()
    {
        isCharge = true;
        var target = Physics2D.Raycast(transform.position, arrow.right, 20f, 1 << LayerMask.NameToLayer("Boundary"));
        point = target.point;
        PlayAnim("Charge");

        transform.DOMove(target.point, speedCharge)
            .SetSpeedBased(true).SetDelay(timeCharge).SetEase(Ease.InQuad)
            .OnComplete(CompleteCharge)
            .OnStart(StartCharge);
    }

    private void StartCharge()
    {
        PlayAnim("Run");
        arrow.gameObject.SetActive(false);
    }

    private void CompleteCharge()
    {
        PlayAnim("Hurt");
        Camera.main.DOShakePosition(5f);
        DOVirtual.DelayedCall(1f, PrepareCharge);
        
    }

    private void PrepareCharge()
    {
        PlayAnim("Idle");
        isCharge = false;
        arrow.gameObject.SetActive(true);
        timer = 0;
    }
}

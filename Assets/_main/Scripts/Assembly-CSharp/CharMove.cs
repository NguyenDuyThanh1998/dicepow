using System.Collections;
using Lean.Pool;
using UnityEngine;
using UnityEngine.Purchasing;
using DG.Tweening;
using System.Collections.Generic;
using Utilities.Common;

public class CharMove : MonoBehaviour
{
	public enum CharState
	{
		Norm = 0, // idle, run, takehit, dead
		Jump = 1, // jump, roll, fall
		Land = 2, // land
	}

	// Hash
	int blendHash;
	int jumpHash;
	int flipHash;
	int fallHash;
	int landHash;
	int takehitHash;
	int deadHash;

	// anim time
	readonly float jumpTime = .15f;
	readonly float flipTime = .55f;
	readonly float fallTime = .15f;

	private bool RemoveAds {
		get => PlayerPrefs.GetInt("RemoveAds", 0) == 1;
		set => PlayerPrefs.SetInt("RemoveAds", value ? 1 : 0);
	} 

	private Rigidbody2D rb;

	[Header("Modified speed")]
	public float normalSpeed;
	[SerializeField] private List<CharacterStatePowerScripableObject> initItem;
	[SerializeField] private List<CharacterStatePowerScripableObject> dataList;
	private float angle;

	private Quaternion rotation;

	private float poundCooldown;

	public CharState charState;
	[Header("Modified Jump")]
	public float magnifier;
	public float jumpHeight=1f;
	public AnimationCurve jumpCurve;

	public GameObject shockWave;

	public Collider2D coll;

	public int curEye;
	[Header("Reference")]
	[Header("Sprite reference")]
	public SpriteRenderer spRange;

	public SpriteRenderer charSp;

	[SerializeField] private Animator anim;
	[SerializeField] private int cachedAnimHash;

	private float godTime;
	public GameObject marker;

	[Header("Manager reference")]
	public ControlJoystickManager joyStick;
	public GameObject iapRemoveAds;

	private bool dead;

	[Header("Audio")]
	public AudioSource jumpA;
	public AudioSource landA;
    public AudioSource hurtA;
    public AudioSource readyA;

    [Header("Ingame")]
    [SerializeField, ReadOnlyAttribute]
    private CharacterStatePowerScripableObject curState;
	[SerializeField, ReadOnlyAttribute]
	Vector2 lastDirection;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		StartGame(null);
		spRange.transform.localScale = (Vector3.one * magnifier).GetVectorInWorld();

		SelectCurrentState();

#if !UNITY_EDITOR
		joyStick.SetActionJump(PowPow);
#endif
		iapRemoveAds.SetActive(!RemoveAds);
		MakeAnimMarker();

		if (!RemoveAds)
			AdsMAXManager.Instance?.ShowBanner();

		//
		blendHash = Animator.StringToHash("BlendTree");
		jumpHash = Animator.StringToHash("Jump");
		flipHash = Animator.StringToHash("Flip");
		fallHash = Animator.StringToHash("Fall");
		landHash = Animator.StringToHash("Land");
		takehitHash = Animator.StringToHash("TakeHit");
		deadHash = Animator.StringToHash("Dead");

		cachedAnimHash = anim.GetCurrentAnimatorStateInfo(0).shortNameHash;
	}

	private void OnEnable()
    {
		EventDispatcher.AddListener<EDPlayEvent>(StartGame);
		EventDispatcher.AddListener<EDGameOverEvent>(GameOver);
		EventDispatcher.AddListener<EDAddItemData>(AddItem);
		EventDispatcher.AddListener<EDAddBonusData>(AddItemBonus);
    }

    private void OnDisable()
    {
		EventDispatcher.RemoveListener<EDPlayEvent>(StartGame);
		EventDispatcher.RemoveListener<EDGameOverEvent>(GameOver);
		EventDispatcher.RemoveListener<EDAddItemData>(AddItem);
		EventDispatcher.RemoveListener<EDAddBonusData>(AddItemBonus);
	}

    private void AddItemBonus(EDAddBonusData evt)
    {
		var wewapon = evt.itemCore as CharacterStatePowerScripableObject;
		wewapon.AddEffect(evt.bonus);
    }

    private void StartGame(EDPlayEvent evt)
    {
		dataList = new List<CharacterStatePowerScripableObject>();

		dataList.AddRange(initItem);

        foreach (var item in dataList)
        {
			EventDispatcher.Raise(new EDAddItemData() { itemAdd = item, init = true }); 
		}
	}

	private void GameOver(EDGameOverEvent evt)
    {
		dead = true;
		rb.velocity = Vector2.zero;
	}


	private void AddItem(EDAddItemData newItem)
    {
		if (newItem.init)
			return;

		var ni = newItem.itemAdd as CharacterStatePowerScripableObject;
		if (ni != null)
        {
			dataList.Add(ni);
		}
    }

    private void Update()
	{
		if (dead)
			return;

		poundCooldown -= Time.deltaTime;
		godTime -= Time.deltaTime;

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space)){
			PowPow();
        }
#endif

		if (joyStick.IsActive  && charState != CharState.Jump)
        {
            var velo = joyStick.GetDir * normalSpeed;

            if (charState == CharState.Norm)
            {
            }
            if (charState == CharState.Land)
            {
                velo *= .5f;
            }

            SetVelocity(velo);
            lastDirection = joyStick.GetDir;
            UpdateMarker();
        }
        else if (!joyStick.IsActive && charState == CharState.Norm)
        {
            rb.velocity = Vector2.zero;
		}

		anim.SetFloat("Velocity", rb.velocity.magnitude > float.Epsilon ? 1 : 0);
		anim.SetFloat("Random", Random.Range(0, 1) > .5 ? 1 : 0); //set timer for this check.
	}

	private void SetVelocity(Vector2 velo)
    {
		rb.velocity = velo.GetVectorInWorld();
	}

	private void MakeAnimMarker()
    {
		marker.transform.DOKill();
		marker.transform.DORotate(new Vector3(0,0,360f),1f,RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
		marker.transform.DOPunchScale(Vector3.one*.7f,.5f,2,.005f).SetEase(Ease.Linear).SetLoops(-1);
	}

	private void UpdateMarker()
    {
		var position = curState.Range / 2 * magnifier * lastDirection;
		marker.transform.localPosition = PositionOut(position.GetVectorInWorld());
	}

	private Vector2 PositionOut(Vector2 position)
    {
		var limitHorizotal = SpawnManager.Instance.LimitHorizonal;
        var limitVertical = SpawnManager.Instance.LimitVertical;
        var globalPosition = position + (Vector2)transform.position;
        var blockHorizon = !limitHorizotal.CheckInLimit(globalPosition.x);
        var blockVertial = !limitVertical.CheckInLimit(globalPosition.y);

        if (blockVertial || blockHorizon)
        {
            var hit = Physics2D.Raycast(transform.position, position, 10f, 1 << LayerMask.NameToLayer("Boundary"));
            position = hit.point;
            position -= (Vector2)transform.position;
        }

        return position;
    }

    private void SelectCurrentState()
    {
		curEye = Random.Range(1, dataList.Count+1);
		var currentIndex = curEye - 1;
		curState = dataList[currentIndex];

		
		spRange.sprite = curState.RangeSpr;
		spRange.transform.localScale = curState.Range * Vector3.one * magnifier;
		EventDispatcher.Raise(new EDChangeWeapob() { data = curState });

		UpdateMarker();
	}

	public void BuyRemoveAds(Product p)
    {
		RemoveAds = true;
		AdsMAXManager.Instance.HideBanner();
	}

	private void EndLevel()
	{
		Camera.main.transform.DOShakePosition(.2f, .5f, 20);
		ScoreManager.Instance.SaveScore();
		dead = true;
		rb.velocity = Vector2.zero;
	}

	private IEnumerator LockPos(Rigidbody2D _target, float _duration = .169f)
    {
		_target.constraints = RigidbodyConstraints2D.FreezePosition;
		yield return new WaitForSeconds(_duration);
		_target.constraints = RigidbodyConstraints2D.None;
	}

	private IEnumerator Pound()
	{
		//SetVelocity(lastDirection * curEye * magnifier);
		var totalTime = jumpTime + flipTime + fallTime;
		var target = curState.Range * magnifier / 2 * lastDirection;
		
		transform.DOJump( PositionOut(target.GetVectorInWorld()), jumpHeight, 1, totalTime).SetEase(jumpCurve).SetRelative(true);

		// Jump
		charState = CharState.Jump;
		PlayAnimationByHash(jumpHash);
		jumpA.Play();
		yield return new WaitForSeconds(jumpTime);
		
		// Flip
		PlayAnimationByHash(flipHash);
		spRange.color = new Color(spRange.color.r, spRange.color.g, spRange.color.b, 0f);
		marker.SetActive(false);
		yield return new WaitForSeconds(flipTime);
		
		// Fall
		PlayAnimationByHash(fallHash);
		yield return new WaitForSeconds(fallTime);
		
		LeanPool.Spawn(shockWave, base.gameObject.transform.position, Quaternion.identity);
		Camera.main.transform.DOShakePosition(.2f, .5f, 20);

		curState?.Landing(gameObject);
		SelectCurrentState();
		
		// Land
		charState = CharState.Land;
		PlayAnimationByHash(landHash);
		landA.Play();
		yield return StartCoroutine(LockPos(rb));

		spRange.color = new Color(spRange.color.r, spRange.color.g, spRange.color.b, 0.2f);
		charState = CharState.Norm;
		PlayAnimationByHash(blendHash);
		
		yield return new WaitForSeconds(0.6f);
		
		readyA.Play();
		spRange.color = new Color(spRange.color.r, spRange.color.g, spRange.color.b, 1f);
		marker.SetActive(true);
	}


	public void PowPow()
    {
		if (dead)
			return;

		if ( poundCooldown <= 0f)
		{
			poundCooldown = 1.2f;
			charState = CharState.Jump;
			StartCoroutine(Pound());
		}
	}

    /*private IEnumerator Hurt()
    {
        PlayAnimationByHash(takehitHash);
        charSp.DOFade(0, .1f).SetLoops(12, LoopType.Yoyo);
		yield return StartCoroutine(LockPos(rb));
        coll.enabled = false;
		PlayAnimationByHash(blendHash);
	}*/

	private IEnumerator Hurt()
    {
        for (int i = 0; i < 7; i++)
        {
            charSp.enabled = false;
            yield return new WaitForSeconds(0.1f);
            charSp.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
		yield return new WaitForSeconds(.1f);
		coll.enabled = false;
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		if (godTime <= 0f && poundCooldown <= 0.6f && (col.CompareTag("Enemy") || col.CompareTag("EBullet")))
		{
			LifeManager.Instance.MinusLife();
			godTime = 1.5f;
			hurtA.Play();
			coll.enabled = true;
			StartCoroutine(Hurt());
		}
	}

	private void FixedUpdate()
	{
		if (charState != CharState.Jump && !dead && joyStick.IsActive)
		{
			//TODO: Make direction here
			//mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 vector =  rb.velocity.normalized;
			angle = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			//base.transform.rotation = Quaternion.Slerp(base.transform.rotation, rotation, angleRotation * Time.deltaTime);
			anim.transform.rotation = Quaternion.identity;
			var currentAngle = transform.rotation.eulerAngles.z;
			charSp.flipX = rb.velocity.x < 0;
		}
	}

	void PlayAnimationByHash(int _animHash, float _duration = .1f, int _layer = 0)
	{
		//Debug.Log("Anim: " + m_CachedAnim);
		AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
		bool isSameAnim = cachedAnimHash == _animHash;
		bool isLooping = currentState.loop;
		
		if (isSameAnim && isLooping)
		{
			return;
		}
		else if (isSameAnim && !isLooping)
		{
			anim.Rebind();
			anim.PlayInFixedTime(_animHash);
		}
		else
		{
			//anim.Rebind();
			anim.CrossFade(_animHash, _duration, _layer);
		}
		cachedAnimHash = _animHash;
	}
}

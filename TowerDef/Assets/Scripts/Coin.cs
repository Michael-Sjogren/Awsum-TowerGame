
using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class Coin : MonoBehaviour 
{

	private Rigidbody rb;
	private bool hasJumped = false;
	private Vector3 direction = Vector3.down;
    private float elapsedTime = 0;
	public float maxDistanceToGround;
    public LayerMask groundLayer;

	[MinMaxRange(1f , 50f)]
	public RangedFloat forceVertical;
	public AudioSource audioSource;
	public AuidoEvent coinDropSound;
	public float lifeTime = 10f;
	public float despawnVisualQueueTimeStart = 12;
    private Coroutine routine;
	private GameObject target;
	public float coinMoveSpeed = 5f;

    // Use this for initialization
    void Start () 
	{
		rb = GetComponent<Rigidbody>();
	}

    private void Jump()
    {
		if(rb != null) 
		{
			float yforce = UnityEngine.Random.Range( forceVertical.minValue , forceVertical.maxValue );
			rb.AddForce(Vector3.up * yforce, ForceMode.Impulse);
		}
		hasJumped = true;
    }

	void Update()
	{
		if(hasJumped) 
		{
			elapsedTime += Time.deltaTime;
			if(despawnVisualQueueTimeStart <= elapsedTime)
			{
				//if(routine == null)	
					//routine = StartCoroutine(FlashCoin());
			}
			if(elapsedTime >= lifeTime) 
			{
				Destroy(this.gameObject);
			}
		}
		else 
		{
			Jump();
		}

		if(target != null) 
		{
			
			if(rb != null) 
			{
				Destroy(GetComponent<Gravity>());
				Destroy(rb);
			}

			MoveToTarget(target);
		}
	}

	public IEnumerator FlashCoin()
	{
		yield return new WaitForSeconds(.25f);
		//rend.material.DOFade(1f , 25f);
		yield return new WaitForSeconds(.25f);
		//rend.material.DOFade(.15f , .25f);
		routine = null;
	}
	
	public bool IsOnGround()
	{
		if(Physics.Raycast(transform.position , direction , maxDistanceToGround , groundLayer))
		{
			return true;
		}
		return false;
	}

	public bool IsNotMoving()
	{
		if(rb == null) return true;
		return Vector3.zero.Equals(rb.velocity);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawLine(transform.position , transform.position + (direction * maxDistanceToGround));
	}

	void OnTriggerEnter(Collider other)
	{
		CheckIfPlayerIsInRange(other.gameObject);
	}

    private void CheckIfPlayerIsInRange(GameObject other)
    {
        if (other.CompareTag("Player") /* || other.CompareTag("Tower") */ )
        {
            target = other.gameObject;
        }
    }

    private void MoveToTarget(GameObject gameObject)
    {
        Vector3 dir = target.transform.position - transform.position;
        float distThisFrame = coinMoveSpeed * Time.deltaTime;
        if(dir.magnitude <= distThisFrame) 
        {
			// award player
			Player player = PlayerManager.Instance.player;
			AudioSource source = target.GetComponent<AudioSource>();
			player.ReciveMoney(1);
			coinDropSound.Play(source);
            Destroy(this.gameObject);
            return;
        }
        
        this.transform.rotation = Quaternion.LookRotation(dir , transform.up);
        this.transform.Translate(dir.normalized * distThisFrame , Space.World );
    }
}

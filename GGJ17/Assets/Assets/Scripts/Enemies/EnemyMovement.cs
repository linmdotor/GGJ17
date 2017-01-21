using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour {

	public GameObject currentTarget;
	private Transform targetTransform;

	public GameObject retargetHACK;

	public float speed = 5.0f;

	private float currentTime = 0f;
	private EnemyMovementState currentState;
	private EnemyMovementState previousState;

	public float DelayedRandomTime = 10.0f;


	//STATES
	private enum EnemyMovementState
	{
		NONE,
		MOVING,
		MOVING_AROUND,
		PAUSING,
	}
	public float movingTime = 10.0f;
	public float waitingTime = 2.0f;
	public float pauseTime = 1.5f;



	// Use this for initialization
	void Start () {

		//Get the player by default
		if (!currentTarget)
			currentTarget = GameObject.FindWithTag("PlayerWarrior");

		targetTransform = currentTarget.transform;

		previousState = EnemyMovementState.NONE;
		currentState = EnemyMovementState.MOVING_AROUND;

		DelayedRandomTime *= Random.value;
		currentTime = -DelayedRandomTime;
    }
	
	// Update is called once per frame
	void Update () {

		currentTime += Time.deltaTime;

		switch (currentState)
		{
			case EnemyMovementState.MOVING:
				if (targetTransform != null)
				{
					MoveToTarget(targetTransform.position);
				}


				//Transition to Next State
				if (currentTime >= movingTime)
				{
					currentTime -= movingTime;
					previousState = currentState;
                    currentState = EnemyMovementState.PAUSING;
				}
				break;


			case EnemyMovementState.MOVING_AROUND:
				FaceToTarget(MoveToRandomDirection());

				//Transition to Next State
				if (currentTime >= waitingTime)
				{
					currentTime -= waitingTime;
					previousState = currentState;
					currentState = EnemyMovementState.PAUSING;

					//restart the pause target
					generateNewRandomTarget = true;
				}
				break;

			case EnemyMovementState.PAUSING:
				//Transition to Next State
				if (currentTime >= pauseTime)
				{
					currentTime -= pauseTime;
					if(previousState == EnemyMovementState.MOVING)
					{
						previousState = currentState;
						currentState = EnemyMovementState.MOVING_AROUND;
                    }
					else if(previousState == EnemyMovementState.MOVING_AROUND)
					{
						previousState = currentState;
						currentState = EnemyMovementState.MOVING;
					}

				}
				break;
			default:
				break;
		}


		///HACK PARA CAMBIAR DE OBJETIVO CUANDO
		/// LE METAMOS UN NUEVO GAMEOBJECT EN EL retargetHACK
		if(retargetHACK != null)
		{
			Retarget(retargetHACK);
			retargetHACK = null;
		}

	}

	/// <summary>
	/// MOVES TOWARD THE CURRENT TARGET
	/// THIS METHOD CAN BE AS COMPLEX AS YOU WANT
	/// </summary>
	/// <param name="targetPosition"></param>
	public void MoveToTarget(Vector3 targetPosition)
	{
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
		FaceToTarget(targetPosition);
    }


	/// <summary>
	/// MOVES TO THE CURRENT DIRECTION, AND CHANGE IT WHEN COLLIDE
	/// WITH THE WALL
	/// </summary>
	public void MoveToDirection()
	{

	}
	void OnTriggerEnter2D(Collider2D other)
	{
		//Change currentDirection
		//Debug.Log("YO, " + this.name + " HE CHOCADO CON: " + other.name);
	}


	/// <summary>
	/// GET A RANDOM TARGET NEAR THE ENEMY, AND MOVES TOWARD IT 
	/// </summary>
	private Vector3 movingAroundTarget = Vector3.zero;
	private bool generateNewRandomTarget = true;
	//Generates 3 near targets, and move to one to other, until reaches all of it
	//returns the position of the current target
	public Vector3 MoveToRandomDirection()
	{
		///When we haven't a target, generate it
		if(generateNewRandomTarget)
		{
			movingAroundTarget = transform.position + new Vector3((Random.value - 0.5f)*10.0f, (Random.value - 0.5f)*10.0f, 0f);
			generateNewRandomTarget = false;
        }

		//moves a step to the target, if has not reached it
		if ((transform.position - movingAroundTarget).magnitude > 0.5f)
		{
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, movingAroundTarget, step);
		}

		return movingAroundTarget;
	}

	//face to the target, rotation magic trick
	public void FaceToTarget(Vector3 targetPosition)
	{
		var dir = targetPosition - transform.position;
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
	}

	public void Retarget(GameObject newTarget)
	{
		currentTarget = newTarget;
		targetTransform = newTarget.transform;
    }

}

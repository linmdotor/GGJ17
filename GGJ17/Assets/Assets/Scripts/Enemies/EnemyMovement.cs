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
	public float movingAroundTime = 1.0f;
	public float pauseTime = 1.5f;



	// Use this for initialization
	void Start () {

		//Get the player by default
		if (!currentTarget)
			currentTarget = GameObject.FindWithTag("PlayerWarrior");

		targetTransform = currentTarget.transform;

		previousState = EnemyMovementState.NONE;
		currentState = EnemyMovementState.MOVING;
		this.GetComponentInChildren<Animator>().SetBool("Walking", true);


		DelayedRandomTime *= Random.value;
		currentTime = 0f;


		//Initialize "infinite" positions array
		InfitePoint = new Vector3[numberDirections];
		InfitePoint[0] = new Vector3(-100000f, 0, 0); //LEFT
		InfitePoint[1] = new Vector3(100000f, 0, 0); //RIGHT
		InfitePoint[2] = new Vector3(0, 100000f, 0); //UP
		InfitePoint[3] = new Vector3(0, -100000f, 0); //DOWN
		//InfitePoint[4] = new Vector3(-100000f, 100000f, 0);
		//InfitePoint[5] = new Vector3(-100000f, -100000f, 0);
		//InfitePoint[6] = new Vector3(100000f, -100000f, 0);
		//InfitePoint[7] = new Vector3(100000f, 100000f, 0);

		//Initialize in a random direction
		currentDirection = InfitePoint[Mathf.FloorToInt(Random.value * numberDirections)];

    }
	
	// Update is called once per frame
	void Update () {

		currentTime += Time.deltaTime;

		switch (currentState)
		{
			case EnemyMovementState.MOVING:
				//MOVE 1
				//if (targetTransform != null)
				//	MoveToTarget(targetTransform.position);

				//MOVE 2
				if (Random.value < 0.01f) //Sometimes change direction
					currentDirection = GetInfinitePoint();
				MoveToRandomDirection();

				//Transition to Next State
				if (currentTime >= (movingTime+DelayedRandomTime))
				{
					currentTime -= (movingTime+DelayedRandomTime);
					previousState = currentState;
                    currentState = EnemyMovementState.PAUSING;

					//Call Idle Animation
					this.GetComponentInChildren<Animator>().SetBool("Walking", false);
                }
				break;


			case EnemyMovementState.MOVING_AROUND:
				FaceToTarget(MoveToRandomPosition());

				//Transition to Next State
				if (currentTime >= movingAroundTime)
				{
					currentTime -= movingAroundTime;
					previousState = currentState;
					currentState = EnemyMovementState.PAUSING;

					//Call Idle Animation
					this.GetComponentInChildren<Animator>().SetBool("Walking", false);

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

					//Call Moving Animation
					this.GetComponentInChildren<Animator>().SetBool("Walking", true);

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

	//HACK PARA AVANZAR EN LAS 8 DIRECCIONES
	private Vector3[] InfitePoint;
	private const int numberDirections = 4;
	private Vector3 currentDirection;
	/// <summary>
	/// MOVES TO THE CURRENT DIRECTION, AND CHANGE IT WHEN COLLIDE
	/// WITH THE WALL
	/// </summary>
	public void MoveToRandomDirection()
	{
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, currentDirection, step);
		FaceToTarget(currentDirection);
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		//Change currentDirection if collides with other enemies or walls
		if (this.CompareTag("Enemy"))
		{
			if (other.CompareTag("Enemy") || other.CompareTag("Wall") || other.CompareTag("Furniture"))
			{
				if (Random.value > 0.5f) //RANDOM CHANCE TO FOLLOW THE PLAYER
					currentDirection = GetOppositePoint(currentDirection);
                else { 
                    if(currentTarget != null)
					    currentDirection = currentTarget.transform.position;
                    else
                    {
                        currentDirection = GetOppositePoint(currentDirection);
                        currentTarget = GameObject.FindWithTag("PlayerWarrior");
                    }
                }   
            }
		}
	}

	private Vector3 GetInfinitePoint()
	{
		int randIndex = Mathf.FloorToInt(Random.value * numberDirections);
		return InfitePoint[randIndex];
	}

	public Vector3 GetOppositePoint(Vector3 point)
	{
		return new Vector3(point.x * -1f, point.y * -1f, 0f);
    }


	/// <summary>
	/// GET A RANDOM TARGET NEAR THE ENEMY, AND MOVES TOWARD IT 
	/// </summary>
	private Vector3 movingAroundTarget = Vector3.zero;
	private bool generateNewRandomTarget = true;
	//Generates 3 near targets, and move to one to other, until reaches all of it
	//returns the position of the current target
	public Vector3 MoveToRandomPosition()
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

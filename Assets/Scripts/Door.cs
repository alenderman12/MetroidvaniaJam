using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	public enum Direction
	{
		UpDown,
		LeftRight
	}
	public enum TransitionState
	{
		Idle,
		Start,
		Mid,
		End,
	}
	[SerializeField] private TransitionState transitionState = TransitionState.Idle;
	[Space]
	[SerializeField] private PolygonCollider2D roomA = null;
	[SerializeField] private PolygonCollider2D roomB = null;
	[Space]
	[SerializeField] private DoorTrigger entryA = null;
	[SerializeField] private DoorTrigger entryB = null;
	[Space]
	[SerializeField] private Transform entryPointA = null;
	[SerializeField] private Transform entryPointB = null;
	[Space]
	[SerializeField] private Direction direction = Direction.LeftRight;
	[SerializeField] private float doorCooldownMaxTime = 1f;
	[SerializeField] private float transitionMaxTime = 0.25f;

	private float transitionCountdown = 0;
	private float doorCooldownCountdown = 0;
	public bool doorActivated { get; private set; }


	private Vector3 startPosition = Vector3.zero;
	private Vector3 midPosition = Vector3.zero;
	private Vector3 endPosition = Vector3.zero;

	private Vector3 startPositionCam = Vector3.zero;
	private Vector3 endPositionCam = Vector3.zero;

	private void Update()
	{
		doorActivated = true;

		switch (transitionState)
		{
			case TransitionState.Start:
				if (transitionCountdown < transitionMaxTime)
				{
					transitionCountdown += Time.deltaTime;
					GameManager.instance.playerMove.transform.position = Vector3.Lerp(startPosition, midPosition, transitionCountdown / transitionMaxTime);
				}
				else
				{
					GameManager.instance.playerMove.transform.position = midPosition;
					transitionState = TransitionState.Mid;
					transitionCountdown = 0f;
				}
				break;
			case TransitionState.Mid:
				if (transitionCountdown < transitionMaxTime)
				{
					transitionCountdown += Time.deltaTime;
					GameManager.instance.playerMove.transform.position = Vector3.Lerp(midPosition, endPosition, transitionCountdown / transitionMaxTime);
					GameManager.instance.camTransform.position = Vector3.Lerp(startPositionCam, endPositionCam, transitionCountdown / transitionMaxTime);
				}
				else
				{
					GameManager.instance.playerMove.transform.position = endPosition;
					GameManager.instance.camTransform.position = endPositionCam;
					GameManager.instance.CM_Camera.enabled = true;
					transitionState = TransitionState.End;
					doorCooldownCountdown = 0;
				}
				break;
			case TransitionState.End:
				if (doorCooldownCountdown < doorCooldownMaxTime)
				{
					doorCooldownCountdown += Time.deltaTime;
				}
				else
				{
					transitionState = TransitionState.Idle;
				}
				break;
		}
	}

	public void MovePlayerToRoom(bool isA)
	{
		if (transitionState == TransitionState.Idle)
		{
			StartCoroutine(MovePlayerToRoomCoroutine(isA));
		}
	}

	IEnumerator MovePlayerToRoomCoroutine(bool isA)
	{
		GameManager.instance.playerAttack.enabled = false;
		GameManager.instance.playerMove.enabled = false;

		GameManager.instance.CM_Camera.enabled = false;
		GameManager.instance.CM_Confiner.m_BoundingShape2D = isA ? roomB : roomA;

		startPositionCam = GameManager.instance.camTransform.position;
		endPositionCam = new Vector3(isA ? startPositionCam.x + 20 : startPositionCam.x - 20,
			startPositionCam.y,
			startPositionCam.z);

		startPosition = GameManager.instance.playerMove.transform.position;
		if (direction == Direction.UpDown)
		{
			midPosition = new Vector3(startPosition.x,
				entryPointA.position.y + (entryPointB.position.y - entryPointA.position.y) / 2f,
				0);
			endPosition = new Vector3(startPosition.x,
				isA ? entryPointB.position.y : entryPointA.position.y,
				0);
		}
		else
		{
			midPosition = new Vector3(
				entryPointA.position.x + (entryPointB.position.x - entryPointA.position.x) / 2f,
				startPosition.y,
				0);
			endPosition = new Vector3(
				isA ? entryPointB.position.x : entryPointA.position.x,
				startPosition.y,
				0);
		}

		transitionCountdown = 0f;
		transitionState = TransitionState.Start;

		yield return new WaitUntil(() => transitionState == TransitionState.End);
		GameManager.instance.playerAttack.enabled = true;
		GameManager.instance.playerMove.enabled = true;
	}

}

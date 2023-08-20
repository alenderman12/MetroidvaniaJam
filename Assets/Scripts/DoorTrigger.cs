using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
	[SerializeField] private Door door = null;
	[SerializeField] private bool isA = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (door.doorActivated)
		{
			if (collision.gameObject.tag.Equals("Player"))
			{
				door.MovePlayerToRoom(isA);
			}
		}
	}
}

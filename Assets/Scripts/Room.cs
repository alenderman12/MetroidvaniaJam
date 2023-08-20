using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
	public int roomSizeX = 10;
	public int roomSizeY = 10;
	[Space]
	public int roomPosX = 0;
	public int roomPosY = 0;

	public Room(int roomSizeX, int roomSizeY, int roomPosX, int roomPosY)
	{
		this.roomSizeX = roomSizeX;
		this.roomSizeY = roomSizeY;
		this.roomPosX = roomPosX;
		this.roomPosY = roomPosY;
	}

}

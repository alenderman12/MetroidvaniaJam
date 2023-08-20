using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class RoomManagerWindow : EditorWindow
{
	// Create Room
	// - creates new room of specified size
	// - adds room to list

	[MenuItem("Window/Custom Tools/Room Manager")]
	public static void ShowExample()
	{
		RoomManagerWindow wnd = GetWindow<RoomManagerWindow>();
		wnd.titleContent = new GUIContent("RoomManagerWindow");
	}

	public void CreateGUI()
	{
		// Each editor window contains a root VisualElement object
		VisualElement root = rootVisualElement;

		IntegerField roomWidthField = new IntegerField("New Room Width");
		root.Add(roomWidthField);
		IntegerField roomHeightField = new IntegerField("New Room Height");
		root.Add(roomHeightField);

		// Create button
		Button button = new Button();
		button.text = "Create New Room";
		//button.clickable.clicked += () => CreateNewRoom(roomWidthField.value, roomHeightField.value);
		root.Add(button);


	}

	public void CreateNewRoom(int sizeX, int sizeY, int posX, int posY)
	{
		Room newRoom = new Room(sizeX, sizeY, posX, posY);

	}
}

using Godot;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

public partial class Break : Control
{
	#region VARIABLES
	[Export] PackedScene gameScene;
	
	GmSelection gmSelection;
	CharacterSelection characterSelection;
	#endregion

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		characterSelection = GetNode<CharacterSelection>("CharacterSelection");
		gmSelection = GetNode<GmSelection>("GamemodeSelection");

		characterSelection.Visible = false;
		gmSelection.Run();
	}

	public void StartCharacterSelection()
	{
		characterSelection.Visible = true;
		characterSelection.SetUpLayout();
	}

	public void StartGame()
	{
		GetTree().ChangeSceneToPacked(gameScene);
	}
}

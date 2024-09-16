using Godot;
using System;

public partial class MainMenu : Control
{
	[Export] PackedScene mainGameScene;
	
	public void OnPlayButtonPressed()
	{
		GetTree().ChangeSceneToPacked(mainGameScene);
	}

	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}

    public override void _Ready()
    {
		GodotObject gameManager = new GameManager() as GodotObject;
        GetTree().Root.CallDeferred("add_child", gameManager);
    }
}

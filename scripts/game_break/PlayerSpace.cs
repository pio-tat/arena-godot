using Godot;
using System;
using System.Linq;

public partial class PlayerSpace : Control
{
	[Export] public int id = 0;
	public Player player;
	Label nameLabel;
	TextureRect characterTexture;
	int selectedIndex;

	bool joyBlocked = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		nameLabel = GetNode<Label>("VBox/labelCont/player name");
		characterTexture = GetNode<TextureRect>("VBox/textureCont/selectedCharacter");

		player = PlayerManager.PlayerList.Single<Player>(x => x.Id == id);
		nameLabel.Text = player.name;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		characterTexture.Texture = CharactersStorage.GetCharacter(player.currentCharacter).Texture;
		if(IsLeftPressed()){
			joyBlocked = true;

			if(selectedIndex > 0)
				selectedIndex--;
		}
		if(IsRightPressed())
		{
			joyBlocked = true;

			if(selectedIndex < player.charactersUnlocked.Count - 1)
				selectedIndex++;
		}
		player.currentCharacter = player.charactersUnlocked[selectedIndex];

		//unlocking the joys
		if(id > 1 && joyBlocked && Mathf.Abs(Input.GetJoyAxis(id - 2, JoyAxis.LeftX)) < 0.2f)
			joyBlocked = false;
	}

	bool IsLeftPressed()
	{
		if(id < 2){
			return Input.IsActionJustPressed("left" + (id+1));
		}else{
			return Input.GetJoyAxis(id - 2, JoyAxis.LeftX) < -0.2f && !joyBlocked;
		}
	}
	bool IsRightPressed()
	{
		if(id < 2){
			return Input.IsActionJustPressed("right" + (id+1));
		}else{
			return Input.GetJoyAxis(id - 2, JoyAxis.LeftX) > 0.2f && !joyBlocked;
		}
	}
}

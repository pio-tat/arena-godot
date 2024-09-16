using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class PlayerModeChooser : Control
{
	RichTextLabel text;
	ColorRect triangle;
	Control[] modePanels = new Control[3];

	[Export] Color[] colorlist = new Color[6];
	public int id;
	bool joyBlocked = false;

	int vote = 1;
	public static List<PlayerModeChooser>[] modeVotes = {new List<PlayerModeChooser>(), new List<PlayerModeChooser>(), new List<PlayerModeChooser>()};

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		modeVotes[1].Add(this);

		for(int i = 0; i < 3; i++){
			modePanels[i] = GetParent().GetChild(i) as Control;
		}
		GetParent().RemoveChild(this);
		modePanels[vote].AddChild(this);
		SetPosition(new Vector2(0, -150));

		text = GetChild<RichTextLabel>(1);
		triangle = GetChild<ColorRect>(0);

		int playerNum = PlayerManager.PlayerList.IndexOf(PlayerManager.PlayerList.Single(player => player.Id == id));

		text.AddThemeColorOverride("default_color", colorlist[playerNum]);
		text.Text = "[center]P" + (playerNum+1);

		triangle.Color = colorlist[playerNum];
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//moves to right when right is pressed
		if(IsRightPressed() && vote != 2){
			joyBlocked = true;

			//changes votes
			modeVotes[vote].Remove(this);
			vote++;
			modeVotes[vote].Add(this);

			//change parents
			GetParent().RemoveChild(this);
			modePanels[vote].AddChild(this);

			ChangeNeighboursWhenQuit(vote - 1); //adjust neighbours position
			CheckIfNeighbours(); //adjust position
		}

		//moves to left when left is presed
		if(IsLeftPressed() && vote != 0){
			joyBlocked = true;

			//changes votes
			modeVotes[vote].Remove(this);
			vote--;
			modeVotes[vote].Add(this);

			//changes parent
			GetParent().RemoveChild(this);
			modePanels[vote].AddChild(this);

			ChangeNeighboursWhenQuit(vote + 1); //adjust neighbours position
			CheckIfNeighbours(); //adjust poisition
		}

		if(id > 1 && joyBlocked && Mathf.Abs(Input.GetJoyAxis(id - 2, JoyAxis.LeftX)) < 0.2f)
			joyBlocked = false;

		if(id > 1) GD.Print("JoyBlock: " + joyBlocked);
	}

	bool IsLeftPressed()
	{
		if(id < 2){
			return Input.IsActionJustPressed("left" + (id + 1));
		}else{
			return Input.GetJoyAxis(id - 2, JoyAxis.LeftX) < -0.2f && !joyBlocked;
		}
	}
	bool IsRightPressed()
	{
		if(id < 2){
			return Input.IsActionJustPressed("right" + (id + 1));
		}else{
			return Input.GetJoyAxis(id - 2, JoyAxis.LeftX) > 0.2f && !joyBlocked;
		}
	}

	void CheckIfNeighbours()
	{
		if(modeVotes[vote].Count == 1){
			SetPosition(new Vector2(0, Position.Y));
			return;
		}
		foreach(PlayerModeChooser player in modeVotes[vote]){
			player.AdjustPosition();
		}
	}
	void ChangeNeighboursWhenQuit(int previous) //tells to others to adjust their position when quit
	{
		foreach(PlayerModeChooser player in modeVotes[previous]){
			player.AdjustPosition();
		}
	}
	public void AdjustPosition()
	{
		if(modeVotes[vote].Count == 1){
			Position = new Vector2(0, Position.Y);
			return;
		}

		int orderId = 0;
		foreach(PlayerModeChooser player in modeVotes[vote]){
			if(player.id < id)
				orderId++;
		}
		float position = modeVotes[vote].Count * 20;
		position = -position + (80 * orderId);
		Position = new Vector2(position, Position.Y);
	}
}
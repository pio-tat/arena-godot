using Godot;
using System;

public partial class CharacterSelection : Control
{
	[Export] PackedScene playerSpace;
	TimeCounter timer;
	Break parent;

    public override void _Ready()
    {
		parent = GetParent<Break>();
        timer = GetNode<TimeCounter>("TimeCounter");
		timer.OnTimeOut += parent.StartGame;
    }

    public void SetUpLayout()
	{
		int ammount = PlayerManager.PlayerList.Count;
        Control layout = GetNode<Control>(ammount + "p");
		for(int i = 0; i < ammount; i++){
			Control parent = layout.GetNode<Control>(i.ToString());

			PlayerSpace player = playerSpace.Instantiate<PlayerSpace>();
			player.id = i;
			parent.AddChild(player);
			player.AnchorsPreset = 1;
		}

		timer.Position = layout.GetNode<Control>("time").Position;
		timer.Start();
	}
}

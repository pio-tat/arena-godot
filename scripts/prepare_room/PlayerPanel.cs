using Godot;
using System;
using System.Linq;

public partial class PlayerPanel : Panel
{
	public bool ready = false;
	public bool active = false;
	bool stateChanged = false;
	[Export] int id = 2;
	int device;
	PlayerManager manager;
	Control enabled;
	Control disabled;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		enabled = GetChild<Control>(0);
		disabled = GetChild<Control>(1);
		manager = GetTree().CurrentScene as PlayerManager;
		device = id - 2;
	}
    public override void _Process(double delta)
    {
        if(disabled.Visible == false && Input.IsJoyButtonPressed(device, JoyButton.A) && device >= 0){
			ready = !ready;
			GD.Print("Ready: " + ready);
		}
		if(device < 0 && disabled.Visible == false && Input.IsActionJustPressed("down" + (id + 1)) && !stateChanged){
			ready = !ready;
			GD.Print("Ready: " + ready);
		}
		stateChanged = false;
    }



    public void ChangeState(bool isJoypad)
	{
		stateChanged = true;
		active = isJoypad;
		if(isJoypad){
			enabled.Visible = true;
			disabled.Visible = false;
		}else{
			enabled.Visible = false;
			disabled.Visible = true;
			ready = false;
		}
	}
}

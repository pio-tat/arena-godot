using Godot;
using System;

public partial class Attack : Node2D
{
	#region variables
	AttackInfo attackInfo;
	[Export] PackedScene bulletScn;
	public float bulletSpeed = 600f;
	public float bps = 5; //bullets per second

	// PlayerMovement player;
	IPlayer player;

	//fancy calculations
	float fireRate;
	float timeUntilFire = 0;
	float pos = 15f;
	#endregion

    public override void _Ready()
    {
		//getting player script
		var parent = GetParent();
		player = parent as IPlayer;
		attackInfo = GetParent().GetNode<AttackInfo>("AttackInfo");
		
        fireRate = 1 / bps;
    }

    public override void _Process(double delta)
	{
		//attack input
		if(IsFireButtonDown() && timeUntilFire > fireRate){
			Fire();
			timeUntilFire = 0;
		}else{
			timeUntilFire += (float)delta;
		}
	}

	//attack function
	void Fire()
	{
		RigidBody2D bullet = bulletScn.Instantiate<RigidBody2D>(); //instatiating bullet

		//setting up physics things
		bullet.Rotation = GlobalRotation;
		bullet.Position = GlobalPosition;
		bullet.LinearVelocity = bullet.Transform.X * bulletSpeed;
		(bullet as Bullet).playerId = player.PlayerId; //setting up playerId so bullet can recognise if he hitted enemy or player who summoned it 
		(bullet as Bullet).attackInfo = attackInfo;

		GetTree().CurrentScene.AddChild(bullet); //adding bullet to world
	}

	//flipping firepoint so bullet can fire from the correct site
	public void FlipX(bool state)
	{
		if(state){
			Rotation = (float)Math.PI; //this pi thing is because rotation is in radians and not degrees
			Position = new Vector2(-pos, Position.Y);
		}	
		else{
			Position = new Vector2(pos, Position.Y);
			Rotation = 0f;
		}
	}

	//function to check if player click fire button on keyboard or gamepad
	bool IsFireButtonDown()
	{
		if(player.PlayerId < 2)
			return Input.IsActionJustPressed("fire" + (player.PlayerId + 1));

		return Input.GetJoyAxis(player.PlayerId - 2, JoyAxis.TriggerRight) > 0.3f;
	}
}

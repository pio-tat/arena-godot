using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D, IHurtBox, IPlayer
{
	public int PlayerId {get => playerId; set {playerId = value;}}
	[Export] int playerId;

	public int device;
	[Export] float moveSpeed = 150f;
	[Export] float jumpVelocity = 400f;
	CollisionShape2D collider;
	[Export] Attack firePoint;

	public PlayerStats PlayerStats {get => playerStats;}
	PlayerStats playerStats;

	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		device = playerId - 2;
		collider = GetNode<CollisionShape2D>("Collider");
		playerStats = GetNode<PlayerStats>("PlayerStats");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if(!IsOnFloor()){
			velocity.Y += gravity * (float)delta;
		}

		if(Input.IsJoyButtonPressed(device, JoyButton.A) && IsOnFloor()){
			Input.StartJoyVibration(device, 0.5f, 1, 0.1f);
			velocity.Y = -jumpVelocity;
		}
		if(Input.GetJoyAxis(device, JoyAxis.LeftY) > 0.5f && IsOnFloor())
			Position = new Vector2(Position.X, Position.Y + 1);

		velocity.X = 0;
		float walk = Input.GetJoyAxis(device, JoyAxis.LeftX);
		if(walk < -0.2f){
			velocity.X = -moveSpeed;
			firePoint.FlipX(true);
		}	
		else if(walk > 0.2f){
			velocity.X = moveSpeed;
			firePoint.FlipX(false);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public void OnHit(int damage)
	{
		playerStats.Damage(damage);
	}
}

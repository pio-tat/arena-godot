using Godot;
using System;

public partial class KeyboardMovement : CharacterBody2D, IHurtBox, IPlayer
{
	//Ids
	public int PlayerId {get => playerId; set {playerId = value;}}
	int playerId;
    string keyboardId;

	[Export] float moveSpeed = 150f;
	[Export] float jumpVelocity = 400f;
	CollisionShape2D collider;
	[Export] Attack firePoint;

	//Player stats
	public PlayerStats PlayerStats {get => playerStats;}
	PlayerStats playerStats;

	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        keyboardId = (playerId + 1).ToString();
		collider = GetNode<CollisionShape2D>("Collider");
		playerStats = GetNode<PlayerStats>("PlayerStats");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		//when isn't on the floor makes gravity work
		if(!IsOnFloor()){
			velocity.Y += gravity * (float)delta;
		}

		//inputs for jumping and going down
		if(Input.IsActionJustPressed("up" + keyboardId) && IsOnFloor()){
			velocity.Y = -jumpVelocity;
		}
		if(Input.IsActionJustPressed("down" + keyboardId) && IsOnFloor())
			Position = new Vector2(Position.X, Position.Y + 1);

		velocity.X = 0;
		float walk = Input.GetAxis("left" + keyboardId, "right" + keyboardId); //making walk vector
		velocity.X = moveSpeed * walk; //making walk working

		//flipping firepoint
		if(walk < -0.2f){
			firePoint.FlipX(true);
		}	
		else if(walk > 0.2f){
			firePoint.FlipX(false);
		}

		//making walking work
		Velocity = velocity;
		MoveAndSlide();
	}

	//things that happen when hitted by bullet or something like that
	public void OnHit(int damage)
	{
		playerStats.Damage(damage);
	}
}

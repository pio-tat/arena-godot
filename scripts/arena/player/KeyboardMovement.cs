using Godot;
using System;

public partial class KeyboardMovement : CharacterBody2D, IPlayer
{
	#region variables
	CharacterResource characterProperties;
	public CharacterResource CharacterProperties {get => characterProperties; set => characterProperties = value;}
	//Ids
	public int PlayerId {get => playerId; set {playerId = value;}}
	[Export] int playerId;
    string keyboardId;

	[Export] float moveSpeed = 150f;
	[Export] float jumpVelocity = 400f;
	bool doubleJump = false;
	int jumps = 0;

	CollisionShape2D collider;
    private ArenaManager arenaManager;
    [Export] Attack firePoint;


	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	#endregion

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        keyboardId = (playerId + 1).ToString();
		collider = GetNode<CollisionShape2D>("Collider");

		arenaManager = GetParent() as ArenaManager;

		SetUpCharacter();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		//when isn't on the floor makes gravity work
		if(!IsOnFloor()){
			velocity.Y += gravity * (float)delta;
			if(jumps == 0) jumps = 1;
		}else{
			jumps = 0;
		}

		//inputs for jumping and going down
		if(Input.IsActionJustPressed("up" + keyboardId) && (IsOnFloor() || jumps < 2 && doubleJump)){
			velocity.Y = -jumpVelocity;
			jumps += 1;
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

	//things that happen on death
	public void OnDeath()
	{
		arenaManager.RemovePlayerFromCamera(this);
		QueueFree();
	}

	void SetUpCharacter()
	{
		moveSpeed = characterProperties.MoveSpeed;
		jumpVelocity = characterProperties.JumpVelocity;
		doubleJump = characterProperties.DoubleJump;

		GetNode<HealthComponent>("HealthComponent").SetUpHealth(characterProperties.Health);

		GetNode<AttackInfo>("AttackInfo").attackResource = characterProperties.AttackProperties;

		firePoint.bps = characterProperties.AttackProperties.AttacksPerSecond;
		firePoint.bulletSpeed = characterProperties.AttackProperties.BulletSpeed;

		GetNode<Sprite2D>("Sprite").Texture = characterProperties.Texture;
	}
}

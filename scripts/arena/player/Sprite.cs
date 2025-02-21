using Godot;
using System;

public partial class Sprite : Node2D
{
	AnimationTree animator;
	AnimationPlayer player;
	public override void _Ready()
	{
		animator = GetNode<AnimationTree>("Animator");
		player = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	
	public void UpdateAnimationProperties(bool isMoving, float yVelocity)
	{
		if(yVelocity > 0){
			animator.Set("parameters/OnFloor/transition_request", "falling");
		}else if(yVelocity < 0){
			animator.Set("parameters/OnFloor/transition_request", "jumping");
		}else{
			animator.Set("parameters/OnFloor/transition_request", "true");
		}
			
		animator.Set("parameters/Run/blend_amount", isMoving);
	}

	public void JumpAnimation()
	{
		animator.Set("parameters/Jump/request", (int)AnimationNodeOneShot.OneShotRequest.Fire);
	}

	public void FlipX(bool flip)
	{
		if(flip)
			Scale = new Vector2(-0.15f, Scale.Y);
		else
			Scale = new Vector2(0.15f, Scale.Y);
	}
}

using Godot;
using System;
using System.Collections;

public partial class Bullet : RigidBody2D
{
	[Export] bool onHitDestroy = true;
	public int playerId;
	Hitbox hitbox;
	public AttackInfo attackInfo;

	public override void _Ready()
    {
        Timer timer = GetNode<Timer>("Timer");
		timer.Timeout += () => QueueFree();
		timer.WaitTime = attackInfo.attackResource.AttackDuration;
		//QueueFree() is function which deletes node

		hitbox = GetNode<Hitbox>("Hitbox");
		hitbox.attackInfo = attackInfo;

		GetNode<Sprite2D>("Sprite").Texture = attackInfo.attackResource.AttackTexture;
		timer.Start();
    }

	public void OnBodyEntered(Node2D hitInfo)
	{
		if(onHitDestroy)
			QueueFree();
	}

	public void OnHit(Area2D hitInfo)
	{
		OnBodyEntered(hitInfo);
	}
}

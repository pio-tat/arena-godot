using Godot;
using System;

public partial class Bullet : RigidBody2D
{
	public int playerId;

	public override void _Ready()
    {
        Timer timer = GetNode<Timer>("Timer");
		timer.Timeout += () => QueueFree();
		//QueueFree() is function which deletes node
    }

	public void OnBodyEntered(Node2D body)
	{
		if(body is IPlayer)
			if(playerId == (body as IPlayer).PlayerId)
				return;
		if(body is IHurtBox){
			(body as IHurtBox).OnHit(10);
		}
		QueueFree();
	}
}

using Godot;
using System;

public partial class Hitbox : Area2D
{
	[Signal] public delegate void HitEventHandler(Area2D hitInfo);
	[Export] int damage = 0;
	[Export] public AttackInfo attackInfo;
	
	public void OnAreaEntered(Area2D hitInfo)
	{
		if(attackInfo != null){
			if(hitInfo.GetParent().IsInGroup("player"))
				if((hitInfo.GetParent() as IPlayer).PlayerId == attackInfo.PlayerId)
					return;
			if(hitInfo is Hurtbox){
				(hitInfo as Hurtbox).OnHit(attackInfo.AttackResource);
			}
		}else{
			if(hitInfo is Hurtbox){
				(hitInfo as Hurtbox).OnHit(damage);
			}
		}

		
		
		EmitSignal(SignalName.Hit, hitInfo);
	} //make it more universal
}

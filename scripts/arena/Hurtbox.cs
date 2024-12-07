using Godot;
using System;

public partial class Hurtbox : Area2D
{
	[Export] HealthComponent healthComponent;

    public override void _Ready()
    {
        healthComponent = GetParent().GetNode<HealthComponent>("HealthComponent");
    }
    public void OnHit(int damage)
	{
		GD.Print("Damage taken!");
		healthComponent.TakeDamage(damage);
	}
	public void OnHit(AttackResource attack)
	{
		healthComponent.TakeDamage(attack.Damage);
	}
}

using Godot;
using System;

public partial class HealthComponent : Node2D
{
	public int health;
	[Export] public int maxHealth;
	[Signal] public delegate void DeathEventHandler();
	ProgressBar healthBar;

    public override void _Ready()
    {
        
    }

	public void TakeDamage(int damage) //take damage function
	{
		if(health - damage > 0){
            health -= damage;
        }else{
            health = 0;
            Die();
        }
		UpdateHealthBar();
	}

	//function for managing health
	public void Heal(int heal)
	{
		health = health + heal > maxHealth ? maxHealth : health + heal;
		UpdateHealthBar();
	}
	public void SetHealth(int value)
	{
		health = value;
		UpdateHealthBar();
	}

	void UpdateHealthBar()
	{
		if(healthBar != null)
			healthBar.Value = health;
	}

	public void SetUpHealth(int value)
	{
		maxHealth = value;
		health = maxHealth;

		healthBar = GetNodeOrNull<ProgressBar>("HealthBar");
		healthBar.MaxValue = maxHealth;
		UpdateHealthBar();
	}


    private void Die() //die functino which emits singal
    {
        EmitSignal(SignalName.Death);
    }

}

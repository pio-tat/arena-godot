using Godot;
using System;

[GlobalClass]
public partial class CharacterResource : Resource
{
    [Export] string character;
    public string Character {get => character;}

    [Export] AttackResource attackProperties;
    public AttackResource AttackProperties {get => attackProperties;}
    [Export] int health;
    public int Health {get => health;}
    [Export] float moveSpeed;
    public float MoveSpeed {get => moveSpeed;}
    [Export] float jumpVelocity;
    public float JumpVelocity {get => jumpVelocity;}
    [Export] bool doubleJump = false;
    public bool DoubleJump {get => doubleJump;}


    [Export] Texture2D texture;
    public Texture2D Texture {get => texture;}

    [Export] string description;
    public string Description {get => description;}
}

public enum CharacterEnum
{
    Barry,
    Bux,
    Steve,
    Ninja,
    Devil
}
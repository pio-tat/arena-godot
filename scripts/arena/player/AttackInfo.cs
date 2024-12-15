using Godot;
using System;
using System.Collections.Generic;

public partial class AttackInfo : Node2D
{
	int playerId;
	public int PlayerId {get => playerId;}
	
	[Export] public AttackResource attackResource;
	public AttackResource AttackResource {get => attackResource; set => attackResource = value;}

    public override void _Ready()
    {
        playerId = GetParent<IPlayer>().PlayerId;
    }
}
using Godot;
using System;


public interface IPlayer
{
    public int PlayerId {get; set;}
    public CharacterResource CharacterProperties {get; set;}
}
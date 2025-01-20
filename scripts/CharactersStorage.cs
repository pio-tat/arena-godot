using Godot;
using System;
using System.Collections.Generic;

public partial class CharactersStorage : Node
{
	public static CharactersStorage instance;

	static Dictionary<string, CharacterResource> characterDict = new();


    public override void _Ready()
    {
        instance = this;
		string[] resources = DirAccess.GetFilesAt("./resources/characters");
		foreach(string res in resources){
			GD.Print("res");
			string name = res.Left(res.Find("."));
			CharacterResource character = ResourceLoader.Load("./resources/characters/" + res) as CharacterResource;
			characterDict.Add(name, character);
		}
    }

	public static CharacterResource GetCharacter(string character)
	{
		return characterDict[character];
	}
	public static CharacterResource GetCharacter(CharacterEnum character)
	{
		return characterDict[character.ToString()];
	}
}

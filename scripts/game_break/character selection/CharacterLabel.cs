using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class CharacterLabel : Control
{
	public CharacterResource character;
	[Export] CharacterResource[] resources;

	RichTextLabel name;
	TextureRect image;
	
	public void SetCharacter(CharacterEnum characterId)
	{
		this.character = resources.Single(x => x.Character == characterId);

		name.Text = character.Character.ToString();
		image.Texture = character.Texture;
	}
}
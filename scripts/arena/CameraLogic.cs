using Godot;
using System;
using System.Collections.Generic;

public partial class CameraLogic : Camera2D
{
	[Export] float maxZoom = 2.5f;
	[Export] float margin = 120.5f;
	float factorX = 2.5f * 1152;
	float factorY = 2.5f * 648;
	
	public List<Node2D> players = new();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//230.5f, 129.5f
		float horizontal = 1152f;
		float vertical = 648f;
		
		Vector2 screen = GetViewport().GetVisibleRect().Size;
		Vector2 righSize = new Vector2(230.5f * screen.X / horizontal,  129.5f *  screen.Y / vertical);

		factorX = 2.5f * righSize.X;
		factorY = 2.5f * righSize.Y;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 pos = players[0].Position;
		float highestX = players[0].Position.X;
		float lowestX = players[0].Position.X;
		float highestY = players[0].Position.Y;
		float lowestY = players[0].Position.Y;

		foreach(Node2D player in players){
			//X
			if(player.Position.X > highestX)
				highestX = player.Position.X;
			if(player.Position.X < lowestX)
				lowestX = player.Position.X;

			//Y
			if(player.Position.Y > highestY)
				highestY = player.Position.Y;
			if(player.Position.Y < lowestY)
				lowestY = player.Position.Y;
		}
		pos.X = (highestX + lowestX) / 2;
		pos.Y = (highestY + lowestY) / 2;

		Position = pos;
		
		float zoomX = Math.Abs(Position.X - highestX) > Math.Abs(Position.X - lowestX) ? Math.Abs(Position.X - highestX) : Math.Abs(Position.X - lowestX);
		float zoomY = Math.Abs(Position.Y - highestY) > Math.Abs(Position.Y - lowestY) ? Math.Abs(Position.Y - highestY) : Math.Abs(Position.Y - lowestY);
		zoomX += margin;
		zoomY += margin;
		zoomX = factorX / zoomX;
		zoomY = factorY / zoomY;

		if(zoomX < maxZoom || zoomY < maxZoom){
			float zoom = zoomX < zoomY ? zoomX : zoomY;
			Zoom = new Vector2(zoom, zoom);
		}
	}


}

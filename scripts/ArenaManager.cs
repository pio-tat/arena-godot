using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class ArenaManager : Node2D
{
	[Export] public CameraLogic mainCamera;
	[Export] public Node2D[] gamemodes;
	Node2D currentMap;
	List<Player> playerList;
	[Export] PackedScene padPlayerScn;
	[Export] PackedScene keyPlayerScn;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerList = new(PlayerManager.PlayerList);

		currentMap = gamemodes[0].GetChild<Node2D>(0);

		List<Node2D> spawnPoints = currentMap.GetChildren().Cast<Node2D>().ToList();
		Random ran = new();
		foreach(Player player in playerList){
			Node2D spawnPoint = spawnPoints[ran.Next(0, spawnPoints.Count)];
			spawnPoints.Remove(spawnPoint);
			SpawnPlayer(player, spawnPoint);
		}
	}

    public override void _Process(double delta)
    {
        if(playerList.Count == 1){
			GD.Print("Player" + playerList[0].Id + " won!");
			GameManager.RoundOver(playerList[0]);
		}
    }

    public void RemovePlayerFromCamera(IPlayer player)
	{
		mainCamera.players.Remove(player as Node2D);
		playerList.Remove(playerList.Single<Player>(x => x.playerNode == player as Node));
	}
	void SpawnPlayer(Player player, Node2D spawnPoint = null)
	{
		if(spawnPoint == null){
			Random ran = new();
			Node2D[] SPs = currentMap.GetChildren().Cast<Node2D>().ToArray();
			spawnPoint = SPs[ran.Next(0, SPs.Length - 1)];
		}	

		IPlayer playerInfo;
			if(player.Id < 2)
				playerInfo = keyPlayerScn.Instantiate() as IPlayer;
			else
				playerInfo = padPlayerScn.Instantiate() as IPlayer;
			playerInfo.PlayerId = player.Id;
			(playerInfo as Node2D).Position = spawnPoint.Position;
			GetTree().CurrentScene.AddChild(playerInfo as Node);

			player.playerNode = playerInfo as Node;
			mainCamera.players.Add(playerInfo as Node2D);
	}
}

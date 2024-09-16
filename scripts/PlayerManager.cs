using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class PlayerManager : Control
{
	[Export] PackedScene game;
	static List<Player> players = new();
	public static List<Player> PlayerList {get => players;}
	[Export] PlayerPanel[] panels = new PlayerPanel[6];
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//panels = GetChildren().Cast<PlayerPanel>().ToArray();
		//panels[2] = panels[0]; //placeholder, delete it later
		GetJoypads();
		GD.Print(players);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CheckJoypads();
		CheckKeyboard();
		if(CheckIfReady())
			StartGame();
	}

	void GetJoypads()
	{
		Godot.Collections.Array<int> joypads = Input.GetConnectedJoypads();
		if(joypads.Count < 1)
			return;
		
		foreach(int id in joypads){
			players.Add(new Player(id + 2));
			panels[id + 2].ChangeState(true);
			GD.Print(players[0].Id);
		}

		for(int i = 2; i < panels.Length; i++){
			panels[i].ChangeState(joypads.Contains(i - 2));
		}
	}
	void CheckJoypads()
	{
		Dictionary<int, bool> changes = new(); //dict to see what have changed
		Godot.Collections.Array<int> joypads = Input.GetConnectedJoypads(); //gets list of connected joypads
		

		//check if there are some new joypads
		foreach(int pad in joypads){
			try{ //try is beacuse Single throw error if there's no matching element
				Player playercheck = players.Single<Player>(player => player.Id == pad + 2);
			}
			catch{
				players.Add(new Player(pad + 2)); //adds new player
				changes.Add(pad + 2, true); //note changes
			}
		}

		//check if some joypads have disconnected
		for(int i = 0; i < players.Count; i++){
			if(!joypads.Contains(players[i].Id - 2) && players[i].Id > 2){
				changes.Add(players[i].Id, false); //note change
				players.Remove(players[i]); //removes disconnected player
				i--;
			}
		}

		if(changes.Count == 0) return;
		GD.Print("Joypads length: " + joypads.Count);

		//tells the panels that there was made some changes
		foreach(int panelId in changes.Keys){
			GD.Print(panelId + ", " + changes[panelId]);
			panels[panelId].ChangeState(changes[panelId]);
		}
	}
	void CheckKeyboard()
	{
		if(Input.IsActionJustPressed("down1") && !panels[0].active){
			players.Add(new Player(0));
			panels[0].ChangeState(true);
		}
		if(Input.IsActionJustPressed("down2") && !panels[1].active){
			players.Add(new Player(1));
			panels[1].ChangeState(true);
		}
	}

	bool CheckIfReady()
	{
		int ready = 0;
		foreach(PlayerPanel player in panels){
			if(player.ready) ready++;
		}

		return ready == players.Count && ready > 0;
	}
	void StartGame()
	{
		GameManager.Setup();
		GetTree().ChangeSceneToPacked(game);
	}
}

public class Player
{
	int id;
	public int Id {get => id;}
	public Node playerNode;
	public string name;

	public Player(int id)
	{
		this.id = id;
	}
}
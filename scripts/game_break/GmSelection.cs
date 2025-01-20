using Godot;
using System;
using System.Collections.Generic;

public partial class GmSelection : Control
{
	#region variables
	[Export] PackedScene playerSelScn;
	GameModes[] gameModes = new GameModes[3];
	Panel[] gmPanels = new Panel[3];
	TimeCounter timeCounter;

	Break parent;
    #endregion

    public override void _Ready()
    {
        for(int i = 0; i < 3; i++){
			gmPanels[i] = GetChild<Panel>(i);
		}
		parent = GetParent<Break>();
		timeCounter = GetNode<TimeCounter>("TimeCounter");

		timeCounter.OnTimeOut += SelectMode;
    }

    public void Run()
	{
		Visible = true;

		foreach (List<PlayerModeChooser> mode in PlayerModeChooser.modeVotes)
		{
			mode.Clear();
		}
		//spawns players
		foreach(Player player in PlayerManager.PlayerList){
			PlayerModeChooser playerSel = playerSelScn.Instantiate() as PlayerModeChooser;
			playerSel.id = player.Id;
			this.AddChild(playerSel);
		}

		//makes list of all gamemodes
		List<GameModes> listOfGM = new();
		foreach(GameModes gm in Enum.GetValues(typeof(GameModes))){
			listOfGM.Add(gm);
		}

		//chooses three gamemodes from the list
		Random rand = new Random();
		for(int i = 0; i < 3; i++){
			gameModes[i] = listOfGM[rand.Next(0, listOfGM.Count)]; //choose randomly
			listOfGM.Remove(gameModes[i]); //remove chosen item from the list
			(gmPanels[i].GetNode("text") as RichTextLabel).Text = gameModes[i].ToString(); //sets label
		}
	}

	public void SelectMode()
	{
		int[] gmVotes = new int[3]; //list of votes for gamemodes
		int chosenGM = 0; //gamemode with higher score
		List<int> tieVote = new(); //list for gm if there was a tie

		for(int i = 0; i < 3; i++){
			gmVotes[i] = PlayerModeChooser.modeVotes[i].Count;

			if(gmVotes[i] > gmVotes[chosenGM]){ //checks if gm has higher ammount of votes than current leader
				chosenGM = i;
				tieVote.Clear(); //clears tie list
				tieVote.Add(i); //adds this gm to the list
			}

			if(gmVotes[i] == gmVotes[chosenGM]) //if tie adds another gm
				tieVote.Add(i);
		}

		if(tieVote.Count > 1){ //randomly chooses gm if there's a tie
			Random rand = new();
			chosenGM = rand.Next(0, tieVote.Count);
		}

		GameManager.currentGameMode = gameModes[chosenGM]; //sets current gamemode to newly chosen one
		GameManager.StartNewGame();

		Visible = false;
		parent.StartCharacterSelection();
	}
}

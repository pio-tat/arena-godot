using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class GameManager : Node
{
    private static GameManager instantion;
    //gameplay
	public static int round = 0;
    private static Dictionary<int, int> playersRoundsWon = new();
    static Dictionary<int, int> playersVictories = new();
    public static GameModes currentGameMode;
    //settings
    public static int roundsPerGame = 3; //change it later

    public override void _Ready()
    {
        instantion = this;
    }
    public static void Setup()
    {
        foreach(Player player in PlayerManager.PlayerList){
            playersRoundsWon.Add(player.Id, 0);
            playersVictories.Add(player.Id, 0);
        }
    }
    public static void RoundOver(Player winner)
    {
        round++;
        playersRoundsWon[winner.Id]++;

        if(round > roundsPerGame){
            EndTheGame();
            return;
        }

        instantion.GetTree().ReloadCurrentScene();
    }

    public static void StartNewGame()
    {
        foreach(int playerId in playersRoundsWon.Keys){
            playersRoundsWon[playerId] = 0;
        }
        round = 0;
    }
    static void EndTheGame()
    {
        int winnerId = playersRoundsWon[0];
        foreach(int playerId in playersRoundsWon.Keys){ //checks which player have won the most ammount of rounds
            if(playersRoundsWon[playerId] > playersRoundsWon[winnerId]) //if player have more victories than actual winner, new player takes his title
                winnerId = playerId;
        }

        playersVictories[winnerId]++;
        instantion.GetTree().ChangeSceneToFile("res://main_scenes/game_break.tscn");
    }
}

public enum GameModes //change placeholders!
{
    one,
    two,
    three
}
using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class TimeCounter : RichTextLabel
{
	[Signal] public delegate void TimeEventHandler();
	public event TimeEventHandler OnTimeOut;
	[Export] int sec = 5;
	int currentTime;

    public override void _Ready()
    {
        Text = sec.ToString();
		currentTime = sec;
    }
    public void ChangeText()
	{
		currentTime--;
		Text = currentTime.ToString();

		if(currentTime == 0){
			OnTimeOut?.Invoke();
			GD.Print("Time out!");
		}
			
	}
}

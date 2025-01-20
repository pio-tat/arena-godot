using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class TimeCounter : RichTextLabel
{
	[Signal] public delegate void TimeEventHandler();
	public event TimeEventHandler OnTimeOut;
	[Export] int sec = 5;
	[Export] public bool autoStart = false;
	Timer timer;
	int currentTime;

    public override void _Ready()
    {
		timer = GetNode<Timer>("Timer");
		if(autoStart) timer.Start();

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

	public void Start()
	{
		timer.Start();
	}
}

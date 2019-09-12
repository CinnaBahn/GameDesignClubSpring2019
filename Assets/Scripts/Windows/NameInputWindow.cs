using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The class that handles input when the player is entering their name after beating a level.
/// </summary>
public class NameInputWindow : Window
{
    private int numSlots = 5;
    private char[] slots;
    private Text nickname;
    private int currentSlot = 0;

    private void OnEnable()
    {
        StatsWindow s = this.GetComponent<StatsWindow>();
        s.OnWindowDestroy += CreateWindow;
        s.OnWindowDestroy += InitializeSlots;
        s.OnWindowDestroy += EnableControls;
    }

    private void InitializeSlots()
    {
        slots = new char[numSlots];
        for (int i = 0; i < numSlots; i++)
            slots[i] = ' ';
        nickname = GetTextComponent("Nickname");
    }

    /// <inheritdoc/>
    public override void EnableControls()
    {
        InputManager i = InputManager.instance;
        i.onUpPressed += NextLetter;
        i.onDownPressed += PrevLetter;
        i.onRightPressed += nextSlot;
        i.onLeftPressed += prevSlot;

        i.onPrimaryPressed += UploadName;
        i.onPrimaryPressed += DestroyWindow;
        i.onPrimaryPressed += DisableControls;
    }

    /// <inheritdoc/>
    public override void DisableControls()
    {
        InputManager i = InputManager.instance;
        i.onUpPressed -= NextLetter;
        i.onDownPressed -= PrevLetter;
        i.onRightPressed -= nextSlot;
        i.onLeftPressed -= prevSlot;

        i.onPrimaryPressed -= UploadName;
        i.onPrimaryPressed -= DisableControls;
        i.onPrimaryPressed -= DestroyWindow;
    }

    private void OnDisable()
    {
        StatsWindow s = this.GetComponent<StatsWindow>();
        s.OnWindowDestroy -= CreateWindow;
        s.OnWindowDestroy -= InitializeSlots;
        s.OnWindowDestroy -= EnableControls;
    }

    private void changeSlotsBy(int by) { currentSlot = Mathf.Clamp(currentSlot + by, 0, numSlots - 1); }
    private void nextSlot() { changeSlotsBy(1); }
    private void prevSlot() { changeSlotsBy(-1); }

    private void changeLetterBy(int by)
    {
        int newASCII = slots[currentSlot] + by;
        //print("before: " + newASCII);
        if (newASCII == 64 || newASCII == 91)
            newASCII = 32;
        else if (newASCII == 31)
            newASCII = 90;
        else if (newASCII == 33)
            newASCII = 65;
        slots[currentSlot] = (char)newASCII;
        nickname.text = new string(slots);
        //print("after: " + newASCII);
        //print(string.Format("[{0}]nickname: {1}", currentSlot, nickname.text));
    }

    private void NextLetter() { this.changeLetterBy(1); }
    private void PrevLetter() { this.changeLetterBy(-1); }

    private void UploadName()
    {
        print("doot doot doo " + this.nickname.text + " got the highscore");
    }
}

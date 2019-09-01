using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInputWindow : Window
{
    private int numSlots = 5;
    private char[] slots;
    private Text nickname;
    private int currentSlot = 0;

    private void Start()
    {
        ResultsController r = Controller.resultsController;
        r.onStatsOK += new onStatsOKEventHandler(createWindow);
        r.onNameInputOK += new onNameInputOKEventHandler(uploadName);
        r.onNameInputOK += new onNameInputOKEventHandler(destroyWindow);

        r.onLetterCycleNext += new onLetterCycleNextEventHandler(nextLetter);
        r.onLetterCyclePrev += new onLetterCyclePrevEventHandler(prevLetter);
        r.onLetterSlotNext += new onLetterSlotNextEventHandler(nextSlot);
        r.onLetterSlotPrev += new onLetterSlotPrevEventHandler(prevSlot);
    }

    protected override void createWindow()
    {
        base.createWindow();

        slots = new char[numSlots];
        for (int i = 0; i < numSlots; i++)
            slots[i] = ' ';

        // set nicknameText reference
        foreach (Text t in window.transform.GetComponentsInChildren<Text>())
            if (t.gameObject.name == "Nickname")
            {
                nickname = t;
                break;
            }
    }

    private void changeSlotsBy(int by) { currentSlot = Mathf.Clamp(currentSlot + by, 0, numSlots - 1); }
    private void nextSlot() { changeSlotsBy(1); }
    private void prevSlot() { changeSlotsBy(-1); }

    private void changeLetterBy(int by)
    {
        int newASCII = slots[currentSlot] + by;
        print("before: " + newASCII);
        if (newASCII == 64 || newASCII == 91)
            newASCII = 32;
        else if (newASCII == 31)
            newASCII = 90;
        else if (newASCII == 33)
            newASCII = 65;
        slots[currentSlot] = (char)newASCII;
        nickname.text = new string(slots);
        print("after: " + newASCII);
        //print(string.Format("[{0}]nickname: {1}", currentSlot, nickname.text));
    }

    private void nextLetter() { changeLetterBy(1); }
    private void prevLetter() { changeLetterBy(-1); }

    private void uploadName()
    {
        print("doot doot doo " + nickname.text + " got the highscore");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventTimer : TimerController
{
    private DeltaTimeCountDown timerGameStartWait = null;
    public DeltaTimeCountDown GetTimerGameStartWait() {  return timerGameStartWait; }

    public override void InitializeAssignTimer()
    {
        timerGameStartWait = new DeltaTimeCountDown();

        updateCountDowns = new InterfaceCountDown[]
        {
            timerGameStartWait,
        };
    }
}

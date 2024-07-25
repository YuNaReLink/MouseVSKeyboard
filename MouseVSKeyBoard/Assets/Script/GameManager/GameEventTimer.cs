using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventTimer : TimerController
{
    private DeltaTimeCountDown timerGameStartWait = null;
    public DeltaTimeCountDown GetTimerGameStartWait() {  return timerGameStartWait; }

    private DeltaTimeCountDown timerResetGameIdle = null;
    public DeltaTimeCountDown GetTimerResetGameIdle() { return timerResetGameIdle; }

    private DeltaTimeCountDown timerResultOutputWait = null;
    public DeltaTimeCountDown GetTimerResultOutputWait() {  return timerResultOutputWait; }
    public override void InitializeAssignTimer()
    {
        timerGameStartWait = new DeltaTimeCountDown();
        timerResetGameIdle = new DeltaTimeCountDown();
        timerResultOutputWait = new DeltaTimeCountDown();

        updateCountDowns = new InterfaceCountDown[]
        {
            timerGameStartWait,
            timerResetGameIdle,
            timerResultOutputWait,
        };
    }
}

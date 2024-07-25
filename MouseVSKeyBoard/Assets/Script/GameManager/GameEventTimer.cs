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

    private DeltaTimeCountDown timerMouseClickUICoolDown = null;
    public DeltaTimeCountDown GetTimerMouseClickUICoolDown() { return timerMouseClickUICoolDown; }
    private DeltaTimeCountDown timerKeyClickUICoolDown = null;
    public DeltaTimeCountDown GetTimerKeyClickUICoolDown() { return timerKeyClickUICoolDown; }
    public override void InitializeAssignTimer()
    {
        timerGameStartWait = new DeltaTimeCountDown();
        timerResetGameIdle = new DeltaTimeCountDown();
        timerResultOutputWait = new DeltaTimeCountDown();
        timerMouseClickUICoolDown = new DeltaTimeCountDown();
        timerKeyClickUICoolDown = new DeltaTimeCountDown();

        updateCountDowns = new InterfaceCountDown[]
        {
            timerGameStartWait,
            timerResetGameIdle,
            timerResultOutputWait,
            timerMouseClickUICoolDown,
            timerKeyClickUICoolDown,
        };
    }
}

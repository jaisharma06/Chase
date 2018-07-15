using System;
public class EventManager{

    //This event is called whenever the game is over by the playercontroller
    public static Action OnGameOver;

    public static void GameOver()
    {
        if (OnGameOver != null)
            OnGameOver();
    }
}

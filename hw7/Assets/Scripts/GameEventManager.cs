using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager
{
    private static GameEventManager _instance;

    public delegate void OnPlayerEnterArea(int area);
    public static event OnPlayerEnterArea onPlayerEnterArea;

    public delegate void OnZombieCollideWithPlayer();
    public static event OnZombieCollideWithPlayer onZombieCollideWithPlayer;

    public delegate void OnPlayerCollideWithBlueIdol();
    public static event OnPlayerCollideWithBlueIdol onPlayerCollideWithBlueIdol;

    private GameEventManager()
    {

    }

    public static GameEventManager GetInstance()
    {
        if (_instance == null) {
			_instance = new GameEventManager();
		}
		return _instance;
    }

    public void PlayerEnterArea(int area)
    {
        onPlayerEnterArea?.Invoke(area);
    }

    public void ZombieCollideWithPlayer()
    {
        onZombieCollideWithPlayer?.Invoke();
    }

    public void PlayerCollideWithBlueIdol()
    {
        onPlayerCollideWithBlueIdol?.Invoke();
    }
}

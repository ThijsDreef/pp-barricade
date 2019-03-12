using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
  
  List<PlayerController> players;

  public void StartGame(int playerAmount) {
    players = new List<PlayerController>();
    for (int i = 0; i < playerAmount; i++)
      players.Add(new PlayerController());
  }

  
}

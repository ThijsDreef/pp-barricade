using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
  [SerializeField]
  private MenuController menuController = null;
  [SerializeField]
  private DiceController dice = null;
  [SerializeField]
  private Text currentPlayerText = null;
  [SerializeField]
  private GameObject[] startFields = new GameObject[4];
  [SerializeField]
  private GameObject[] spawnPawns = new GameObject[4];
  private List<PlayerController> players;
  private int currentPlayer;
  private int targetTypeID;


  private void Start() {
    dice.onDiceRollFinish += this.onDiceRollFinish;
  }

  private void onDiceRollFinish(int i) {
    if (targetTypeID == 0) players[currentPlayer].HighlightUnits<SneakyPawn>(true, Color.red);
    else players[currentPlayer].HighlightUnits<HeavyPawn>(true, Color.red);
  }

  public void StartGame(int playerAmount) {
    players = new List<PlayerController>();
    for (int i = 0; i < playerAmount; i++) {
      players.Add(new PlayerController());
      Field[] fields = startFields[i].GetComponentsInChildren<Field>();
      for (int j = 0; j < spawnPawns.Length; j++) {
        Pawn pawn = (Instantiate(spawnPawns[j], fields[j].gameObject.transform.position, Quaternion.identity).GetComponent<Pawn>());
        pawn.MoveToField(fields[j], null);
        players[i].AddUnit(pawn);
      }
    }
    StartTurn(0);
  }

  private void StartTurn(int i) {
    currentPlayer = i;
    menuController.EnableMenu(5);
    currentPlayerText.text = "Next Up Player: " + (currentPlayer + 1);
  }

  public void RollDiceForCurrentPlayer(DiceData data) {
    dice.Roll(data); 
    targetTypeID = data.id;
    menuController.EnableMenu(2);
  } 
}

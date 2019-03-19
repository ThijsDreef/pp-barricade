using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
  public static GameController Instance;

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
  [SerializeField]
  private GameObject fieldHolder = null;
  [SerializeField]
  private GameObject[] unselectableRows = new GameObject[0];
  [SerializeField]
  private Material[] fatPlayerMaterials;
  [SerializeField]
  private Material[] sneakyGuyMaterials;
  public List<Field> selectableFields { get; private set;} = new List<Field>();
  private List<PlayerController> players;
  private MoveController moveController = new MoveController();
  public int currentPlayer { get; private set; }
  private int targetTypeID;
  private int currentRol = 0;


  private void Start() {
    dice.onDiceRollFinish += this.onDiceRollFinish;
    if (Instance == null) Instance = this;
    else Destroy(this);

    for (int i = 0; i < fieldHolder.transform.childCount; i++) {
      bool shouldSkip = false;
      for (int j = 0; j < unselectableRows.Length; j++) {
        if (fieldHolder.transform.GetChild(i).gameObject == unselectableRows[j]) {
          shouldSkip = true;
          break;
        }
      }
      if (shouldSkip) continue;
      selectableFields.AddRange(fieldHolder.transform.GetChild(i).GetComponentsInChildren<Field>());
    }
  }

  private void onDiceRollFinish(int i) {
    currentRol = i;
    if (i == 0) {
      dice.Respawn();
      NextTurn();
      return;
    } 
    if (targetTypeID == 0) players[currentPlayer].HighlightUnits<SneakyPawn>(true, currentPlayer);
    else players[currentPlayer].HighlightUnits<HeavyPawn>(true, currentPlayer);
  }

  public void SelectUnit(Pawn unit) {
    moveController.StartPathSelection(unit, currentRol);
    dice.Respawn();
  }

  public void SelectField(Field field) {
    if (targetTypeID == 0) players[currentPlayer].HighlightUnits<SneakyPawn>(false, currentPlayer);
    else players[currentPlayer].HighlightUnits<HeavyPawn>(false, currentPlayer);
    moveController.SelectField(field);
  }

  public void NextTurn() {
    if (targetTypeID == 0) players[currentPlayer].HighlightUnits<SneakyPawn>(false, currentPlayer);
    else players[currentPlayer].HighlightUnits<HeavyPawn>(false, currentPlayer);
    currentPlayer += 1;
    currentPlayer %= players.Count;
    StartTurn(currentPlayer);
  }

  public void StartGame(int playerAmount) {
    players = new List<PlayerController>();
    for (int i = 0; i < playerAmount; i++) {
      players.Add(new PlayerController());
      Field[] fields = startFields[i].GetComponentsInChildren<Field>();
      for (int j = 0; j < spawnPawns.Length; j++) {
        Pawn pawn = (Instantiate(spawnPawns[j], fields[j].gameObject.transform.position, Quaternion.identity).GetComponent<Pawn>());
        if(j < 2) {
        Renderer[] rend;
            rend = pawn.GetComponentsInChildren<Renderer>();
            foreach(Renderer renderer in rend) {
            renderer.material = fatPlayerMaterials[i];
            }
        }

        if(j >= 2) {
        Renderer[] rend;
        rend = pawn.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in rend) {
        renderer.material = sneakyGuyMaterials[i];
            }
        }
        pawn.startField = fields[j];
        pawn.MoveToField(fields[j], null);
        pawn.onSelect += SelectUnit;
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

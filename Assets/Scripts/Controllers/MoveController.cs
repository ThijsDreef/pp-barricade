using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine;

class MoveController {

  private HashSet<Field> movedFields;
  private List<Field> selectableFields;
  private Pawn currentPawn;
  private FieldOccupier currentBarier;
  private int currentMoves;
  private bool barierMove = false;
  private Pawn deathPawn;

  public void SelectField(Field field) {
    if (barierMove) {
      MoveBarrier(field);
    } else {
      StartMove(field);
    }
    ResetSelectableFields();
  }

  public MoveController() {
    movedFields = new HashSet<Field>();
    selectableFields = new List<Field>();
  }

  private void StartMoveBarier(FieldOccupier barrier) {
    currentBarier = barrier;
    
    barierMove = true;
    for (int i = 0 ; i < GameController.Instance.selectableFields.Count; i++) {
      Field field = GameController.Instance.selectableFields[i];
      if (field.GetOnFieldType() == FieldOccupierType.EMPTY) {
        field.BarricadeHighLight(true, Color.red);
      }
    }
  }

  public void StartPathSelection(Pawn pawn, int moves) {
    currentPawn = pawn;
    currentMoves = moves;
    ResetSelectableFields();
    movedFields = new HashSet<Field>();
    selectableFields = new List<Field>();
    if (currentMoves == 0) {
      GameController.Instance.NextTurn();
      return;
    }
    for (int i = 0; i < pawn.currentField.Neighbours.Count; i++) {
      Field fieldToAdd = pawn.currentField.Neighbours[i];
      if (fieldToAdd.GetOnFieldType() == FieldOccupierType.BARRICADE && currentMoves != 1) continue;
      fieldToAdd.HighLight(true, Color.red);
      selectableFields.Add(fieldToAdd);
    }
  }

  private void MoveBarrier(Field field) {
    currentBarier.MoveToField(field, GameController.Instance.NextTurn);
    currentBarier.currentField.onField = null;
    field.onField = currentBarier;
    barierMove = false;
    for (int i = 0 ; i < GameController.Instance.selectableFields.Count; i++) {
      Field f = GameController.Instance.selectableFields[i];
      f.BarricadeHighLight(false, Color.red);
    }
  }

  private void ResumePathSelection(bool hasHitBarier) {
    currentPawn.lastmove = true;
    if (hasHitBarier) movedFields = new HashSet<Field>();
    selectableFields = new List<Field>();
    for (int i = 0; i < currentPawn.currentField.Neighbours.Count; i++) {
      Field fieldToAdd = currentPawn.currentField.Neighbours[i];
      if (fieldToAdd.GetOnFieldType() == FieldOccupierType.BARRICADE && currentMoves != 1) continue;
      if (movedFields.Contains(fieldToAdd)) continue;
      fieldToAdd.HighLight(true, Color.red);
      selectableFields.Add(fieldToAdd);
    }
  }


  private void ResetSelectableFields() {
    for (int i = 0; i < selectableFields.Count; i++) {
      selectableFields[i].HighLight(false, Color.white);
    }
  }

  public void StartMove(Field field) {
    ResetSelectableFields();
    StartRecursiveMove(field);
  }

  public void OnLastTile(Field lastField) {
    currentPawn.lastmove = true;
    switch (lastField.GetOnFieldType()) {
      case FieldOccupierType.PLAYER:
                deathPawn = (Pawn)lastField.onField;
                if (lastField.onField == currentPawn) break;
                //Add attack.
        GameController.Instance.StartCoroutine(MoveEnemyAndAttack(StartCoroutineAction, (Pawn)lastField.onField));
        break;
      case FieldOccupierType.BARRICADE:
        StartMoveBarier(lastField.onField);
        break;
    }
    // currentPawn.currentField.onField = null;
    lastField.onField = currentPawn;
  }

  private void StartCoroutineAction() {
        GameController.Instance.StartCoroutine(AttackAnim(currentPawn, deathPawn));
  }

  private void StartRecursiveMove(Field field) {
    currentPawn.lastmove = false;
    movedFields.Add(currentPawn.currentField);
    currentPawn.MoveToField(field, CheckNextField);
    if (field.cost != 0 && currentMoves == 1) {
      OnLastTile(field);
    }
    currentMoves -= field.cost;
  }

  private void CheckNextField() {
    if (currentPawn.currentField.Neighbours.Count == 0) {
        GameController.Instance.EndGame();
        return;
    }
    if (currentMoves <= 0) {
      if (!barierMove) GameController.Instance.NextTurn();
      return;
    }

    int temp = 0;
    bool hitBarricade = false;
    Field target;
    List<Field> possibleTargets = new List<Field>(currentPawn.currentField.Neighbours);

    for (int i = 0; i < currentPawn.currentField.Neighbours.Count; i++) {
      if (movedFields.Contains(currentPawn.currentField.Neighbours[i]) || (currentPawn.currentField.Neighbours[i].GetOnFieldType() == FieldOccupierType.BARRICADE && currentMoves != 1)) {
        possibleTargets.Remove(currentPawn.currentField.Neighbours[i]);
        hitBarricade = (currentPawn.currentField.Neighbours[i].GetOnFieldType() == FieldOccupierType.BARRICADE) || hitBarricade;
      } else temp = i;
    }

    if (possibleTargets.Count != 1) {
      ResumePathSelection(hitBarricade);
      //Idle animation start
      return;
    }
    target = currentPawn.currentField.Neighbours[temp];
    movedFields.Add(target);
    StartRecursiveMove(target);
  }

    public IEnumerator MoveEnemyAndAttack(Action callback, Pawn enemy)
    {
        Vector3 diePos = enemy.currentField.transform.position;
        diePos.z += .9f;
        do
        {
            yield return new WaitForEndOfFrame();
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, diePos, 1f * Time.deltaTime);
        } while (Vector3.Distance(diePos, enemy.transform.position) >= 0.4f);
        callback();
    }

    public IEnumerator AttackAnim(Pawn newFieldPlayer, Pawn deadPawn) {
        Animator animController = newFieldPlayer.GetComponent<Animator>();
        newFieldPlayer.StartAttack();
        yield return new WaitForSeconds(2f);
        deadPawn.MoveToField(deadPawn.startField, null);
    }
};
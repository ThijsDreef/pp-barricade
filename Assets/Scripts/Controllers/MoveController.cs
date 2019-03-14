using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class MoveController {

  private HashSet<Field> movedFields;
  private List<Field> selectableFields;
  private Pawn currentPawn;
  private int currentMoves;

  public MoveController() {
    movedFields = new HashSet<Field>();
    selectableFields = new List<Field>();
  }

  public void StartPathSelection(Pawn pawn, int moves) {
    currentPawn = pawn;
    currentMoves = moves;
    ResetSelectableFields();
    movedFields = new HashSet<Field>();
    selectableFields = new List<Field>();
    for (int i = 0; i < pawn.currentField.Neighbours.Count; i++) {
      Field fieldToAdd = pawn.currentField.Neighbours[i];
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

  private void StartRecursiveMove(Field field) {
    movedFields.Add(currentPawn.currentField);
    currentPawn.MoveToField(field, CheckNextField);
  }

  private void CheckNextField() {
    if (currentMoves <= 0) {
      GameController.Instance.NextTurn();
      return;
    }
    currentMoves--;
    Field target;
    List<Field> possibleTargets = new List<Field>(currentPawn.currentField.Neighbours);
    for (int i = 0; i < possibleTargets.Count; i++) {
      if (movedFields.Contains(possibleTargets[i])) {
        possibleTargets.Remove(possibleTargets[i]);
      }
    }
    if (possibleTargets.Count == 1) {
      target = possibleTargets[0];
    } else {
      StartPathSelection(currentPawn, currentMoves);
      return;
    }
    StartRecursiveMove(target);


  }

};
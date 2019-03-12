using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController {
  public Action endTurn;
  public Action startTurn;

  private Pawn[][] units;
  private DiceData[] diceData;
  private const int UNITTYPES = 2;
  private const int UNITAMOUNT = 2;

  public PlayerController() {
    units = new Pawn[UNITTYPES][];
    for (int i = 0; i < UNITTYPES; i++) {
      units[i] = new Pawn[UNITAMOUNT];
    }
  }

  public int getFreeId(int type) {
    for (int i = 0; i < units[type].Length; i++) {
      if (units[type][i] == null) {
        return i;
      }
    }
    return -1;
  }

  public void addUnit(Pawn unit) {
    int freeId = getFreeId(unit.type);
    if (freeId == -1) return;
    units[unit.type][freeId] = unit;
  }

  public void removeUnit(Pawn unit) {
    
  }
}

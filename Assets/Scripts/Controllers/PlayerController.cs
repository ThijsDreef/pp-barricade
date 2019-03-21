using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController {
  private List<Pawn> units;
  private const int UNITTYPES = 2;
  private const int UNITAMOUNT = 2;
  
  public PlayerController() {
    units = new List<Pawn>();
  }
  
  public void HighlightUnits<T>(bool enabled, int color) where T : class {
    for (int i = 0; i < units.Count; i++) {
      if (units[i] is T) {
        units[i].EnableHighlight(enabled, color);
      }
    }
  }

  public void SelectUnit(Pawn unit) {
    
  }

  public void AddUnit(Pawn unit) {
    units.Add(unit);
  }
}
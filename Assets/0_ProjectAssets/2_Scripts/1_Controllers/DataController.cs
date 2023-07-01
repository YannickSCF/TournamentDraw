using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.TournamentDraw.MainManagers.Controllers;
using YannickSCF.TournamentDraw.Scriptables;

public class DataController {
    public void SaveDraw() {
        DrawConfiguration config = GameManager.Instance.Config;

        string json = JsonUtility.ToJson(config);
        Debug.Log("Json: " + json);
    }
}

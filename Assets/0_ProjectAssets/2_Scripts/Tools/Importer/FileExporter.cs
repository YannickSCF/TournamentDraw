using AnotherFileBrowser.Windows;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace YannickSCF.TournamentDraw.FileManagement {
    public static class FileExporter {
        public static void SaveFileBrowser(string tournamentName, string jsonContent) {
            var bp = new BrowserProperties();
            bp.filter = "json files (*.json)|*.json";
            bp.filterIndex = 0;

            new FileBrowser().SaveFileBrowser(bp, tournamentName, ".json", path => {
                Debug.Log(path);

                if(path != null) {
                    using (StreamWriter writer = File.CreateText(path)) {
                        {
                            writer.Write(jsonContent);
                        }
                        writer.Close();
                    }
                }
            });
        }
    }
}

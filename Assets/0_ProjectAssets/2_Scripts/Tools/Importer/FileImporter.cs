using AnotherFileBrowser.Windows;
using System;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.TournamentDraw.Models;

namespace YannickSCF.TournamentDraw.Importers {
    public static class FileImporter {

        public static void OpenFileBrowser() {
            var browserProperties = new BrowserProperties();
            string myPath = "(holi)";

            new FileBrowser().OpenFileBrowser(browserProperties, path => {
                myPath = path;
                IDeserializer deserializer;

                if (path.ToLower().EndsWith(".json")) {
                    deserializer = new JSONDeserializer();
                } else if (path.ToLower().EndsWith(".csv")) {
                    deserializer = new CSVDeserializer();
                } else {
                    deserializer = new CSVDeserializer();
                }

                List<ParticipantModel> participants = deserializer.GetParticipantsFromFile(path);

                //PouleBuilder pouleBuilder = new AlphaPouleBuilder();
                //ITierListBuilder tierListBuilder = new StyleTierListBuilder();
                //_ = pouleBuilder.BuildPoules(participants, tierListBuilder);
                Debug.Log("OpenFileBrowser Finished!");
            });

            Debug.Log("Returned path: " + myPath);
        }

        public static List<ParticipantModel> ImportParticipantsFromFile(string filePath) {
            IDeserializer deserializer;

            if (filePath.ToLower().EndsWith(".json")) {
                deserializer = new JSONDeserializer();
            } else if (filePath.ToLower().EndsWith(".csv")) {
                deserializer = new CSVDeserializer();
            } else {
                throw new Exception("File with incorrect extension");
            }

            return deserializer.GetParticipantsFromFile(filePath);
        }

        public static string SelectFileWithBrowser() {
            var browserProperties = new BrowserProperties();
            string res = "";

            new FileBrowser().OpenFileBrowser(browserProperties, path => {
                if (path.ToLower().EndsWith(".json") || path.ToLower().EndsWith(".csv")) {
                    res = path;
                }
            });

            return res;
        }
    }
}

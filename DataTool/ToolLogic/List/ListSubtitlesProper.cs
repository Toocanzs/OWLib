﻿using System;
using System.Collections.Generic;
using DataTool.FindLogic;
using DataTool.Flag;
using static DataTool.Program;
using static DataTool.Helper.STUHelper;
using static DataTool.Helper.IO;

namespace DataTool.ToolLogic.List {
    [Tool("list-subtitles-real", Description = "List subtitles (from audio data)", TrackTypes = new ushort[] {0x5F}, CustomFlags = typeof(ListFlags))]
    public class ListSubtitlesProper : ITool {
        public void IntegrateView(object sender) {
            throw new NotImplementedException();
        }

        public void Parse(ICLIFlags toolFlags) {
            GetSubtitles();
            
            // todo: json

            // if (toolFlags is ListFlags flags)
            //     if (flags.JSON) {
            //         ParseJSON(subtitles, flags);
            //         return;
            //     }
        }

        public void GetSubtitles() {
            Combo.ComboInfo comboInfo = new Combo.ComboInfo();

            HashSet<KeyValuePair<ulong, ulong>> done = new HashSet<KeyValuePair<ulong, ulong>>();

            foreach (ulong key in TrackedFiles[0x5F]) {
                Combo.Find(comboInfo, key);
                if (!comboInfo.VoiceSets.ContainsKey(key)) continue;
                foreach (KeyValuePair<ulong,HashSet<Combo.VoiceLineInstanceInfo>> lineInstance in comboInfo.VoiceSets[key].VoiceLineInstances) {
                    foreach (Combo.VoiceLineInstanceInfo lineInstanceInfo in lineInstance.Value) {
                        if (lineInstanceInfo.Subtitle != 0) {
                            foreach (ulong soundInfoSound in lineInstanceInfo.SoundFiles) {
                                PrintSubtitle(done, soundInfoSound, lineInstanceInfo.Subtitle);
                            }
                        }
                    }
                }
            }
        }

        public void PrintSubtitle(HashSet<KeyValuePair<ulong, ulong>> done, ulong guid, ulong subtitleGUID) {
            KeyValuePair<ulong, ulong> pair = new KeyValuePair<ulong, ulong>(guid, subtitleGUID);
            if (done.Contains(pair)) return;
            done.Add(pair);
            Console.Out.WriteLine($"{GetFileName(guid)} - {GetSubtitleString(subtitleGUID)}");
        }
    }
}
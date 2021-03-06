﻿using System.IO;
using System.Runtime.InteropServices;

namespace OWLib.Types.STUD {
    [System.Diagnostics.DebuggerDisplay(OWLib.STUD.STUD_DEBUG_STR)]
    public class InventoryDescription : ISTUDInstance {
        public uint Id => 0x96ABC153;
        public string Name => "InventoryDescription";

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public unsafe struct Structure {
            public STUDInstanceInfo instance;
            public OWRecord str;
        }

        private Structure header;
        public Structure Header => header;

        public void Read(Stream input, OWLib.STUD stud) {
            using (BinaryReader reader = new BinaryReader(input, System.Text.Encoding.Default, true)) {
                header = reader.Read<Structure>();
            }
        }
    }
}

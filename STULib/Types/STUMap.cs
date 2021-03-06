// File auto generated by STUHashTool
using System.Linq;
using static STULib.Types.Generic.Common;

namespace STULib.Types {
    [STU(0x02514B6A)]
    public class STUMap : STUInstance {
        [STUField(0x4E87690F)]
        public STUGUID MapDataResource1;

        [STUField(0x38F33424)]
        public STUGUID MapDataResource3;

        [STUField(0x5FF3ACFB)]
        public STUMapUnknownNested1[] UnknownNested;

        [STUField(0xD608E9F3)]
        public STUGUID[] Gamemodes;

        [STUField(0xDDC37F3D)]
        public STUGUID MapDataResource2;

        [STUField(0x86C1CFAB)]
        public STUGUID ImageResource1;

        [STUField(0xC6599DEB)]
        public STUGUID ImageResource3;

        [STUField(0xA0AE2E3E)]
        public STUGUID EffectMusic;

        [STUField(0x956158FF)]
        public STUGUID EffectAnnouncer;  // these should be right

        [STUField(0x7F5B54B2)]
        public STUGUID VoiceSet;

        [STUField(0x762B6796, EmbeddedInstance = true)]
        public STU_7D6D8405[] UnknownArray;

        [STULib.STUField(0x506FA8D8)]
        public string m_506FA8D8;

        [STULib.STUField(0xD7A516EC)]
        public string m_D7A516EC;

        [STULib.STUField(0xCA7E6EDC, "m_description")]
        public string Description;

        [STUField(0x5DB91CE2, "m_displayName")]
        public STUGUID DisplayName;

        [STUField(0x1C706502)]
        public STUGUID VariantName;

        [STUField(0xEBCFAD22)]
        public STUGUID SublineString;

        [STUField(0x5AFE2F61)]
        public STUGUID StateStringA;

        [STUField(0x8EBADA44)]
        public STUGUID StateStringB;

        [STUField(0xACB95597)]
        public STUGUID DescriptionB;

        [STUField(0x389CB894)]
        public STUGUID DescriptionA;

        [STUField(0x9386E669)]
        public STUGUID ImageResource2;

        [STULib.STUField(0xEBE72514)]
        public STULib.Types.Generic.Common.STUGUID m_EBE72514;

        [STULib.STUField(0xD58D0365)]
        public STULib.Types.Generic.Common.STUGUID m_D58D0365;

        [STUField(0xAF869CEC)]
        public byte[] Checksum;  // used for highlights / verifying things

        [STUField(0xA125818B)]
        public uint Unknown11;

        [STUField(0x1DD3A0CD)]
        public uint Unknown19; // 0 = assault/escort/hybrid, 1 = other, 2 = event

        [STUField(0x44D13CC2)]
        public int Unknown2;

        public ulong DataKey => (MapDataResource1 & ~0xFFFFFFFF00000000ul) | 0x0DD0000100000000ul;
        public ulong GetDataKey(ushort type) {
            return (DataKey & ~0xFFFF00000000ul) | ((ulong) type << 32);
        }
        public string ChecksumString => string.Join("", Checksum.Select(a => a.ToString("x2")));
    }
    
    [STU(0xAC40722C)]
    public class STUMapUnknownNested1 : STUInstance {
        [STUField(0xED999C8B)]
        public STUGUID Unknown2;

        [STUField(0x4E87690F)]
        public STUGUID Unknown1;
    }

    [STU(0x7D6D8405)]
    public class STU_7D6D8405 : STUInstance {
        [STUField(0xC0A83121)]
        public STUGUID Override;

        [STUField(0x7DD89F4F)]
        public STUGUID Entity;
    }
}

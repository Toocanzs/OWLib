﻿using System;
using System.IO;
using OWLib.Types;
using OWLib.Types.STUD;

namespace OWLib {
  public class STUD {
    private STUDManager manager;

    private STUDHeader header;
    private STUDTableInstanceRecord[] instanceTable;
    private STUDBlob blob;

    public STUDHeader Header => header;
    public STUDTableInstanceRecord[] InstanceTable => instanceTable;
    public STUDBlob Blob => blob;

    public STUD(STUDManager manager, Stream stream) {
      stream.Seek(0, SeekOrigin.Begin);
      this.manager = manager;
      using(BinaryReader reader = new BinaryReader(stream)) {
        header = reader.Read<STUDHeader>();
        blob = manager.NewInstance(header.type, stream);
        ulong instanceTableSz = ((ulong)stream.Length - header.size - 32) / 16;
        instanceTable = new STUDTableInstanceRecord[instanceTableSz];
        stream.Seek(32 + header.size, SeekOrigin.Begin);
        for(ulong i = 0; i < instanceTableSz; ++i) {
          instanceTable[i] = reader.Read<STUDTableInstanceRecord>();
        }
      }
    }

    public static void DumpInstance(TextWriter writer, STUDTableInstanceRecord instance) {
      writer.WriteLine("\tID: {0}", instance.id);
      writer.WriteLine("\tFlags: {0}", instance.flags);
      writer.WriteLine("\tKey: {0}", instance.key);
    }
    
    public void Dump(TextWriter writer) {
      writer.WriteLine("{0} instance records...", InstanceTable.Length);
      for(int i = 0; i < InstanceTable.Length; ++i) {
        writer.WriteLine("Instance {0}", i);
        DumpInstance(writer, InstanceTable[i]);
        writer.WriteLine("");
      }
      manager.Dump(header.type, blob, Console.Out);
    }

    public void Dump(Stream output) {
      using(TextWriter writer = new StreamWriter(output)) {
        Dump(writer);
      }
    }

    public void Dump() {
      Dump(Console.Out);
    }
  }

  public class STUDManager {

    private Type[] handlers;
    private uint[] handlerIds;

    public STUDManager() {
      handlers = new Type[0];
      handlerIds = new uint[0];
    }

    public static STUDManager Create() {
      STUDManager stud = new STUDManager();
      stud.AddHandler<A301496F>();
      return stud;
    }

    public void AddHandler(Type T) {
      int i = handlers.Length;

      {
        Type[] tmpT = new Type[handlers.Length + 1];
        handlers.CopyTo(tmpT, 0);
        handlers = tmpT;
        uint[] tmpI = new uint[handlerIds.Length + 1];
        handlerIds.CopyTo(tmpI, 0);
        handlerIds = tmpI;
      }

      handlers[i] = T;
      handlerIds[i] = (uint)handlers[i].GetField("id", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).GetValue(null);
    }

    public void AddHandler<T>() where T : STUDBlob {
      AddHandler(typeof(T));
    }

    public STUDBlob NewInstance(uint id, Stream data) {
      for(int i = 0; i < handlerIds.Length; ++i) {
        if(handlerIds[i] == id) {
          STUDBlob inst = (STUDBlob)(handlers[i].GetConstructor(new Type[] { }).Invoke(new object[] { }));
          handlers[i].GetMethod("Read", new Type[] { typeof(Stream) }).Invoke(inst, new object[] { data });
          return inst;
        }
      }
      return null;
    }

    public void Dump(uint id, STUDBlob inst) {
      for(int i = 0; i < handlerIds.Length; ++i) {
        if(handlerIds[i] == id) {
          handlers[i].GetMethod("Dump", new Type[] { }).Invoke(inst, new object[] { });
          break;
        }
      }
    }

    public void Dump(uint id, STUDBlob inst, Stream stream) {
      for(int i = 0; i < handlerIds.Length; ++i) {
        if(handlerIds[i] == id) {
          handlers[i].GetMethod("Dump", new Type[] { typeof(Stream) }).Invoke(inst, new object[] { stream });
          break;
        }
      }
    }

    public void Dump(uint id, STUDBlob inst, TextWriter writer) {
      for(int i = 0; i < handlerIds.Length; ++i) {
        if(handlerIds[i] == id) {
          handlers[i].GetMethod("Dump", new Type[] { typeof(TextWriter) }).Invoke(inst, new object[] { writer });
          break;
        }
      }
    }
  }
}
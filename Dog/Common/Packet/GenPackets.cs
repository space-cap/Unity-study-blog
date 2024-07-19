using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ServerCore;

public enum PacketID
{
	C_PlayerInfoReq = 1,
	S_Test = 2,
	
}

public interface IPacket
{
	ushort Protocol { get; }
	void Read(ArraySegment<byte> segment);
	ArraySegment<byte> Write();
}


public class C_PlayerInfoReq : IPacket
{
	public sbyte testByte;
	public long playerId;
	public string name;
	public class Skill
	{
		public int id;
		public short level;
		public float duration;
		public class Attribute
		{
			public int att;
		
			public void Read(ArraySegment<byte> segment, ref ushort count)
			{
				this.att = BitConverter.ToInt32(segment.Array, segment.Offset + count);
				count += sizeof(int);
			}
		
			public bool Write(ArraySegment<byte> segment, ref ushort count)
			{
				bool success = true;
				Array.Copy(BitConverter.GetBytes(this.att), 0, segment.Array, segment.Offset + count, sizeof(int));
				count += sizeof(int);
				return success;
			}	
		}
		public List<Attribute> attributes = new List<Attribute>();
	
		public void Read(ArraySegment<byte> segment, ref ushort count)
		{
			this.id = BitConverter.ToInt32(segment.Array, segment.Offset + count);
			count += sizeof(int);
			this.level = BitConverter.ToInt16(segment.Array, segment.Offset + count);
			count += sizeof(short);
			this.duration = BitConverter.ToSingle(segment.Array, segment.Offset + count);
			count += sizeof(float);
			this.attributes.Clear();
			ushort attributeLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
			count += sizeof(ushort);
			for (int i = 0; i < attributeLen; i++)
			{
				Attribute attribute = new Attribute();
				attribute.Read(segment, ref count);
				attributes.Add(attribute);
			}
		}
	
		public bool Write(ArraySegment<byte> segment, ref ushort count)
		{
			bool success = true;
			Array.Copy(BitConverter.GetBytes(this.id), 0, segment.Array, segment.Offset + count, sizeof(int));
			count += sizeof(int);
			Array.Copy(BitConverter.GetBytes(this.level), 0, segment.Array, segment.Offset + count, sizeof(short));
			count += sizeof(short);
			Array.Copy(BitConverter.GetBytes(this.duration), 0, segment.Array, segment.Offset + count, sizeof(float));
			count += sizeof(float);
			Array.Copy(BitConverter.GetBytes((ushort)this.attributes.Count), 0, segment.Array, segment.Offset + count, sizeof(ushort));
			count += sizeof(ushort);
			foreach (Attribute attribute in this.attributes)
				attribute.Write(segment, ref count);
			return success;
		}	
	}
	public List<Skill> skills = new List<Skill>();

	public ushort Protocol { get { return (ushort)PacketID.C_PlayerInfoReq; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		this.testByte = (sbyte)segment.Array[segment.Offset + count];
		count += sizeof(sbyte);
		this.playerId = BitConverter.ToInt64(segment.Array, segment.Offset + count);
		count += sizeof(long);
		ushort nameLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.name = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, nameLen);
		count += nameLen;
		this.skills.Clear();
		ushort skillLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		for (int i = 0; i < skillLen; i++)
		{
			Skill skill = new Skill();
			skill.Read(segment, ref count);
			skills.Add(skill);
		}
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_PlayerInfoReq), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		segment.Array[segment.Offset + count] = (byte)this.testByte;
		count += sizeof(sbyte);
		Array.Copy(BitConverter.GetBytes(this.playerId), 0, segment.Array, segment.Offset + count, sizeof(long));
		count += sizeof(long);
		ushort nameLen = (ushort)Encoding.Unicode.GetBytes(this.name, 0, this.name.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(nameLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += nameLen;
		Array.Copy(BitConverter.GetBytes((ushort)this.skills.Count), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		foreach (Skill skill in this.skills)
			skill.Write(segment, ref count);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class S_Test : IPacket
{
	public int testInt;

	public ushort Protocol { get { return (ushort)PacketID.S_Test; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		this.testInt = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_Test), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes(this.testInt), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}


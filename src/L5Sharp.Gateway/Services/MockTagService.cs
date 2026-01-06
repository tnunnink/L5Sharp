using System;
using System.Text;
using L5Sharp.Gateway.Abstractions;

namespace L5Sharp.Gateway.Services;

/// <summary>
/// A mock implementation of the <see cref="ITagService"/> interface used for testing or simulation purposes.
/// Provides various methods for managing and manipulating tag data, including creating, reading, writing,
/// and configuring attributes of tags.
/// </summary>
public class MockTagService : ITagService
{
    private int _tagId = 0;
    private readonly Func<int, object> _valueGenerator;

    private Action<int, int, int> _onEvent;
    private Action<int, int, int, IntPtr> _onEventEx;
    private IntPtr _userData = IntPtr.Zero;

    public MockTagService(Func<int, object> valueGenerator)
    {
        _valueGenerator = valueGenerator;
    }

    public int Abort(int tag)
    {
        throw new NotImplementedException();
    }

    public int CheckVersion(int major, int minor, int patch)
    {
        throw new NotImplementedException();
    }

    public int Create(string path, int timeout)
    {
        throw new NotImplementedException();
    }

    public int Create(string path, Action<int, int, int, IntPtr> callback, IntPtr userData, int timeout)
    {
        throw new NotImplementedException();
    }

    public string Decode(int error)
    {
        throw new NotImplementedException();
    }

    public int Read(int tag, int timeout)
    {
        throw new NotImplementedException();
    }

    public int Write(int tag, int timeout)
    {
        throw new NotImplementedException();
    }

    public int Status(int tag)
    {
        throw new NotImplementedException();
    }

    public int RegisterCallback(int tag, Action<int, int, int> callback)
    {
        throw new NotImplementedException();
    }

    public int UnregisterCallback(int tag)
    {
        throw new NotImplementedException();
    }

    public int RegisterLogger(Action<int, int, string> logger)
    {
        throw new NotImplementedException();
    }

    public int UnregisterLogger(int tag)
    {
        throw new NotImplementedException();
    }

    public int Lock(int tag)
    {
        throw new NotImplementedException();
    }

    public int Unlock(int tag)
    {
        throw new NotImplementedException();
    }

    public int Destroy(int tag)
    {
        throw new NotImplementedException();
    }

    public int Shutdown()
    {
        throw new NotImplementedException();
    }

    public void SetDebugLevel(int debugLevel)
    {
        throw new NotImplementedException();
    }

    public int GetAttribute(int tag, string attributeName, int defaultValue)
    {
        throw new NotImplementedException();
    }

    public int SetAttribute(int tag, string attributeName, int newValue)
    {
        throw new NotImplementedException();
    }

    public int GetByteArrayAttribute(int tag, string attributeName, byte[] buffer, int length)
    {
        throw new NotImplementedException();
    }

    public int GetSize(int tag)
    {
        throw new NotImplementedException();
    }

    public int SetSize(int tag, int size)
    {
        throw new NotImplementedException();
    }

    public int GetBit(int tag, int offSetBit)
    {
        throw new NotImplementedException();
    }

    public sbyte GetSByte(int tag, int offSet)
    {
        throw new NotImplementedException();
    }

    public short GetShort(int tag, int offSet)
    {
        throw new NotImplementedException();
    }

    public int GetInt(int tag, int offSet)
    {
        throw new NotImplementedException();
    }

    public long GetLong(int tag, int offSet)
    {
        throw new NotImplementedException();
    }

    public float GetFloat(int tag, int offSet)
    {
        throw new NotImplementedException();
    }

    public double GetDouble(int tag, int offSet)
    {
        throw new NotImplementedException();
    }

    public byte GetByte(int tag, int offSet)
    {
        throw new NotImplementedException();
    }

    public ushort GetUShort(int tag, int offSet)
    {
        throw new NotImplementedException();
    }

    public uint GetUInt(int tag, int offSet)
    {
        throw new NotImplementedException();
    }

    public ulong GetULong(int tag, int offSet)
    {
        throw new NotImplementedException();
    }

    public int GetRawBytes(int tag, int start, byte[] buffer, int length)
    {
        throw new NotImplementedException();
    }

    public int GetStringLength(int tag, int offset)
    {
        throw new NotImplementedException();
    }

    public int GetString(int tag, int offset, StringBuilder buffer, int length)
    {
        throw new NotImplementedException();
    }

    public int GetStringTotalLength(int tag, int offset)
    {
        throw new NotImplementedException();
    }

    public int GetStringCapacity(int tag, int offset)
    {
        throw new NotImplementedException();
    }

    public int SetBit(int tag, int offSetBit, int val)
    {
        throw new NotImplementedException();
    }

    public int SetSByte(int tag, int offSet, sbyte val)
    {
        throw new NotImplementedException();
    }

    public int SetShort(int tag, int offSet, short val)
    {
        throw new NotImplementedException();
    }

    public int SetInt(int tag, int offSet, int val)
    {
        throw new NotImplementedException();
    }

    public int SetLong(int tag, int offSet, long val)
    {
        throw new NotImplementedException();
    }

    public int SetFloat(int tag, int offSet, float val)
    {
        throw new NotImplementedException();
    }

    public int SetDouble(int tag, int offSet, double val)
    {
        throw new NotImplementedException();
    }

    public int SetByte(int tag, int offSet, byte val)
    {
        throw new NotImplementedException();
    }

    public int SetUShort(int tag, int offSet, ushort val)
    {
        throw new NotImplementedException();
    }

    public int SetUInt(int tag, int offSet, uint val)
    {
        throw new NotImplementedException();
    }

    public int SetULong(int tag, int offSet, ulong val)
    {
        throw new NotImplementedException();
    }

    public int SetRawBytes(int tag, int offset, byte[] buffer, int length)
    {
        throw new NotImplementedException();
    }

    public int SetString(int tag, int offset, string value)
    {
        throw new NotImplementedException();
    }
}
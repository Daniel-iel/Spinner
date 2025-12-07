using Spinner.Attribute;

namespace Spinner.Test.Models
{
    public class Int32Model
    {
        [ReadProperty(0, 10)]
        public int Value { get; set; }
    }

    public class Int64Model
    {
        [ReadProperty(0, 19)]
        public long Value { get; set; }
    }

    public class BooleanModel
    {
        [ReadProperty(0, 5)]
        public bool Value { get; set; }
    }

    public class ByteModel
    {
        [ReadProperty(0, 3)]
        public byte Value { get; set; }
    }

    public class SByteModel
    {
        [ReadProperty(0, 4)]
        public sbyte Value { get; set; }
    }

    public class Int16Model
    {
        [ReadProperty(0, 6)]
        public short Value { get; set; }
    }

    public class UInt16Model
    {
        [ReadProperty(0, 5)]
        public ushort Value { get; set; }
    }

    public class UInt32Model
    {
        [ReadProperty(0, 10)]
        public uint Value { get; set; }
    }

    public class UInt64Model
    {
        [ReadProperty(0, 20)]
        public ulong Value { get; set; }
    }

    public class SingleModel
    {
        [ReadProperty(0, 10)]
        public float Value { get; set; }
    }

    public class DoubleModel
    {
        [ReadProperty(0, 15)]
        public double Value { get; set; }
    }

    public class CharModel
    {
        [ReadProperty(0, 1)]
        public char Value { get; set; }
    }

    public class DateTimeModel
    {
        [ReadProperty(0, 19)]
        public System.DateTime Value { get; set; }
    }

    public class TimeSpanModel
    {
        [ReadProperty(0, 20)]
        public System.TimeSpan Value { get; set; }
    }

    public class NIntModel
    {
        [ReadProperty(0, 10)]
        public nint Value { get; set; }
    }

    public class NUIntModel
    {
        [ReadProperty(0, 10)]
        public nuint Value { get; set; }
    }

    public class StringModel
    {
        [ReadProperty(0, 20)]
        public string Value { get; set; }
    }

    public class StringModelWithInterceptor
    {
        [ReadProperty(0, 20, typeof(Helper.Interceptors.ObjectToStringInterceptor))]
        public string Value { get; set; }
    }
}

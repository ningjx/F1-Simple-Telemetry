using F1Tools.Models;
using System;

namespace F1Tools
{
    public class FH5
    {
        public static float[] GetFh5Data(byte[] bytes)
        {
            float[] data = new float[100];

            if (bytes.Length != 324)
                return data;

            int index = 0;
            int arrayIndex = 0;
            foreach (var item in FH5Format.FH5FormatData)
            {
                switch (item.Type)
                {
                    case "Int32":
                        data[arrayIndex] = BitConverter.ToInt32(bytes, index);
                        index += item.Size; arrayIndex++;
                        break;
                    case "float":
                        data[arrayIndex] = BitConverter.ToSingle(bytes, index);
                        index += item.Size; arrayIndex++;
                        break;
                    case "bool":
                        data[arrayIndex] = BitConverter.ToInt32(bytes, index);
                        index += item.Size; arrayIndex++;
                        break;
                    case "UInt16":
                        data[arrayIndex] = BitConverter.ToUInt16(bytes, index);
                        index += item.Size; arrayIndex++;
                        break;
                    case "uint8":
                        data[arrayIndex] = bytes[index];
                        index += item.Size; arrayIndex++;
                        break;
                    default: break;
                }
            }
            return data;
        }
    }
}

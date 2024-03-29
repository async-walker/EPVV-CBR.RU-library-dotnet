﻿using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace EPVV_CBR_RU.Extensions
{
    internal static class StreamExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? DeserializeJsonFromStream<T>(this Stream? stream)
        {
            if (stream is null || !stream.CanRead) { return default; }

            using var streamReader = new StreamReader(stream);
            using var jsonTextReader = new JsonTextReader(streamReader);

            var jsonSerializer = JsonSerializer.CreateDefault();
            var searchResult = jsonSerializer.Deserialize<T>(jsonTextReader);

            return searchResult;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ConvertToByteArray(this Stream stream)
        {
            byte[] bytes;

            using (var binaryReader = new BinaryReader(stream))
                bytes = binaryReader.ReadBytes((int)stream.Length);

            return bytes;
        }
    }
}

﻿//<snippet1>
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

public static class MemoryWriteReadExample
{
    private const string Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
    private static readonly byte[] s_messageBytes = Encoding.ASCII.GetBytes(Message);

    public static void Run()
    {
        Console.WriteLine($"The original string length is {s_messageBytes.Length} bytes.");
        using var stream = new MemoryStream();
        CompressBytesToStream(stream);
        Console.WriteLine($"The compressed stream length is {stream.Length} bytes.");
        int decompressedLength = DecompressStreamToBytes(stream);
        Console.WriteLine($"The decompressed string length is {decompressedLength} bytes, same as the original length.");
        /*
         Output:
            The original string length is 445 bytes.
            The compressed stream length is 265 bytes.
            The decompressed string length is 445 bytes, same as the original length.
        */
    }

    private static void CompressBytesToStream(Stream stream)
    {
        using var compressor = new DeflateStream(stream, CompressionMode.Compress, leaveOpen: true);
        compressor.Write(s_messageBytes, 0, s_messageBytes.Length);
    }

    private static int DecompressStreamToBytes(Stream stream)
    {
        stream.Position = 0;
        int bufferSize = 512;
        byte[] buffer = new byte[bufferSize];
        using var deflateStream = new DeflateStream(stream, CompressionMode.Decompress);

        int totalRead = 0;
        while (totalRead < buffer.Length)
        {
            int bytesRead = deflateStream.Read(buffer.AsSpan(totalRead));
            if (bytesRead == 0) break;
            totalRead += bytesRead;
        }

        return totalRead;
    }
}
//</snippet1>

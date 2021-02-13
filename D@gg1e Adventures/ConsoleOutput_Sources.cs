using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using EncodingsConverter;

namespace ConsoleOutputModule
{
    public static class ConsoleOutput
    {
        static CharInfo[] OutputBuffer;
        static SmallRectangle Rectangle;
        static SafeFileHandle Handle;
        static bool IsPrepared = false;
        static short Size_X;
        static short Size_Y;
        public class Cell
        {
            public byte Background_Color;
            public byte Foreground_Color;
            public char Symbol;
        }

        public static byte GetColor(ConsoleColor ConsoleColor) { return Convert.ToByte(ConsoleColor); }

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
        string fileName,
        [MarshalAs(UnmanagedType.U4)] uint fileAccess,
        [MarshalAs(UnmanagedType.U4)] uint fileShare,
        IntPtr securityAttributes,
        [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
        [MarshalAs(UnmanagedType.U4)] int flags,
        IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutput(
        SafeFileHandle hConsoleOutput,
        CharInfo[] lpOutputBufferfer,
        Coord dwOutputBufferferSize,
        Coord dwOutputBufferferCoord,
        ref SmallRectangle lpWriteRegion);

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            public short X;
            public short Y;

            public Coord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        [StructLayout(LayoutKind.Explicit)]
        public struct CharUnion
        {
            [FieldOffset(0)]
            public char UnicodeChar;
            [FieldOffset(0)]
            public byte AsciiChar;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CharInfo
        {
            [FieldOffset(0)]
            public CharUnion Char;
            [FieldOffset(2)]
            public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SmallRectangle
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        public static bool Prepare(short New_Size_X, short New_Size_Y)
        {
            if (New_Size_X == Size_X && New_Size_Y == Size_Y) return true;
            if (!IsPrepared || !Handle.IsInvalid)
            {
                //Console.SetWindowSize(New_Size_X, New_Size_Y);
                //Console.SetBufferSize(New_Size_X, New_Size_Y);
                Handle = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
                OutputBuffer = new CharInfo[New_Size_X * New_Size_Y];
                Rectangle = new SmallRectangle() { Left = 0, Top = 0, Right = New_Size_X, Bottom = New_Size_Y };
                if (OutputBuffer.Length == 5) IsPrepared = true;
                IsPrepared = true;
                Size_X = New_Size_X;
                Size_Y = New_Size_Y;
                return true;
            }
            else return false;
        }

        public static void WriteOutput(short New_Size_X, short New_Size_Y, Cell[,] NewBuffer)
        {
            if (Prepare(New_Size_X, New_Size_Y))
            {
                int Index = 0;
                for (int Y = 0; Y < Size_Y; Y++)
                    for (int X = 0; X < Size_X; X++)
                    {
                        OutputBuffer[Index].Attributes = Convert.ToInt16(NewBuffer[X, Y].Background_Color * 16 + NewBuffer[X, Y].Foreground_Color);
                        OutputBuffer[Index].Char.AsciiChar = ConvertEncodings.UnicodeToASCII(Convert.ToInt16(NewBuffer[X, Y].Symbol));
                        Index++;
                    }
                WriteConsoleOutput(Handle, OutputBuffer, new Coord() { X = New_Size_X, Y = New_Size_Y }, new Coord() { X = 0, Y = 0 }, ref Rectangle);
            }
        }
    }
}
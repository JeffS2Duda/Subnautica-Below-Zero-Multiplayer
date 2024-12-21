namespace Subnautica.API.Features.DiscordManager
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    using Subnautica.API.Features.DiscordManager.Exceptions;

    public class NamedPipeClientStream : System.IO.Stream
    {
        private IntPtr ptr;
        private bool _isDisposed;

        public override bool CanRead { get { return true; } }

        public override bool CanSeek { get { return false; } }

        public override bool CanWrite { get { return true; } }

        public override long Length { get { return 0; } }

        public override long Position { get { return 0; } set { } }

        public bool IsConnected { get { return Native.IsConnected(ptr); } }

        public string PipeName { get; }

        private static readonly string s_pipePrefix = Path.Combine(Path.GetTempPath(), "CoreFxPipe_");

        #region Constructors
        public NamedPipeClientStream(string server, string pipeName)
        {
            ptr = Native.CreateClient();
            PipeName = FormatPipe(server, pipeName);
            Console.WriteLine("Created new NamedPipeClientStream '{0}' => '{1}'", pipeName, PipeName);
        }

        ~NamedPipeClientStream()
        {
            Dispose(false);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!_isDisposed)
            {
                Disconnect();
                Native.DestroyClient(ptr);
                _isDisposed = true;
            }
        }

        private static string FormatPipe(string server, string pipeName)
        {
            switch (Environment.OSVersion.Platform)
            {
                default:
                case PlatformID.Win32NT:
                    return string.Format(@"\\{0}\pipe\{1}", server, pipeName);

                case PlatformID.Unix:
                    if (server != ".")
                        throw new PlatformNotSupportedException("Remote pipes are not supported on this platform.");

                    return s_pipePrefix + pipeName;
            }
        }
        #endregion

        #region Open Close
        public void Connect()
        {
            int code = Native.Open(ptr, PipeName);
            if (!IsConnected) throw new NamedPipeOpenException(code);

        }

        public void Disconnect()
        {
            Native.Close(ptr);
        }
        #endregion

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!IsConnected)
                throw new NamedPipeConnectionException("Cannot read stream as pipe is not connected");

            if (offset + count > buffer.Length)
                throw new ArgumentOutOfRangeException("count", "Cannot read as the count exceeds the buffer size");

            int bytesRead = 0;
            int size = Marshal.SizeOf(buffer[0]) * count;
            IntPtr buffptr = Marshal.AllocHGlobal(size);

            try
            {
                bytesRead = Native.ReadFrame(ptr, buffptr, count);
                if (bytesRead <= 0)
                {
                    if (bytesRead < 0)
                    {
                        throw new NamedPipeReadException(bytesRead);
                    }

                    return 0;
                }
                else
                {
                    Marshal.Copy(buffptr, buffer, offset, bytesRead);
                    return bytesRead;
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buffptr);
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (!IsConnected)
                throw new NamedPipeConnectionException("Cannot write stream as pipe is not connected");

            int size = Marshal.SizeOf(buffer[0]) * count;
            IntPtr buffptr = Marshal.AllocHGlobal(size);

            try
            {
                Marshal.Copy(buffer, offset, buffptr, count);

                int result = Native.WriteFrame(ptr, buffptr, count);
                if (result < 0) throw new NamedPipeWriteException(result);
            }
            finally
            {
                Marshal.FreeHGlobal(buffptr);
            }
        }

        #region unsupported

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        #endregion

        private static class Native
        {
            public const string LIBRARY_NAME = "DiscordRPCNativeNamedPipe.dll";

            #region Creation and Destruction
            [DllImport(LIBRARY_NAME, EntryPoint = "createClient", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr CreateClient();

            [DllImport(LIBRARY_NAME, EntryPoint = "destroyClient", CallingConvention = CallingConvention.Cdecl)]
            public static extern void DestroyClient(IntPtr client);
            #endregion

            #region State Control

            [DllImport(LIBRARY_NAME, EntryPoint = "isConnected", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern bool IsConnected([MarshalAs(UnmanagedType.SysInt)] IntPtr client);

            [DllImport(LIBRARY_NAME, EntryPoint = "open", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern int Open(IntPtr client, [MarshalAs(UnmanagedType.LPStr)] string pipename);

            [DllImport(LIBRARY_NAME, EntryPoint = "close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern void Close(IntPtr client);

            #endregion

            #region IO

            [DllImport(LIBRARY_NAME, EntryPoint = "readFrame", CallingConvention = CallingConvention.Cdecl)]
            public static extern int ReadFrame(IntPtr client, IntPtr buffer, int length);

            [DllImport(LIBRARY_NAME, EntryPoint = "writeFrame", CallingConvention = CallingConvention.Cdecl)]
            public static extern int WriteFrame(IntPtr client, IntPtr buffer, int length);

            #endregion
        }
    }
}

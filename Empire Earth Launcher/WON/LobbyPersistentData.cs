using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Empire_Earth_Launcher.WON
{
    /// <summary>
    /// LobbyGlobalData: Parsing fully done
    /// LobbyUserData: Only friends parsing done
    /// </summary>
    class LobbyPersistentData
    {
        private static ulong ReadFileSignature(Stream input)
        {
            if (input.Length < (6 + 4) || !ReadString(6, input).Equals("WONPER", StringComparison.InvariantCultureIgnoreCase))
                return 0;
            return ReadLong_x86(input);
        }

        private static string ReadString(int len, Stream input)
        {
            string result = null;

            for (int i = 0; i < len; ++i)
            {
                int read = input.ReadByte();
                result += (char)read;
            }
            return result;
        }

        private static string ReadWideString(Stream input)
        {
            int len = ReadShort(input);
            string result = null;

            for (int i = 0; i < len; ++i)
            {
                byte[] buff = new byte[2];
                buff[0] = (byte)input.ReadByte();
                buff[1] = (byte)input.ReadByte();
                // I don't think that very good
                result += (Encoding.UTF8.GetString(buff)[0]).ToString();
            }
            return result;
        }

        private static ushort ReadShort(Stream input)
        {
            byte[] result = new byte[2];

            for (int i = 0; i < 2; ++i)
                result[i] = (byte)input.ReadByte();

            return BitConverter.ToUInt16(result, 0);
        }

        private static ulong ReadLong_x86(Stream input)
        {
            byte[] result = new byte[4];

            for (int i = 0; i < 4; ++i)
                result[i] = (byte)input.ReadByte();

            return BitConverter.ToUInt32(result, 0);
        }

        public class LobbyGlobalData
        {
            private string path;
            public ulong FileSignature { get; private set; }

            public List<PlayerInfoGlobalData> PlayerInfoGlobalDatas { get; private set; }

            public class PlayerInfoGlobalData
            {
                public string Username { get; private set; }
                public ulong LastUse { get; private set; }
                public ushort FileID { get; private set; }

                public PlayerInfoGlobalData(Stream input)
                {
                    Username = ReadWideString(input);
                    LastUse = ReadLong_x86(input);
                    FileID = ReadShort(input);

                    ulong sysTOUTime = ReadLong_x86(input);
                    ulong gameTOUTime = ReadLong_x86(input);

                    // Fake read the password part, not very hard or even secure lol but not required anyway
                    ushort passlen = ReadShort(input);
                    ReadString(passlen, input);
                }
            }

            /// <summary>
            /// Will automatically parse the given lobby user file
            /// </summary>
            /// <param name="path">Path to the lobby user file (_wonlobbypersistent.dat)</param>
            public LobbyGlobalData(string path)
            {
                this.path = path;
                PlayerInfoGlobalDatas = new List<PlayerInfoGlobalData>();

                Reload();
            }

            public void Reload()
            {
                PlayerInfoGlobalDatas.Clear();
                using (Stream input = File.OpenRead(path))
                {
                    FileSignature = ReadFileSignature(input);

                    if (FileSignature == 0)
                        return;

                    ushort aNumUserNames = ReadShort(input);

                    for (int i = 0; i < aNumUserNames; ++i)
                    {
                        PlayerInfoGlobalDatas.Add(new PlayerInfoGlobalData(input));
                    }

                    bool lobbySoundEffects = input.ReadByte() != 0;
                    bool globalLobbyMusic = input.ReadByte() != 0;
                    ulong networkAdapter = ReadLong_x86(input);
                }
            }
        }

        public class LobbyUserData
        {
            private string path;

            public ulong FileSignature { get; private set; }

            /// <summary>
            /// Dictionary of Friends, key is the player name and value is it's WON ID
            /// </summary>
            public IDictionary<string, int> Friends { get; private set; }

            /// <summary>
            /// Will automatically parse the given lobby user file
            /// </summary>
            /// <param name="path">Path to the lobby user file (_wonuser.dat)</param>
            public LobbyUserData(string path)
            {
                this.path = path;
                this.Friends = new Dictionary<string, int>();

                Reload();
            }

            public void Reload()
            {
                Friends.Clear();
                using (Stream input = File.OpenRead(path))
                {
                    FileSignature = ReadFileSignature(input);

                    if (FileSignature == 0)
                        return;

                    ushort numReconnectIds = ReadShort(input);
                    for (int i = 0; i < numReconnectIds; ++i)
                    {
                        ReadString(6, input);

                        ReadLong_x86(input);
                        ReadLong_x86(input);
                    }

                    ushort numIgnored = ReadShort(input);
                    for (int i = 0; i < numIgnored; ++i)
                        ReadWideString(input);

                    ushort numFriend = ReadShort(input);
                    for (int i = 0; i < numFriend; ++i)
                    {
                        string name = ReadWideString(input);
                        ulong won_id = ReadLong_x86(input);
                        Friends.Add(name, (int)won_id);
                    }
                }
            }
        }
    }
}

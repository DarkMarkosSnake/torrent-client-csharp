using System;
using System.Collections.Generic;
using BencodeNET.Torrents;
using BencodeNET.Parsing;

namespace TorrentClient
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Parse torrent by specifying the file path
            var parser = new BencodeParser(); // Default encoding is Encoding.UTF8, but you can specify another if you need to
            var torrent = parser.Parse<Torrent>("C:\\debian.torrent");

            var torrentFile = new TorrentFile(
                new List<byte>(torrent.OriginalInfoHashBytes),
                new List<byte>(torrent.Pieces),
                torrent.PieceSize,
                torrent.TotalSize,
                torrent.File.FileName
            );
        }
    }
}

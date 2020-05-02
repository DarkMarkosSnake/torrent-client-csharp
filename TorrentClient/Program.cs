using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web;
using BencodeNET.Objects;
using BencodeNET.Torrents;
using BencodeNET.Parsing;

namespace TorrentClient
{
    internal static class Program
    {
        static readonly HttpClient Client = new HttpClient();
        
        private static void Main(string[] args)
        {
            // Parse torrent by specifying the file path
            var parser = new BencodeParser(); // Default encoding is Encoding.UTF8, but you can specify another if you need to
            var torrent = parser.Parse<Torrent>("C:\\ubuntu.torrent");

            var torrentFile = new TorrentFile(
                torrent.Trackers[0][0],
                new List<byte>(torrent.OriginalInfoHashBytes),
                new List<byte>(torrent.Pieces),
                torrent.PieceSize,
                torrent.TotalSize
            );

            var queryStringCollection = HttpUtility.ParseQueryString(string.Empty);
            
            var rnd = new Random();
            var b = new byte[20];
            rnd.NextBytes(b);
            
            var bString = new BString("E2467CBF021192C241367B892230DC1E05C0580E");
            
            SHA1Managed sha1 = new SHA1Managed();

            var computedSha1 = sha1.ComputeHash(bString.EncodeAsBytes());
            
            queryStringCollection.Add("info_hash", System.Text.Encoding.ASCII.GetString(computedSha1));
            // queryStringCollection.Add("peer_id", System.Text.Encoding.ASCII.GetString(b));
            queryStringCollection.Add("peer_id", "-UT3000-%ced%f6_%df%ba%d2%b5Q%e7%14%7d");
            queryStringCollection.Add("port", "6881");
            queryStringCollection.Add("uploaded", "0");
            queryStringCollection.Add("downloaded", "0");
            queryStringCollection.Add("compact", "1");
            queryStringCollection.Add("left", torrentFile.TotalSize.ToString());
            queryStringCollection.Add("no_peer_id", "1");

            var uriBuilder = new UriBuilder(torrentFile.Announce) {Query = queryStringCollection.ToString()};

            var responseTask = Client.GetStringAsync(uriBuilder.ToString());
            var responseString = responseTask.Result;
        }
    }
}

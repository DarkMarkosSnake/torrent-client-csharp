using System.Collections.Generic;

namespace TorrentClient
{
    public class TorrentFile
    {
        public List<byte> InfoHash { get; }
        public List<byte> Pieces { get; }
        public long PieceSize { get; }
        public long TotalSize { get; }
        public string Name { get; }

        public TorrentFile(List<byte> infoHash, List<byte> pieces, long pieceSize, long totalSize, string name)
        {
            InfoHash = infoHash;
            Pieces = pieces;
            PieceSize = pieceSize;
            TotalSize = totalSize;
            Name = name;
        }
    }
}
using System.Drawing;
using System.IO;

namespace Gravatar
{
    internal interface IImageCache
    {
        void CacheImage(string imageFileName, Stream imageStream);

        void ClearCache();

        void DeleteCachedFile(string imageFileName);

        bool FileIsCached(string imageFileName);

        bool FileIsExpired(string imageFileName, int cacheDays);

        Image LoadImageFromCache(string imageFileName, Bitmap defaultBitmap);
    }
}

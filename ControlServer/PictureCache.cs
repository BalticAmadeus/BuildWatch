using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace BuildWatch.ControlServer
{
    public class PictureCache
    {
        private string _pictureDir;
        private Dictionary<string, PictureCacheEntry> _byUser;

        public PictureCache(string pictureDir)
        {
            _pictureDir = pictureDir;
            _byUser = new Dictionary<string, PictureCacheEntry>();
        }

        public PictureCacheEntry GetForUser(string user)
        {
            PictureCacheEntry entry;
            if (_byUser.TryGetValue(user, out entry))
                validate(ref entry);
            if (entry != null)
                return entry;
            entry = loadForUser(user);
            putToCache(entry);
            return entry;
        }

        private void validate(ref PictureCacheEntry entry)
        {
            DateTime timeout = DateTime.Now.AddMinutes(-5);
            if (entry.Timestamp > timeout)
                return;
            removeFromCache(entry);
            entry = null;
        }

        private void putToCache(PictureCacheEntry entry)
        {
            _byUser[entry.User] = entry;
        }

        private void removeFromCache(PictureCacheEntry entry)
        {
            _byUser.Remove(entry.User);
        }

        private PictureCacheEntry loadForUser(string user)
        {
            string fileNameBase = Path.Combine(_pictureDir, user);
            foreach (string extension in new string[] { ".jpg", ".png" })
            {
                string fileName = fileNameBase + extension;
                try
                {
                    byte[] data = File.ReadAllBytes(fileName);
                    return new PictureCacheEntry(user, data);
                }
                catch (Exception)
                {
                    // silence the exception
                    continue;
                }
            }
            return new PictureCacheEntry(user, null);
        }
    }

    public class PictureCacheEntry
    {
        public string User { get; private set; }
        public string Hash { get; private set; }
        public byte[] Data { get; private set; }
        public DateTime Timestamp { get; private set; }

        public PictureCacheEntry(string user, byte[] data)
        {
            User = user;
            Hash = GetDataHash(data);
            Data = data;
            Timestamp = DateTime.Now;
        }

        public static string GetDataHash(byte[] data)
        {
            if (data == null || data.Length == 0)
                return "";
            using (SHA1 md = SHA1.Create())
            {
                byte[] hash = md.ComputeHash(data);
                return BitConverter.ToString(data);
            }
        }
    }

}
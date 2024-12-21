namespace Subnautica.API.Features.Helper
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using Subnautica.API.Extensions;

    public class ApiDataFormat
    {
        public bool IsStatus { get; set; }
        
        public bool IsPreRelease { get; set; }

        public string Version { get; set; }

        public List<ApiDataAssetsFormat> Assets { get; set; } = new List<ApiDataAssetsFormat>();

        public List<ApiDataDownloadItem> Downloads { get; set; } = new List<ApiDataDownloadItem>();

        public List<string> Languages { get; set; } = new List<string>();

        public double GetTotalFileSize()
        {
            long totalFileSize = 0;

            foreach (var item in this.Downloads)
            {
                totalFileSize += item.FileSize;
            }

            return (double)totalFileSize;
        }

        public List<ApiDataDownloadItem> GetDownloadFiles()
        {
            var files = new List<ApiDataDownloadItem>();

            foreach (var item in this.Downloads)
            {
                files.Add(this.GetDownloadItem(item.TempName, item.LocalPath, item.RemoteUrl, item.FileSize, item.CheckVersion, item.CustomVersion));
            }

            return files;
        }

        private ApiDataDownloadItem GetDownloadItem(string tempname, string localPath, string remoteUrl, long fileSize, bool checkVersion, string customVersion)
        {
            return new ApiDataDownloadItem()
            {
                TempName      = tempname,
                LocalPath     = string.Format("{0}{1}{2}", localPath, localPath.IsNull() ? "" : Paths.DS.ToString(), tempname.IsNotNull() ? tempname : Path.GetFileName(remoteUrl)),
                RemoteUrl     = remoteUrl,
                FileSize      = fileSize,
                CheckVersion  = checkVersion,
                CustomVersion = customVersion,
            };
        }
    }

    public class ApiDataAssetsFormat
    {
        public string Path { get; set; }

        public string Url { get; set; }
    }

    public class ApiDataDownloadItem
    {
        public string TempName { get; set; }

        public bool CheckVersion { get; set; }

        public string CurrentVersion { get; set; }

        public string CustomVersion { get; set; }

        public string LocalPath { get; set; }

        public string RemoteUrl { get; set; }

        public long FileSize { get; set; }
    }
}
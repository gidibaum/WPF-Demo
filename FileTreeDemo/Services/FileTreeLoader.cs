using System.Collections.Generic;
using System.IO;
using System.Linq;
using Base;
using FileTreeDemo.Models;

namespace FileTreeDemo.Services
{
    public class FileTreeLoader
    {
        static FileTreeLoader _Instance;

        public static FileTreeLoader Instance => _Instance ?? (_Instance = new FileTreeLoader());

        FileTreeLoader()
        {            
        }

        public DirItem Load(string rootPath)
        {
            var dir = new DirectoryInfo(rootPath);

            var root = new DirItem {Path = rootPath};

            LoadRecursive(root, dir);

            return root;
        }

        void LoadRecursive(DirItem dirItem, DirectoryInfo dir)
        {
            dirItem.Items.AddRange(GetFiles(dir));

            foreach (var currentDir in dir.EnumerateDirectories())
            {
                var childdir = new DirItem
                {
                    Name = currentDir.Name,
                    Path = currentDir.FullName
                };

                dirItem.Items.Add(childdir);

                LoadRecursive(childdir, currentDir);
            }
        }

        IEnumerable<FileItem> GetFiles(DirectoryInfo dir)
        {
            return dir.EnumerateFiles().Select(f => new FileItem
            {
                Name = f.Name,
                Path = f.Directory?.FullName,
                Extension = f.Extension
            });
        }
    }
}

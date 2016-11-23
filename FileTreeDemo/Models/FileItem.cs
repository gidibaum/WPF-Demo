
using System.Collections.ObjectModel;
using Base;

namespace FileTreeDemo.Models
{
    public abstract class FileTreeItem : ObservableObject
    {
        #region Property: Name

        string _Name;

        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }

        #endregion

        #region Property: Path

        string _Path;

        public string Path
        {
            get { return _Path; }
            set { SetProperty(ref _Path, value); }
        }

        #endregion

        public virtual string FullPath => $@"{Path}/{Name}";
        
        #region Equals

        public static bool operator ==(FileTreeItem a, FileTreeItem b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b)) return true;

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) return false;

            // Return true if the fields match:
            return a.Equals(b);
        }


        public static bool operator !=(FileTreeItem a, FileTreeItem b) => !(a == b);


        public override bool Equals(object obj)
        {
            var exp = obj as FileTreeItem;
            if ((object)exp == null) return false;

            return Equals(exp);
        }

        public virtual bool Equals(FileTreeItem other) => FullPath == other.FullPath;

        public override int GetHashCode() => FullPath.GetHashCode();

        #endregion
    }

    public class FileItem : FileTreeItem
    {
        public override string ToString() => $"File: {Name}";

        #region Property: Extension

        string _Extension;

        public string Extension
        {
            get { return _Extension; }
            set { SetProperty(ref _Extension, value); }
        }

        #endregion
    }

    public class DirItem : FileTreeItem
    {
        public ObservableCollection<FileTreeItem> Items { get; }

        public DirItem()
        {
            Items = new ObservableCollection<FileTreeItem>();
        }

        public override string ToString() => $"Dir: {Name}";
    }
}

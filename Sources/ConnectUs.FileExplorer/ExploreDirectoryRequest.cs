namespace ConnectUs.FileExplorer
{
    public class ExploreDirectoryRequest
    {
        public string Name { get { return GetType().Name; } set{}}
        public string DirectoryPath { get; set; }
        public bool GetFiles { get; set; }
        public bool GetDirectories { get; set; }
    }
}

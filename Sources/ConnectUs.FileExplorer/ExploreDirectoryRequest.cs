namespace ConnectUs.FileExplorer
{
    public class ExploreDirectoryRequest
    {
        public string DirectoryPath { get; set; }
        public bool GetFiles { get; set; }
        public bool GetDirectories { get; set; }
    }
}

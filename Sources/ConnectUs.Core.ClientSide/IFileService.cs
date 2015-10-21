namespace ConnectUs.Core.ClientSide
{
    public interface IFileService
    {
        void Copy(string sourceFileName, string destinationFileName);
        string GenerateRandomFileName();
    }
}
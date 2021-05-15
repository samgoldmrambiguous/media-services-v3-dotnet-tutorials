namespace UploadEncodeAndStreamFiles
{
    using Microsoft.Azure.Management.Media;
    using Microsoft.Azure.Management.Media.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class AssetsContainer
    {
        public async static Task<Uri> GetUrl(IAzureMediaServicesClient client, AzureMediaServiceConfig config, string assetName = "")
        {
            // Use Media Services API to get back a response that contains
            // SAS URL for the Asset container into which to upload blobs.
            // That is where you would specify read-write permissions 
            // and the exparation time for the SAS URL.
            var response = await client.Assets.ListContainerSasAsync(
                config.ResourceGroup,
                config.AccountName,
                assetName == string.Empty ? "moon" : assetName,
                permissions: AssetContainerPermission.ReadWrite,
                expiryTime: DateTime.UtcNow.AddHours(4).ToUniversalTime());

            return new Uri(response.AssetContainerSasUrls.First());
        }
    }
}

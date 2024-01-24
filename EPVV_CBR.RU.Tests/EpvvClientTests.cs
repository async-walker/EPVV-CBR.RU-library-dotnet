using EPVV_CBR_RU;
using EPVV_CBR_RU.Requests.Methods.DeleteMessages;
using EPVV_CBR_RU.Requests.Methods.GetReferenceInfo;
using EPVV_CBR_RU.Types;
using EPVV_CBR_RU.Types.Enums;
using Microsoft.Extensions.Configuration;

namespace EPVV_Client.Tests
{
    public class EpvvClientTests
    {
        const string DownloadPath = @"C:\EPVV_Lib";

        IEpvvClient _epvvClient = default!;

        public EpvvClientTests() => SetEpvvClient(true);

        private void SetEpvvClient(bool useTestPortal)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var clientSection = configuration.GetSection("EPVV_Client");

            var optionsSection = useTestPortal switch
            {
                true => clientSection.GetSection("TestPortalCreds"),
                false => clientSection.GetSection("PortalCreds"),
            };

            var username = optionsSection.GetValue<string>("Username")!;
            var password = optionsSection.GetValue<string>("Password")!;

            var options = new EpvvClientOptions(username, password, useTestPortal);

            _epvvClient = new EpvvClient(options);
        }

        [Fact]
        public async void MakeRequestAsync()
        {
            var message = await _epvvClient.CreateDraftMessageAsync(
                task: "Zadacha_137",
                title: "TITLE_EPVVV_CLIENT_TESTS",
                text: "TEXT",
                files: [new DirectedFile(
                    name: "name.zip.enc",
                    fileType: FileType.Document,
                    encrypted: true,
                    size: 1000)]);

            await _epvvClient.MakeRequestAsync(
                new DeleteMessageRequest(message.Id));
        }

        [Fact]
        public async void MakeRequestAsyncWithResponse()
        {
            var request = await _epvvClient.MakeRequestAsync(
                new GetProfileInfoRequest());

            Assert.NotNull(request);
        }
    }
}

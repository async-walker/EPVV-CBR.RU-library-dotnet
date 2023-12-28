using EPVV_CBR_RU;
using EPVV_CBR_RU.Enums;
using EPVV_CBR_RU.Models;
using Microsoft.Extensions.Configuration;

namespace EPVV_Client.Tests
{
    public class EpvvClientExtensions_ApiMethodsTests
    {
        private readonly IEpvvClient _epvvClient;

        public EpvvClientExtensions_ApiMethodsTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var clientSection = configuration.GetSection("EPVV_Client");

            var testPortal = true;

            var optionsSection = testPortal switch
            {
                true => clientSection.GetSection("TestPortalCreds"),
                false => clientSection.GetSection("PortalCreds"),
            };

            var username = optionsSection.GetValue<string>("Username")!;
            var password = optionsSection.GetValue<string>("Password")!;

            var options = new EpvvClientOptions(username, password, testPortal);

            _epvvClient = new EpvvClient(options);
        }

        [Fact]
        public async void CreateAndConfirmSendMessage()
        {
            var encFileInfo = new FileInfo(@"N:\ЗАДАЧИ\KYC\2023-10-02.zip.enc");
            var sigFileInfo = new FileInfo(@"N:\ЗАДАЧИ\KYC\2023-10-02.zip.sig");

            var draftMessage = await _epvvClient.CreateDraftMessageAsync(
                task: "Zadacha_137",
                title: "TEST_APIMETHOD_137TASK",
                text: "TEXT_TEST_APIMETHOD",
                files:
                [
                    new(name: encFileInfo.Name,
                        fileType: FileType.Document,
                        encrypted: true,
                        size: encFileInfo.Length),
                    new(name: sigFileInfo.Name,
                        fileType: FileType.Sign,
                        encrypted: false,
                        size: sigFileInfo.Length,
                        signedFile: encFileInfo.Name)
                ]);

            foreach (var file in draftMessage.Files)
            {
                var sessionInfo = await _epvvClient.CreateUploadSessionAsync(draftMessage.Id, file.Id);

                var uploadedFile = await _epvvClient.UploadFileAsync(sessionInfo, @"N:\ЗАДАЧИ\KYC\2023-10-02.zip.enc");
            }
        }

        [Fact]
        public async void CreateDraftMessage()
        {
            var encFileInfo = new FileInfo(@"N:\ЗАДАЧИ\KYC\2023-10-02.zip.enc");
            var sigFileInfo = new FileInfo(@"N:\ЗАДАЧИ\KYC\2023-10-02.zip.sig");

            var responseMessage = await _epvvClient.CreateDraftMessageAsync(
                task: "Zadacha_137",
                title: "TEST_APIMETHOD_137TASK",
                text: "TEXT_TEST_APIMETHOD",
                files:
                [
                    new(name: encFileInfo.Name,
                        fileType: FileType.Document,
                        encrypted: true,
                        size: encFileInfo.Length),
                    new(name: sigFileInfo.Name,
                        fileType: FileType.Sign,
                        encrypted: false,
                        size: sigFileInfo.Length,
                        signedFile: encFileInfo.Name)
                ]);

            Assert.True(responseMessage.Status is MessageStatus.draft);
        }

        [Fact]
        public async void GetMessagesInfoByFilters()
        {
            var queryFilters = new QueryFilters();

            var messages = await _epvvClient.GetMessagesInfoByFiltersAsync(queryFilters);

            Assert.True(messages.Any());
        }

        [Fact]
        public async void GetMessageInfoById()
        {
            var messageInfo = await _epvvClient.GetMessageInfoByIdAsync("3d3b52bd-1529-434d-a8a7-b0e000af9a03");

            Assert.NotNull(messageInfo);
        }
    }
}

using EPVV_CBR_RU;
using EPVV_CBR_RU.Enums;
using EPVV_CBR_RU.Exceptions;
using EPVV_CBR_RU.Models;
using Microsoft.Extensions.Configuration;

namespace EPVV_Client.Tests
{
    public class EpvvClientExtensions_ApiMethodsTests
    {
        private IEpvvClient _epvvClient = default!;

        public EpvvClientExtensions_ApiMethodsTests() 
            => SetEpvvClient();

        private void SetEpvvClient(bool useTestPortal = true)
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
        public async void CreateThenUploadAndConfirmSendMessage()
        {
            var encFileInfo = new FileInfo("2024-01-09.zip.enc");
            var sigFileInfo = new FileInfo("2024-01-09.zip.sig");

            var draftMessage = await _epvvClient.CreateDraftMessageAsync(
                task: "Zadacha_137",
                title: "TITLE_TEST_APIMETHOD",
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

                var stream = new FileStream(path: file.Name, FileMode.Open);

                var uploadedFile = await _epvvClient.UploadFileAsync(sessionInfo, stream);
            }

            await _epvvClient.ConfirmSendMessage(draftMessage.Id);
        }

        [Theory]
        [InlineData(true)]
        [InlineData]
        [InlineData(false, MessageType.outbox, MessageStatus.registered)]
        public async void GetMessagesInfoByFilters(
            bool testPortal = false,
            MessageType? messageType = default,
            MessageStatus? messageStatus = default)
        {
            SetEpvvClient(testPortal);

            var queryFilters = new QueryFilters(
                type: messageType,
                status: messageStatus);

            var messages = await _epvvClient.GetMessagesInfoByFiltersAsync(queryFilters);

            Assert.True(messages.Count != 0);
        }

        [Theory]
        [InlineData("e19657da-0c0b-4d89-80ac-b0f20103f891")]
        [InlineData("a054c858-bff1-412f-bced-b0f3004f3fd2", true)]
        public async void GetMessageInfoById(
            string messageId, 
            bool testPortal = false)
        {
            SetEpvvClient(testPortal);

            var messageInfo = await _epvvClient.GetMessageInfoByIdAsync(messageId);

            Assert.NotNull(messageInfo);
        }

        [Theory]
        [InlineData("f30cbf11-8650-4883-be48-b5f30057d0bf", "MESSAGE_NOT_FOUND")] // Несуществующее сообщение с данным ID
        [InlineData("f30cbf11-8650-4883-be48-b5f30057d0bf", "MESSAGE_NOT_FOUND", true)]
        public async void ExceptionMessageNotFoundGetMessageInfoById(
            string messageId, 
            string exceptedErrorCode, 
            bool testPortal = false)
        {
            SetEpvvClient(testPortal);

            try
            {
                await _epvvClient.GetMessageInfoByIdAsync(messageId);
            }
            catch (ApiRequestException ex)
            {
                Assert.Equal(exceptedErrorCode, ex.Code);
            }
        }

        [Fact]
        public async void DownloadMessage()
        {
            var path = await _epvvClient.DownloadMessageAsync("93b896c6-8400-4b7f-9c31-b0f30077757b", @"C:\Service");

            Assert.True(File.Exists(path));
        }
    }
}

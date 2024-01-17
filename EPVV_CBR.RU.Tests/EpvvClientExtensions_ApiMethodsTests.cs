using EPVV_CBR_RU;
using EPVV_CBR_RU.Exceptions;
using EPVV_CBR_RU.Types;
using EPVV_CBR_RU.Types.Enums;
using EPVV_CBR_RU.Types.Responses;
using Microsoft.Extensions.Configuration;

namespace EPVV_Client.Tests
{
    public class EpvvClientExtensions_ApiMethodsTests
    {
        const string DownloadPath = @"C:\EPVV_Lib";
        IEpvvClient _epvvClient = default!;

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
        public async Task<DraftMessage> CreateDraftMessage()
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

            return draftMessage;
        }

        [Fact]
        public async void Create_Then_Upload_And_Confirm_Send_Message()
        {
            var draftMessage = await CreateDraftMessage();

            foreach (var file in draftMessage.Files)
            {
                var sessionInfo = await _epvvClient.CreateUploadSessionAsync(draftMessage.Id, file.Id);

                var stream = new FileStream(path: file.Name, FileMode.Open);

                await _epvvClient.UploadFileAsync(sessionInfo, stream);
            }

            await _epvvClient.ConfirmSendMessage(draftMessage.Id);
        }

        [Theory]
        [InlineData(true, default, default)]
        [InlineData(false, default, default)]
        [InlineData(false, MessageType.outbox, MessageStatus.registered)]
        public async void GetMessagesInfoByFilters(
            bool testPortal,
            MessageType? messageType,
            MessageStatus? messageStatus)
        {
            SetEpvvClient(testPortal);

            var queryFilters = new QueryFilters(
                type: messageType,
                status: messageStatus);

            var messages = await _epvvClient.GetMessagesInfoByFiltersAsync(queryFilters);

            Assert.True(messages.Count != 0);
        }

        [Theory]
        [InlineData("a054c858-bff1-412f-bced-b0f3004f3fd2", true)]
        [InlineData("6238594a-6115-4c7a-9cea-b0f800726fa9", false)]
        public async void GetMessageInfoById(string messageId, bool testPortal)
        {
            SetEpvvClient(testPortal);

            var messageInfo = await _epvvClient.GetMessageInfoByIdAsync(messageId);

            Assert.NotNull(messageInfo);
        }

        [Theory]
        [InlineData("f30cbf11-8650-4883-be48-b5f30057d0bf", "MESSAGE_NOT_FOUND", true)]
        [InlineData("f30cbf11-8650-4883-be48-b5f30057d0bf", "MESSAGE_NOT_FOUND", false)]
        public async void Exception_Message_Not_Found_Get_Message_Info_By_Id(
            string messageId, 
            string exceptedErrorCode, 
            bool testPortal)
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
            var path = await _epvvClient.DownloadMessageAsync(
                messageId: "93b896c6-8400-4b7f-9c31-b0f30077757b",
                directoryToSave: DownloadPath);

            Assert.True(File.Exists(path));
        }

        [Fact]
        public async void GetMessageFileInfo()
        {
            var file = await _epvvClient.GetMessageFileInfoAsync(
                messageId: "a054c858-bff1-412f-bced-b0f3004f3fd2",
                fileId: "854146e7-b560-4bd8-9097-b0f3004f3fcb");

            Assert.NotNull(file);
        }

        [Fact]
        public async void DownloadFile()
        {
            var path = await _epvvClient.DownloadFileFromMessageAsync(
                messageId: "a054c858-bff1-412f-bced-b0f3004f3fd2",
                fileId: "854146e7-b560-4bd8-9097-b0f3004f3fcb",
                directoryToSave: DownloadPath);

            Assert.True(File.Exists(path));
        }

        [Theory]
        [InlineData("a054c858-bff1-412f-bced-b0f3004f3fd2", true)]
        [InlineData("6238594a-6115-4c7a-9cea-b0f800726fa9", false)]
        public async void GetReceipts(string messageId, bool testPortal)
        {
            SetEpvvClient(testPortal);

            var receipts = await _epvvClient.GetReceiptsInfoAsync(messageId);

            Assert.NotNull(receipts);
        }

        [Fact]
        public async void GetReceiptById()
        {
            var receipt = await _epvvClient.GetReceiptByIdAsync(
                messageId: "a054c858-bff1-412f-bced-b0f3004f3fd2",
                receiptId: "729de497-be3f-438a-8296-b0f3004f9802");

            Assert.NotNull(receipt);
        }

        [Fact]
        public async void GetReceiptFileInfo()
        {
            var receiptFileInfo = await _epvvClient.GetReceiptFileInfoAsync(
                messageId: "a054c858-bff1-412f-bced-b0f3004f3fd2",
                receiptId: "9c3c35ff-a6a6-48ff-86fa-b0f3004fa540",
                fileId: "b1b0e4f1-2c77-4d5e-9310-18e39cb47190");

            Assert.NotNull(receiptFileInfo);
        }

        [Fact]
        public async void DownloadReceipt()
        {
            var path = await _epvvClient.DownloadReceiptAsync(
                messageId: "a054c858-bff1-412f-bced-b0f3004f3fd2",
                receiptId: "9c3c35ff-a6a6-48ff-86fa-b0f3004fa540",
                fileId: "b1b0e4f1-2c77-4d5e-9310-18e39cb47190",
                directoryToSave: DownloadPath);

            Assert.True(File.Exists(path));
        }

        [Fact]
        public async void Create_And_Then_Delete_Message()
        {
            var message = await CreateDraftMessage();

            await _epvvClient.DeleteMessageByIdAsync(message.Id);
        }

        [Fact]
        public async void Create_Message_Then_Create_Upload_Session_And_Delete_Session()
        {
            var message = await CreateDraftMessage();

            var messageId = message.Id;
            var fileId = message.Files[0].Id;

            await _epvvClient.CreateUploadSessionAsync(messageId, fileId);
            await _epvvClient.DeleteFileOrSessionAsync(messageId, fileId);
        }
    }
}

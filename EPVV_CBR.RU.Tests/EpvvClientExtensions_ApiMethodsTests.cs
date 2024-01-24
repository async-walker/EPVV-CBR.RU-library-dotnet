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

        public EpvvClientExtensions_ApiMethodsTests() => SetEpvvClient(true);

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
        public async void Create_ThenUpload_AndConfirmSendMessage()
        {
            var draftMessage = await CreateDraftMessage();

            foreach (var file in draftMessage.Files)
            {
                var sessionInfo = await _epvvClient.CreateUploadSessionAsync(draftMessage.Id, file.Id);

                var stream = new FileStream(path: file.Name, FileMode.Open);

                await _epvvClient.UploadFileAsync(sessionInfo, stream);
            }

            await _epvvClient.ConfirmSendMessageAsync(draftMessage.Id);
        }

        [Theory]
        [InlineData(default, default, true)]
        [InlineData(default, default, false)]
        [InlineData(MessageType.outbox, MessageStatus.registered, false)]
        public async void GetMessagesInfoByFilters(
            MessageType? messageType,
            MessageStatus? messageStatus,
            bool testPortal)
        {
            SetEpvvClient(testPortal);

            var queryFilters = new QueryFilters(
                type: messageType,
                status: messageStatus);

            var messages = await _epvvClient.GetMessagesInfoByFiltersAsync(queryFilters);

            Assert.True(messages.Count != 0);
        }

        [Theory]
        [InlineData("79cd449b-f113-4826-95f6-b0fc00a3e325", true)]
        [InlineData("3bcaab4f-ecfb-40de-95d1-b0ff00ce3171", false)]
        public async void GetMessageInfoById(string messageId, bool testPortal)
        {
            SetEpvvClient(testPortal);

            var messageInfo = await _epvvClient.GetMessageInfoByIdAsync(messageId);

            Assert.NotNull(messageInfo);
        }

        [Theory]
        [InlineData("f30cbf11-8650-4883-be48-b5f30057d0bf", "MESSAGE_NOT_FOUND", true)]
        [InlineData("f30cbf11-8650-4883-be48-b5f30057d0bf", "MESSAGE_NOT_FOUND", false)]
        public async void ExceptionMessageNotFound_WhenGetMessageInfoById(
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

        [Theory]
        [InlineData("a054c858-bff1-412f-bced-b0f3004f3fd2", "729de497-be3f-438a-8296-b0f3004f9802", true)]
        [InlineData("6238594a-6115-4c7a-9cea-b0f800726fa9", "018ac453-a904-4652-af20-b0f800728ea6", false)]
        public async void GetReceiptById(
            string messageId, 
            string receiptId, 
            bool testPortal)
        {
            SetEpvvClient(testPortal);

            var receipt = await _epvvClient.GetReceiptByIdAsync(messageId, receiptId);

            Assert.NotNull(receipt);
        }

        [Theory]
        [InlineData("a054c858-bff1-412f-bced-b0f3004f3fd2", "9c3c35ff-a6a6-48ff-86fa-b0f3004fa540", "b1b0e4f1-2c77-4d5e-9310-18e39cb47190", true)]
        [InlineData("6238594a-6115-4c7a-9cea-b0f800726fa9", "018ac453-a904-4652-af20-b0f800728ea6", "ccc20bef-7aa5-4b27-a225-5f2c910df4ee", false)]
        public async void GetReceiptFileInfo(
            string messageId,
            string receiptId,
            string fileId,
            bool testPortal)
        {
            SetEpvvClient(testPortal);

            var receiptFileInfo = await _epvvClient.GetReceiptFileInfoAsync(messageId, receiptId, fileId);

            Assert.NotNull(receiptFileInfo);
        }

        [Theory]
        [InlineData("a054c858-bff1-412f-bced-b0f3004f3fd2", "9c3c35ff-a6a6-48ff-86fa-b0f3004fa540", "b1b0e4f1-2c77-4d5e-9310-18e39cb47190", true)]
        [InlineData("6238594a-6115-4c7a-9cea-b0f800726fa9", "018ac453-a904-4652-af20-b0f800728ea6", "ccc20bef-7aa5-4b27-a225-5f2c910df4ee", false)]
        public async void DownloadReceipt(
            string messageId, 
            string receiptId, 
            string fileId, 
            bool testPortal)
        {
            SetEpvvClient(testPortal);

            var directoryToSave = DownloadPath;
            var path = await _epvvClient.DownloadReceiptAsync(messageId, receiptId, fileId, directoryToSave);

            var ms = new MemoryStream();
            await _epvvClient.DownloadReceiptAsync(messageId, receiptId, fileId, ms);

            Assert.True(File.Exists(path));
            Assert.True(ms.Length > 0);
        }

        [Fact]
        public async void Create_ThenDeleteMessage()
        {
            var message = await CreateDraftMessage();

            await _epvvClient.DeleteMessageByIdAsync(message.Id);
        }

        [Fact]
        public async void CreateMessage_ThenCreateUploadSession_AndDeleteSession()
        {
            var message = await CreateDraftMessage();

            var messageId = message.Id;
            var fileId = message.Files[0].Id;

            await _epvvClient.CreateUploadSessionAsync(messageId, fileId);
            await _epvvClient.DeleteFileOrSessionAsync(messageId, fileId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(DirectionExchangeType.Inbox)]
        [InlineData(DirectionExchangeType.Outbox)]
        [InlineData(DirectionExchangeType.Bidirectional)]
        public async void GetGuideTasks(DirectionExchangeType? directionExchange)
        {
            var tasks = await _epvvClient.GetGuideTasksAsync(directionExchange);

            Assert.True(tasks.Count > 0);
        }

        [Fact]
        public async void ExceptionDirectionIncorrect_WhenGetGuideTasks()
        {
            try
            {
                var incorrectDirection = (DirectionExchangeType)3;

                await _epvvClient.GetGuideTasksAsync(incorrectDirection);
            }
            catch (ApiRequestException ex)
            {
                Assert.Equal("DERECTION_INCORRECT", ex.Code); // В ответ приходит ответ DERECTION_INCORRECT, должно быть DIRECTION_INCORRECT.
                                                              // Как минимум по документации неправильно + неправильное написание на английском
            }
        }

        [Fact]
        public async void GetProfileInformation()
        {
            var profileInfo = await _epvvClient.GetProfileInfoAsync();

            Assert.NotNull(profileInfo);
        }
        
        [Fact]
        public async void GetQuotaInformation()
        {
            var quotaInfo = await _epvvClient.GetProfileQuotaAsync();

            Assert.NotNull(quotaInfo);
        }

        [Fact]
        public async void GetNotificationsInformation()
        {
            var notifications = await _epvvClient.GetNotificationsAsync();

            Assert.NotNull(notifications);
        }

        [Fact]
        public async void GetGuideList()
        {
            SetEpvvClient(false);

            var guideList = await _epvvClient.GetGuideListAsync();

            Assert.NotNull(guideList);
            Assert.True(guideList.Count > 0);
        }

        [Fact]
        public async void GetGuideRecords()
        {
            SetEpvvClient(false);

            var records = await _epvvClient.GetGuideRecordsAsync("64529d5a-b1d9-453c-96f3-f380ea577314", 2);

            Assert.True(records.Items.Count > 0);
        }

        [Fact]
        public async void DownloadGuide()
        {
            SetEpvvClient(false);

            var path = Path.Combine(DownloadPath, "guide.zip");

            using var fs = new FileStream(path, FileMode.Create);

            await _epvvClient.DownloadGuideAsync("64529d5a-b1d9-453c-96f3-f380ea577314", fs);
        }
    }
}

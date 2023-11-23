using EPVV_CBR.RU.Enums;
using EPVV_CBR.RU.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace EPVV_CBR.RU.Tests
{
    [TestClass()]
    public class EpvvServiceTests
    {
        record struct Credentials
        {
            public string UsernameCB { get; set; }
            public string PasswordCB { get; set; }
        }

        static EpvvServiceOptions GetServiceOptions()
        {
            using (var sr = new StreamReader(@"C:\ServiceCIK\appsettings.json"))
            {
                var data = sr.ReadToEnd();
                var creds = JsonConvert.DeserializeObject<Credentials>(data);
                
                return new EpvvServiceOptions(creds.UsernameCB, creds.PasswordCB, true);
            }
        }

        readonly IEpvvService _service = new EpvvService(GetServiceOptions());

        [TestMethod()]
        public async Task GetMessagesInfoByParametersTest()
        {
            try
            {
                var qp = new QueryParameters(
                        status: MessageStatus.Rejected);

                var messages = await _service.GetMessagesInfoByParameters(qp);

                Assert.IsNotNull(messages);
            }
            catch (Exception ex) 
            { 
                Assert.Fail(ex.Message); 
            }
        }

        [TestMethod()]
        public async Task CreateDraftMessageTest()
        {
            try
            {
                var encFileInfo = new FileInfo(@"C:\ServiceCIK\2023-11-21.zip.enc");
                var encFile = new DirectedFile(
                    name: encFileInfo.Name,
                    fileType: FileType.Document,
                    encrypted: true,
                    size: encFileInfo.Length);

                var sigFileInfo = new FileInfo(@"C:\ServiceCIK\2023-11-21.zip.sig");
                var sigFile = new DirectedFile(
                    name: sigFileInfo.Name,
                    fileType: FileType.Sign,
                    encrypted: false,
                    size: sigFileInfo.Length,
                    signedFile: encFileInfo.Name);

                var files = new List<DirectedFile>()
                {
                    encFile,
                    sigFile
                };

                var messageBody = new RequestMessageBody(
                    task: "Zadacha_137",
                    title: "TEST_137",
                    text: "text_test",
                    files: files);

                var response = await _service.CreateDraftMessage(messageBody);

                var status = response.Status;

                Assert.AreEqual(MessageStatus.Draft, status);
            }
            catch (Exception ex) { Assert.Fail(ex.Message); }
        }
    }
}
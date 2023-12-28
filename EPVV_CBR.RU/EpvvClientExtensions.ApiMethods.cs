using EPVV_CBR_RU.Extensions;
using EPVV_CBR_RU.Models;
using EPVV_CBR_RU.Requests.Methods;
using System.Net.Http.Headers;

namespace EPVV_CBR_RU
{
    /// <summary>
    /// Класс расширений для взаимодействия с методами API
    /// </summary>
    public static class EpvvClientExtensions
    {
        public static async Task<DraftMessage> CreateDraftMessageAsync(
            this IEpvvClient client,
            string task,
            string title,
            string text,
            List<DirectedFile> files,
            string? correlationId = default,
            string? groupId = default,
            List<Receiver>? receivers = default,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
                .MakeRequestAsync(
                    request: new CreateDraftMessageRequest(
                        task: task,
                        title: title,
                        text: text, 
                        files: files, 
                        correlationId: correlationId, 
                        groupId: groupId, 
                        receivers: receivers),
                    cancellationToken)
                .ConfigureAwait(false);

        public static async Task<SessionInfo> CreateUploadSessionAsync(
            this IEpvvClient client,
            string messageId,
            string fileId,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
               .MakeRequestAsync(
                   request: new CreateUploadSessionRequest(messageId, fileId),
                   cancellationToken)
               .ConfigureAwait(false);

        public static async Task<UploadedFile> UploadFileAsync(
            this IEpvvClient client,
            SessionInfo sessionInfo,
            string filePath,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
               .MakeRequestAsync(
                   request: new UploadFileRequest(sessionInfo, filePath),
                   cancellationToken)
               .ConfigureAwait(false);

        public static async Task<List<MessageInfo>> GetMessagesInfoByFiltersAsync(
            this IEpvvClient client,
            QueryFilters? filters = default,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
               .MakeRequestAsync(
                   request: new GetMessagesInfoByFiltersRequest(filters),
                   cancellationToken)
               .ConfigureAwait(false);

        public static async Task<MessageInfo> GetMessageInfoByIdAsync(
            this IEpvvClient client,
            string messageId,
            CancellationToken cancellationToken = default) =>
            await client.ThrowIfNull()
               .MakeRequestAsync(
                   request: new GetMessageInfoByIdRequest(messageId),
                   cancellationToken)
               .ConfigureAwait(false);
    }
}

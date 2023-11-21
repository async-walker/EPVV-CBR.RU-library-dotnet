namespace EPVV_CBR.RU.Models
{
    public class MessageBody
    {
        public MessageBody(string task, string title, string text, List<MessageFile> files, List<MessageReciever> recievers)
        {
            Task = task;
            Title = title;
            Text = text;
            Files = files;
            Recievers = recievers;
        }

        public string Task { get; set; }
        public string? CorrelationId { get; set; }
        public string? GroupId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public List<MessageFile> Files { get; set; }
        public List<MessageReciever>? Recievers { get; set; }
    }
}

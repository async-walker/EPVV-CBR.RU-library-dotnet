namespace EPVV_CBR_RU.Requests.Abstractions
{
    public interface IRequest
    {
        HttpMethod Method { get; }

        string Endpoint { get; }

        HttpContent? ToHttpContent();
    }
}

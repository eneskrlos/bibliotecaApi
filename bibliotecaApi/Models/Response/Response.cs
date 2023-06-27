namespace bibliotecaApi.Models.Response
{
    public class Response
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public Response() 
        {
            Code = string.Empty;
            Message = string.Empty;
            Data = string.Empty;
        
        }

        public Response(string code, string message)
        {
            Code = code;
            Message = message;
            Data = string.Empty;
        }

        public Response(string code, string message, object data)
        {
            Code =code;
            Message =message;
            Data = data;
        }
    }
}

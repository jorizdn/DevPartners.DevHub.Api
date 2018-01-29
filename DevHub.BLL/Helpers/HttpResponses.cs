namespace DevHub.BLL.Helpers
{
    public class HttpResponses
    {
        //constants
        public const int cUnprocessableEntity = 1;
        public const int cOk = 2;
        public const int cNotFound = 3;


        //getters
        public int UnprocessableEntity { get { return cUnprocessableEntity; } }
        public int Ok { get { return cOk; } }
        public int NotFound { get { return cNotFound; } }


        //Method Get
        public Response ShowHttpResponse(int errorCode)
        {
            return GetCorrespondingResponse(errorCode);
        }

        //Validates response
        private Response GetCorrespondingResponse(int errorCode)
        {
            if (errorCode == UnprocessableEntity)
            {
                return new Response
                {
                    CodeResponse = 422,
                    Title = "Unprocessable Entity",
                };
            }

            if (errorCode == Ok)
            {
                return new Response
                {
                    CodeResponse = 200,
                    Title = "Ok",
                    Details = "Successful."
                };
            }

            if (errorCode == NotFound)
            {
                return new Response
                {
                    CodeResponse = 404,
                    Title = "Not Found",
                    Details = "Sorry, Object cannot be found."
                };
            }

            return null;
        }


        //Response Model
        public class Response
        {
            public int CodeResponse { get; set; }
            public string Title { get; set; }
            public string Details { get; set; }
        }
    }
}

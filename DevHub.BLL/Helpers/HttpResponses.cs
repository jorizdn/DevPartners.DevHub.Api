namespace DevHub.BLL.Helpers
{
    public class HttpResponses
    {
        //constants
        public const int cUnprocessableEntity = 1;
        public const int cOk = 2;
        public const int cNotFound = 3;
        public const int cUnauthorized = 4;

        public const int cCreated = 5;
        public const int cUpdated = 6;


        //getters
        public int UnprocessableEntity { get { return cUnprocessableEntity; } }
        public int Ok { get { return cOk; } }
        public int NotFound { get { return cNotFound; } }
        public int Unauthorized { get { return cUnauthorized; } }

        public int Created { get { return cCreated; } }
        public int Updated { get { return cUpdated; } }


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

            if (errorCode == Unauthorized)
            {
                return new Response
                {
                    CodeResponse = 401,
                    Title = "Unauthorized",
                    Details = "Your not allowed to view this end."
                };
            }

            if (errorCode == Created)
            {
                return new Response
                {
                    CodeResponse = 201,
                    Title = "Created",
                    Details = "Object has been created."
                };
            }

            if (errorCode == Updated)
            {
                return new Response
                {
                    CodeResponse = 200,
                    Title = "Updated",
                    Details = "Object has been Modified."
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

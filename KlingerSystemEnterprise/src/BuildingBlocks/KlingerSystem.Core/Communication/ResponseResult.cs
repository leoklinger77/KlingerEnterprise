namespace KlingerSystem.Core.Communication
{
    public class ResponseResult
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorMessages errors { get; set; }

        public ResponseResult()
        {
            errors = new ResponseErrorMessages();
        }
    }
}

namespace Interview
{
    public class GenericPayload
    {
        public string Msg {get; set;} 
        public GenericPayload(string msg = "")
        {
            this.Msg = msg;
        }
    }
}
namespace Logic.MessagesFiles
{
    internal class ErrorMessage : TcpMessage
    {
        public ErrorMessage() { Type = "Error"; }
        public string Reason { get; set; } 
    }
}
using Logic.MessagesFiles;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Logic
{
    public class ServerController
    {
        

        private SynchronizationContext ctx;

        public Action<string>? MessageGetEvent; 
        public Action<string>? ServerLogEvent;  

        
        private readonly UsersManager _userManager = new UsersManager();
        private readonly ChatHistoryManager _historyManager = new ChatHistoryManager();
        private readonly PrivateHistoryManager _privateHistoryManager = new PrivateHistoryManager();

        
        private readonly UdpService _udpService;
        private readonly TcpService _tcpService;

        private bool _isServerRunning = false;


        public ServerController(SynchronizationContext ctx = null)
        {

            if (ctx != null)
            {
                this.ctx = ctx;
            }
            
            
            _udpService = new UdpService(_historyManager, LogMessages, LogSystem);
            _tcpService = new TcpService(_userManager, _historyManager, LogSystem, _privateHistoryManager);
        }

        public void LogSystem(string msg)
        {
            if (ctx != null)

                ctx.Post(d => ServerLogEvent?.Invoke(msg), null);
            else
                ServerLogEvent?.Invoke(msg);
        }

        public void LogMessages(string name, string msg)
        {
            string fullMsg = $"{name}: {msg}";

            if (ctx != null)

                ctx.Post(d => MessageGetEvent?.Invoke(fullMsg), null);
            else
                MessageGetEvent?.Invoke(fullMsg);
        }

        public void Start()
        {
            _isServerRunning = true;
            _ = Task.Run(() => _udpService.StartListeningAsync());
            _ = Task.Run(() => _tcpService.StartListeningAsync());

            LogSystem("Server started");
        }

        public void Stop()
        {
            if (!_isServerRunning) return;

            LogSystem("Server stopping...");
            _isServerRunning = false;
            
            _tcpService.Stop();
            _udpService.Stop();

            
            _userManager.CloseAllConnections();
            LogSystem("Server stopped");
        }

    }
}

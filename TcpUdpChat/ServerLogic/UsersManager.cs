using System.Collections.Concurrent;
using System.Net.Sockets;
using Logic.MessagesFiles;

namespace Logic
{
    public class UsersManager
    {
        private readonly ConcurrentDictionary<string, TcpClient> _users = new ConcurrentDictionary<string, TcpClient>();
        public bool AddUser(string username, TcpClient user)
        {
            return _users.TryAdd(username, user);
        }
        public bool RemoveUser(string username)
        {
            return _users.TryRemove(username, out _);
        }
        public TcpClient? GetUser(string username)
        {
            _users.TryGetValue(username, out TcpClient user);
            return user;
        }

        public List<string> GetAllUserNames()
        {
            
            return _users.Keys.ToList();
        }
        public async Task SendMessageAsync(byte[] message, string senderName = null)
        {
            foreach (var pair in _users)
            {
                
                if (pair.Key == senderName)
                {
                    continue;
                }

                TcpClient client = pair.Value;
                try
                {
                    if (client.Connected)
                    {
                        
                        NetworkStream stream = client.GetStream();
                        await stream.WriteAsync(message, 0, message.Length);
                    }
                }
                catch (System.IO.IOException)
                {
                    RemoveUser(pair.Key);
                }
                catch (System.Exception)
                {
                    RemoveUser(pair.Key);
                }
            }
        }

        public void CloseAllConnections()
        {
            
            
            foreach (var pair in _users)
            {
                try
                {
                    
                    
                    
                    pair.Value.Close();
                }
                catch (Exception)
                {
                    
                    
                }
            }

            
            
            
            
            _users.Clear();
        }

    }

}
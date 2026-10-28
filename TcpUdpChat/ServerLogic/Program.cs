/*
TCP_UDP. UDP-chat (multicast) / TCP.
0) Клієнт – сервер – клієнт.
0.1) Сервер знає імена + EndPoint учасників, зберігає історію чату.
1) MVC патерн: виносимо асинхронні методи відправки і отримання сповіщень в контролер, форма викликає методи контролера.
2) Переписати код, додати інформацію, хто відправляє сповіщення. Для учасника чату організувати введення свого імені, яке потім відображається в сповіщеннях.
3) Додати окремо колонку: список учасників. Клік по учаснику — перехід у спілкування TCP, плюс кнопка виходу в загальний чат (Server).
Додати class Message (Msg, Name) + JSON.
*/
namespace Logic
{
    
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var server = new ServerController();

            

            
            server.ServerLogEvent += (msg) =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow; 
                Console.WriteLine($"[SYSTEM] {msg}");
                Console.ResetColor();
            };

            
            server.MessageGetEvent += (fullMsg) =>
            {
                Console.WriteLine(fullMsg); 
            };
            
            server.Start();

            
            
            
            
            Console.WriteLine("Сервер запущен. Нажмите [Enter] для остановки.");
            Console.ReadLine();
        }
    }
}

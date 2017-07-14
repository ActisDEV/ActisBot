# ActisBot
A Discord bot written in C# using DSharpPlus.
## Commands
```
a!help - выводит список доступных команд
a!ping - выводит пинг
a!greet <member member> - здоровается с указанным участником
a!hi - здоровается
a!random <int first> <int second> - выводит случайное число в заданном диапозоне
a!magicball <string message> - выводит вероятность заданного события (joke)
a!dateball <string message> - выводит дату заданного события (joke)
a!quiz - выводит вопрос викторины
a!answer <int key> <string answer> - ответ на вопрос викторины

a!admin sudo <member member> <string command> - выполнение команды от имени другого пользователя
a!admin setnick <member member> <string newNick> - изменение ника
a!admin kick <member member> <string reason> - кик участника
a!admin ban <member member> <int days> <string reason> - бан участника с удалением всех его сообщений за последнии days дней
a!admin getanswer <int key> - получение ответа на вопрос из викторины
```

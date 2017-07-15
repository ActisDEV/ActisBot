using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
//using DSharpPlus.VoiceNext;
using IniParser;
using IniParser.Model;

namespace bot
{
    public class UngrouppedCommands
    {
        [Command("ping")]
        [Description("Возвращает пинг")]

        public async Task Ping(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync($"{ctx.Member.Mention} Pong! Ping: {ctx.Client.Ping}ms");
        }

        [Command("greet")]
        [Description("Здоровается с упомянутым участником")]
        
        public async Task Hi(CommandContext ctx, [Description("Пользователь, с которым поздороваться")] DiscordMember member)
        {
            await ctx.TriggerTypingAsync();

            await ctx.RespondAsync($"Привет, {member.Mention}");
        }

        [Command("hi")]
        [Description("Здоровается с отправившим сообщение")]

        public async Task Greet(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            await ctx.RespondAsync($"Привет, {ctx.Member.Mention}");
        }

        [Command("random")]
        [Description("Выводит случайное число из заданного диапозона")]
        
        public async Task Random(CommandContext ctx, [Description("Нижняя граница диапозона")] int down, [Description("Верхняя граница диапозона")] int up)
        {
            await ctx.TriggerTypingAsync();

            Random random = new Random();
            int randomNum;
            randomNum = random.Next(down, up);

            await ctx.RespondAsync($"{ctx.Member.Mention} Твоё число - {randomNum}");
        }

        [Command("magicball")]
        [Description("Магический шар, выдающий инфу")]
        [Aliases("info")]

        public async Task MagicBall(CommandContext ctx, [Description("Анализируемая инфа")] string message)
        {
            await ctx.TriggerTypingAsync();

            Random random = new Random();
            int randomNum;
            randomNum = random.Next(0, 100);

            await ctx.RespondAsync($"{ctx.Member.Mention} Магический шар считает, что вероятность равна {randomNum}%. {Functions.Result(randomNum)}.");
        }

        [Command("dateball")]
        [Description("Магический шар, определяющий когда произойдут события")]
        [Aliases("date", "when")]

        public async Task DateBall(CommandContext ctx, [Description("Анализируемое событие")] string message)
        {
            await ctx.TriggerTypingAsync();

            var random = new Random();

            var date = new DateTime();
            date = DateTime.Now;

            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            int randomDay = random.Next(1, 28);
            int randomMonth = random.Next(1, 12);
            int randomYear = random.Next(date.Year, 2100);

            if ((year == randomYear) && (randomMonth < month))
            {
                randomMonth = month;
                if (randomDay <= day)
                {
                    randomDay = day++;
                }
            }
            else if ((year == randomYear) && (randomMonth == month))
            {
                if (randomDay <= day)
                {
                    randomDay = day++;
                }
            }

            await ctx.RespondAsync($"{ctx.Member.Mention} Это событие произойдёт {randomDay}.{randomMonth}.{randomYear}");
        }

        [Command ("quiz")]
        [Description("Выводит вопрос из викторины")]

        public async Task Quiz(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            var random = new Random();
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("quiz.ini");

            int key = random.Next(1,50);
            string keyStr = Convert.ToString(key);

            KeyDataCollection keyCol = data["questions"];
            string question = keyCol[keyStr];

            Console.WriteLine(question);

            await ctx.RespondAsync($"{ctx.Member.Mention} Вот твой вопрос: ");
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync($"{ctx.Member.Mention} {question} Номер: {key} ");
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync($"{ctx.Member.Mention} Введи a!answer {key} <твой ответ>, чтобы ответить");
        }

        [Command("answer")]
        [Description("Ответ на вопрос a!quiz")]
        
        public async Task Answer(CommandContext ctx, [Description("Номер вопроса")] int key, [Description("Ответ")] string answer)
        {
            await ctx.TriggerTypingAsync();

            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("quiz.ini");

            KeyDataCollection keyCol = data["answers"];
            string keyStr = Convert.ToString(key);
            string answerTrue = keyCol[keyStr];
            
            if (answer.ToLower().Equals(answerTrue))
            {
                await ctx.RespondAsync($"{ctx.Member.Mention} :white_check_mark: Твой ответ верный!");
            }
            else
            {
                await ctx.RespondAsync($"{ctx.Member.Mention} :x: Твой ответ неверный!");
            }
        }
    }

    [Group("admin")]
    [Description("Административные команды")]
    [Hidden]

    public class AdminCommands
    {
        //команды выполняются a!admin <команда> <аргументы>
        [Command("sudo")]
        [Description("Выполнение команды от имени другого пользователя")]
        [RequirePermissions(Permissions.ManageGuild)]
        [Hidden]
        public async Task Sudo(CommandContext ctx, [Description("Пользователь, от чьего имени будет исполнена команда")] DiscordMember member, [Description("Команда для выполнения")] string command)
        {
            await ctx.TriggerTypingAsync();
            var cmds = ctx.Client.GetCommandsNext();

            await cmds.SudoAsync(member, ctx.Channel, command);
        }

        [Command("setnick")]
        [Description("Изменение ника пользователя")]
        [RequirePermissions(Permissions.ManageNicknames)]
        [Hidden]
        public async Task Setnick(CommandContext ctx, [Description("Пользователь, которому изменяется ник")] DiscordMember member, [RemainingText, Description("Устанавливаемый ник")] string newNick)
        {
            await ctx.TriggerTypingAsync();
            if (newNick.Equals("Actis"))
            {
                await ctx.RespondAsync($"Да пошёл ты нахуй, {ctx.User.Username}");
            }
            else
            {
                try
                {
                    await member.ModifyAsync(newNick, reason: $"Changed by {ctx.User.Username} ({ctx.User.Id})");

                    await ctx.RespondAsync($"Никнейм {member.Username} был изменён на {newNick} администратором {ctx.User.Username}");
                }
                catch (Exception)
                {
                    await ctx.RespondAsync($"Никнейм {member.Username} не может быть изменён на {newNick}");
                }
            }
        }

        [Command("kick")]
        [Description("Кик пользователя")]
        [RequirePermissions(Permissions.KickMembers)]
        [Hidden]
        public async Task Kick(CommandContext ctx, [Description("Исключаемый пользователь")] DiscordMember member, [RemainingText, Description("Причина")] string reason)
        {
            await ctx.TriggerTypingAsync();
            DiscordGuild guild = new DiscordGuild();
            guild = member.Guild;
            try
            {
                await guild.RemoveMemberAsync(member, reason);
                await ctx.RespondAsync($"Пользователь @{member.Username}#{member.Discriminator} был исключён администратором {ctx.User.Username}");
            }
            catch (Exception)
            {
                await ctx.RespondAsync($"Пользователь {member.Username} не может быть исключён");
            }
        }

        [Command("ban")]
        [Description("Бан пользователя")]
        [RequirePermissions(Permissions.BanMembers)]
        [Hidden]
        public async Task Ban(CommandContext ctx, [Description("Блокируемый пользователь")] DiscordMember member, [Description("За сколько дней удалить сообщения?")] int days, [RemainingText, Description("Причина")] string reason)
        {
            await ctx.TriggerTypingAsync();
            DiscordGuild guild = new DiscordGuild();
            guild = member.Guild;
            try
            {
                await guild.BanMemberAsync(member, days, reason);
                await ctx.RespondAsync($"Пользователь @{member.Username}#{member.Discriminator} был исключён администратором {ctx.User.Username}");
            }
            catch (Exception)
            {
                await ctx.RespondAsync($"Пользователь {member.Username} не может быть заблокирован");
            }
        }

        [Command("getanswer")]
        [Description("Получает ответ на вопрос викторины")]
        [RequirePermissions(Permissions.ManageGuild)]
        [Hidden]
        public async Task GetAnswer(CommandContext ctx, [Description("Номер вопроса на который нужно получить ответ")] int key)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("quiz.ini");

            string keyStr = Convert.ToString(key);
            KeyDataCollection qCol = data["questions"];
            KeyDataCollection keyCol = data["answers"];

            string q = qCol[keyStr]; //вопрос
            string a = keyCol[keyStr]; //ответ

            var embed = new DiscordEmbed
            {
                Title = "Вопрос: " + q,
                Description = "Ответ: " + a,
                Color = 0xFFFF00
            };

            await ctx.RespondAsync("", embed: embed);
        }
    }

   /* [Group("audio")]
    [Description("Аудио-команды")]
    [Hidden]*/

  /*  public class AudioCommands
    {
        //команды выполняются a!audio <комманда>

        [Command("join")]
        [Description("Присоединение к голосовому каналу")]

        public async Task Join(CommandContext ctx, DiscordChannel chnl = null)
        {
            var vnext = ctx.Client.GetVoiceNextClient();
            if (vnext == null)
            {
                await ctx.RespondAsync($"{ctx.Member.Mention} VNext не сконфигрурирован");
                return;
            }

            var vnc = vnext.GetConnection(ctx.Guild);

            if (vnc != null)
            {
                await ctx.RespondAsync($"{ctx.Member.Mention} Уже присоединён");
                return;
            }

            var vstat = ctx.Member?.VoiceState;

            if ((vstat == null || vstat.Channel == null) && chnl == null)
            {
                await ctx.RespondAsync($"{ctx.Member.Mention} Ты не в голосовом канале!");
                return;
            }

            if (chnl == null)
                chnl = vstat.Channel;

            vnc = await vnext.ConnectAsync(chnl);

            await ctx.RespondAsync($"{ctx.Member.Mention} Присоединяюсь к каналу");
        }

        [Command("leave")]
        [Description("Отключение от канала")]
        [RequirePermissions(Permissions.MoveMembers)]

        public async Task Leave(CommandContext ctx)
        {
            var vnext = ctx.Client.GetVoiceNextClient();

            var vnc = vnext.GetConnection(ctx.Guild);
            if (vnc == null)
            {
                await ctx.RespondAsync($"{ctx.Member.Mention} Не присоединён");
                return;
            }

            // disconnect
            vnc.Disconnect();

            await ctx.RespondAsync($"{ctx.Member.Mention} Отключен");
        }
    } */ //проблемы с опусом
}

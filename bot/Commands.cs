using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
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
            
            if (answer.Equals(answerTrue))
            {
                await ctx.RespondAsync($"{ctx.Member.Mention} :white_check_mark: Твой ответ верный!");
            }
            else
            {
                await ctx.RespondAsync($"{ctx.Member.Mention} :x: Твой ответ неверный!");
            }
        }
    }
}

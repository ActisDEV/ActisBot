using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

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

        public async Task MagicBall(CommandContext ctx, [Description("Анализируемое сообщение")] string message)
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

        public async Task DateBall(CommandContext ctx, [Description("Анализируемая инфа")] string message)
        {
            await ctx.TriggerTypingAsync();

            Random random = new Random();

            DateTime date = new DateTime();
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
    }
}

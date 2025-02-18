﻿using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using TomatBot.Core.Content.Embeds;
using TomatBot.Core.Framework.CommandFramework;
using TomatBot.Core.Framework.DataStructures;

namespace TomatBot.Core.Content.Commands.InfoCommands
{
    public sealed class HelpCommand : TomatCommand
    {
        public override MethodInfo? AssociatedMethod => GetType().GetMethod("HandleCommand");

        public override HelpCommandData HelpData =>
            new("help", "Displays an embed containing basic information on all registered commands.");

        public override CommandType CType => CommandType.Info;

        [Command("help")]
        [Summary("Shows bot commands.")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public Task HandleCommand()
        {
            string listOfInfoCommands = string.Join('\n', CommandRegistry.infoCommands!);
            string listOfFunCommands = string.Join('\n', CommandRegistry.funCommands!);
            string listOfConfigCommands = string.Join('\n', CommandRegistry.configCommands!);

            BaseEmbed embed = new(Context.User)
            {
                Title = "Command Help",

                Description = "The following is a list of all bot commands." +
                              $"\nAll of these should be prefixed with `{BotStartup.GetGuildPrefix(Context.Guild)}`. (If your guild has changed the prefix, you can also use `{BotStartup.DefaultPrefix}`)" +
                              "\n`<>`: required" +
                              "\n`[]`: optional",
                Fields = new List<EmbedFieldBuilder>
                {
                    new()
                    {
                        IsInline = false,
                        Name = "Informative Commands",
                        Value = string.IsNullOrEmpty(listOfInfoCommands) ? "N/A" : listOfInfoCommands
                    },

                    new()
                    {
                        IsInline = false,
                        Name = "Fun Commands",
                        Value = string.IsNullOrEmpty(listOfFunCommands) ? "N/A" : listOfFunCommands
                    },

                    new()
                    {
                        IsInline = false,
                        Name = "Configuration Commands",
                        Value = string.IsNullOrEmpty(listOfConfigCommands) ? "N/A" : listOfConfigCommands
                    }
                }
            };

            return ReplyAsync(embed: embed.Build());
        }
    }
}

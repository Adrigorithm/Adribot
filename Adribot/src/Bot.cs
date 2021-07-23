using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Adribot.config;
using Adribot.src.events;
using Adribot.src.services;
using Adribot.src.services.spec;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Adribot
{
    public class Bot
    {
        private DiscordClient _client;
        private CommandsNextExtension _commands;
        private InteractivityExtension _interactivity;
        private BotEventHandler _eventHandler;
        private Config _botConfig;

        /// <summary>
        /// Creates a bot
        /// </summary>
        /// <param name="useCommands">Whether or not this bot should use commands</param>
        /// <param name="useInteractivity">Whether or not this bot will interact with users</param>
        public Bot() => 
            SetupDiscordClient();

        private void SetupDiscordClient() {
            _botConfig = new Config();
            Task.Run(async () => await _botConfig.LoadConfigAsync()).Wait();

            _client = new DiscordClient(new DiscordConfiguration {
                Token = _botConfig.Token,
                MinimumLogLevel = LogLevel.Information,
                Intents = DiscordIntents.All
            });

            SetupInteractivity();
            SetupCommands();

            AttachEvents();
        }

        /// <summary>
        /// Enables specific events to the current DiscordClient instance
        /// </summary>
        private void AttachEvents() {
            _eventHandler = new BotEventHandler(_client) {
                EnableMessageCreated = true
            };

            _eventHandler.Attach();
        }

        /// <summary>
        /// Enables the execution of commands on the client
        /// </summary>
        private void SetupCommands() {

            // Dependency Injection
            var services = new ServiceCollection()
                .AddSingleton<IService, BanService>()
                .BuildServiceProvider();

            _commands = _client.UseCommandsNext(new CommandsNextConfiguration {
                EnableDms = false,
                StringPrefixes = _botConfig.Prefixes,
                Services = services
            });
        }

        private void SetupInteractivity() {
            _interactivity = _client.UseInteractivity(new InteractivityConfiguration());
        }

        /// <summary>
        /// Add commands grouped in a class
        /// This class should inherit from BaseCommandModule
        /// </summary>
        /// <param name="commands">List of classes with commands that this bot should use</param>
        public void AttachCommands(IEnumerable<Type> commands) {
            foreach(var commandCollection in commands) {
                _commands.RegisterCommands(commandCollection);
            }
        }

        /// <summary>
        /// Starts the bot
        /// </summary>
        public async Task Start() {
            await _client.ConnectAsync();
        }

        /// <summary>
        /// Stops the bot
        /// </summary>
        public async Task Stop() {
            await _client.DisconnectAsync();
        }
    }
}

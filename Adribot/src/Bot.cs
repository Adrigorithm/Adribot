using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adribot.config;
using Adribot.services;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.Logging;
using EventHandler = Adribot.events.EventHandler;

namespace Adribot
{
    public class Bot
    {
        private DiscordClient _client;
        private CommandsNextExtension _commands;
        private InteractivityExtension _interactivity;
        private EventHandler _events;

        /// <summary>
        /// Creates a bot
        /// </summary>
        /// <param name="useCommands">Whether or not this bot should use commands</param>
        /// <param name="useInteractivity">Whether or not this bot will interact with users</param>
        public Bot(bool useCommands, bool useInteractivity) {
            SetupDiscordClient(useCommands, useInteractivity);
        }

        private void SetupDiscordClient(bool useCommands, bool useInteractivity) {
            _client = new DiscordClient(new DiscordConfiguration {
                Token = Config.Token,
                MinimumLogLevel = LogLevel.Information
            });

            if(useCommands) {
                SetupCommands();
            }

            if(useInteractivity) {
                SetupInteractivity();
            }

            AttachEvents();
        }
        
        /// <summary>
        /// Enables specific events to the current DiscordClient instance
        /// </summary>
        private void AttachEvents() {
            _events = new EventHandler(_client)
            {
                EnableMessageCreated = true
            };
        }

        /// <summary>
        /// Enables the execution of commands on the client
        /// </summary>
        private void SetupCommands() {
            _commands = _client.UseCommandsNext(new CommandsNextConfiguration {
                EnableDms = false,
                StringPrefixes = new[] { Config.Prefix }
            });

            SetupServices();
        }

        private void SetupServices() {
            BanService.SetupBanService(_client);
        }

        private void SetupInteractivity() {
            _interactivity = _client.UseInteractivity(new InteractivityConfiguration());
        }

        /// <summary>
        /// Add commands grouped in a class
        /// This class should inherit from BaseCommands
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

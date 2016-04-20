using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using CommanderLucy.Model;

namespace CommanderLucy.Services
{
    public class SpeechService : ISpeechService
    {
        private readonly List<Command> _commands;
        private readonly IPluginService _pluginService;
        private readonly SpeechRecognitionEngine _recognitionEngine;
        private readonly SpeechSynthesizer _synthesizer;

        //TODO: ExceptionHandling, if ther is no audio device
        public SpeechService(IConfigService configService, IPluginService pluginService)
        {
            _pluginService = pluginService;
            _recognitionEngine = new SpeechRecognitionEngine();
            _recognitionEngine.SetInputToDefaultAudioDevice();

            _synthesizer = new SpeechSynthesizer();
            _synthesizer.SelectVoice("Microsoft Zira Desktop"); //english

            //TODO: register config changed msg?!
            _commands = configService.LoadConfig();
        }

        public void StartRecognizing()
        {
            // Create a grammar
            var choices = new Choices();
            choices.Add(new[] {"Commander"});

            LoadGrammar(choices);

            // Register a handler for the SpeechRecognized event.
            _recognitionEngine.SpeechRecognized += LucyRecognized;

            _synthesizer.Speak("Commander Lucy expects your commands.");

            _recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void LoadGrammar(Choices choices)
        {
            // Create a GrammarBuilder object and append the Choices object.
            var gb = new GrammarBuilder();
            gb.Append(choices);

            // Create the Grammar instance and load it into the speech recognition engine.
            var g = new Grammar(gb);
            _recognitionEngine.LoadGrammar(g);
        }

        private void LucyRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            _recognitionEngine.SpeechRecognized -= LucyRecognized;
            _synthesizer.Speak("What can i do for you?");

            // Create a grammar
            var commandChoices = new Choices();
            commandChoices.Add(_commands.Select(c => c.CommandText).ToArray());

            LoadGrammar(commandChoices);

            // Register a handler for the SpeechRecognized event.
            _recognitionEngine.SpeechRecognized += CommandRecognized;
        }

        private void CommandRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            _recognitionEngine.SpeechRecognized -= CommandRecognized;

            Command command = _commands.FirstOrDefault(c => c.CommandText == e.Result.Text);

            if (command != null)
            {
                if (command.Type == CommandType.Basic)
                {
                    Process.Start(command.Action);
                }
                else
                {
                    _pluginService.ExecutePlugin(command.Action, command.Parameters);
                }
            }

            // Register a handler for the SpeechRecognized event.
            _recognitionEngine.SpeechRecognized += LucyRecognized;
        }
    }
}
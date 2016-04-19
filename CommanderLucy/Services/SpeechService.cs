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
        private readonly SpeechRecognitionEngine _recognitionEngine;

        public SpeechService(IConfigService configService)
        {
            _recognitionEngine = new SpeechRecognitionEngine();
            _commands = configService.LoadConfig();
        }

        public void StartRecognizing()
        {
            //TODO: only 'commander lucy' in choices, if rec then add commands!

            // Create a grammar
            var commandChoices = new Choices();
            commandChoices.Add(_commands.Select(c => c.CommandText).ToArray());

            // Create a GrammarBuilder object and append the Choices object.
            var gb = new GrammarBuilder();
            gb.Append(commandChoices);

            // Create the Grammar instance and load it into the speech recognition engine.
            var g = new Grammar(gb);
            _recognitionEngine.LoadGrammar(g);

            // Register a handler for the SpeechRecognized event.
            _recognitionEngine.SpeechRecognized += sre_SpeechRecognized;

            _recognitionEngine.SetInputToDefaultAudioDevice();

            var synthesizer = new SpeechSynthesizer();
            synthesizer.SelectVoice("Microsoft Zira Desktop"); //english
            synthesizer.Speak("Commander Lucy expects your commands.");

            _recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            var command = _commands.FirstOrDefault(c => c.CommandText == e.Result.Text);

            if (command != null)
            {
                Process.Start(command.Action);
            }
        }
    }
}
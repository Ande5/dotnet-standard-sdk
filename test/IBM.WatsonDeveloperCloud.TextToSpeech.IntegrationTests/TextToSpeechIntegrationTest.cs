﻿/**
* Copyright 2017 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/
using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.TextToSpeech.v1;
using IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.IntegrationTests
{
    [TestClass]
    public class TextToSpeechIntegrationTest
    {
        private string _userName;
        private string _password;
        private string _endpoint;

        private ITextToSpeechService _service;

        [TestInitialize]
        public void Setup()
        {

            var environmentVariable =
            Environment.GetEnvironmentVariable("VCAP_SERVICES");

            var fileContent =
            File.ReadAllText(environmentVariable);

            var vcapServices =
            JObject.Parse(fileContent);

            _endpoint = vcapServices["text_to_speech"][0]["credentials"]["url"].Value<string>();
            _userName = vcapServices["text_to_speech"][0]["credentials"]["username"].Value<string>();
            _password = vcapServices["text_to_speech"][0]["credentials"]["password"].Value<string>();

            _service =
                new TextToSpeechService(_userName, _password);
        }

        [TestMethod]
        public void GetVoices_Success()
        {
            var result =
                _service.GetVoices();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.VoiceList);
            Assert.IsNotNull(result.VoiceList.Count > 0);
        }

        [TestMethod]
        public void GetVoice_Success()
        {
            var resultGetVoices =
                _service.GetVoices();

            var voice = resultGetVoices.VoiceList.First();

            var result =
                _service.GetVoice(voice.Name);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, voice.Name);
        }

        [TestMethod]
        public void Synthesize_WithText_Sucess()
        {
            var audio =
                _service.Synthesize("This is a dotnet SDK", accept: HttpMediaType.AUDIO_WAV);

            Assert.IsNotNull(audio);
            Assert.IsTrue(audio.Length > 0);

            File.WriteAllBytes("audio_teste.wav", audio);
        }

        [TestMethod]
        public void Synthesize_WithBody_Sucess()
        {
            _service =
                new TextToSpeechService(_userName, _password);

            var audio =
                _service.Synthesize(new Text()
                {
                    TextProperty = "This is a dotnet SDK, with body text!"
                }, accept: HttpMediaType.AUDIO_WAV);

            Assert.IsNotNull(audio);
            Assert.IsTrue(audio.Length > 0);

            File.WriteAllBytes("audio_body_teste.wav", audio);
        }

        [TestMethod]
        public void GetPronunciation_Success()
        {
            _service =
               new TextToSpeechService(_userName, _password);

            var pronunciation =
                _service.GetPronunciation("This is a pronunciation");

            Assert.IsNotNull(pronunciation);
            Assert.IsNotNull(pronunciation.Value);
        }

        [TestMethod]
        public void ListCustomModels_Success()
        {
            _service =
              new TextToSpeechService(_userName, _password);

            var customModels =
                _service.ListCustomModels();

            Assert.IsNotNull(customModels);
            Assert.IsNotNull(customModels.CustomizationsList);
            Assert.IsTrue(customModels.CustomizationsList.Count > 0);
        }

        [TestMethod]
        public void ListCustomModel_Success()
        {
            _service =
               new TextToSpeechService(_userName, _password);

            var customModels =
                _service.ListCustomModels();

            var firstModel =
                customModels.CustomizationsList.First();

            var customModel =
                _service.ListCustomModel(firstModel.CustomizationId);

            Assert.IsNotNull(customModel);
            Assert.IsTrue(customModel.CustomizationId == firstModel.CustomizationId);
        }

        [TestMethod]
        public void CreateCustomModel_Success()
        {
            _service =
               new TextToSpeechService(_userName, _password);

            var result =
                _service.CreateCustomModel(new CustomVoice()
                {
                    Name = "dotnet-standard-custom-voice-model",
                    Description = "Custom voice model created by the .NET Standard SDK.",
                    Language = "en-US"
                });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.CustomizationIdProperty);
        }
    }
}
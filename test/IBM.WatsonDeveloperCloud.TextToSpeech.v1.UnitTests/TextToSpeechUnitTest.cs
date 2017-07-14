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
using IBM.WatsonDeveloperCloud.Http.Exceptions;
using IBM.WatsonDeveloperCloud.TextToSpeech.v1;
using IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.UnitTests
{
    [TestClass]
    public class TextToSpeechUnitTest
    {
        [TestMethod]
        public void GetVoices_Success()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            VoiceSet response =
                new VoiceSet()
                {
                    VoiceList = new List<Voice>()
                    {
                        new Voice()
                        {
                            Customizable = true,
                            Description = "description",
                            Gender = "gender",
                            Language = "language",
                            Name = "name",
                            SupportedFeatures = new SupportedFeatures()
                            {
                                CustomPronunciation = false,
                                VoiceTransformation = false
                            },
                            Url = "url"
                        }
                    }
                };

            request.As<VoiceSet>()
                   .Returns(Task.FromResult(response));

            TextToSpeechService service =
                new TextToSpeechService(client);

            var voiceSet =
                service.GetVoices();

            Assert.IsNotNull(voiceSet);
            Assert.IsNotNull(voiceSet.VoiceList);
            Assert.IsTrue(voiceSet.VoiceList.Count() > 0);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void GetVoices_Catch_Exception()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                  });

            TextToSpeechService service =
                new TextToSpeechService(client);

            var voiceSet =
                service.GetVoices();
        }

        [TestMethod]
        public void GetVoice_Success()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            VoiceCustomization response =
                 new VoiceCustomization()
                 {
                     Customizable = true,
                     Description = "description",
                     Gender = "gender",
                     Language = "language",
                     Name = "name",
                     SupportedFeatures = new SupportedFeatures()
                     {
                         CustomPronunciation = false,
                         VoiceTransformation = false
                     },
                     Url = "url",
                     Customization = new Customization()
                     {
                         Created = "created",
                         CustomizationId = "customization_id",
                         Description = "description",
                         Language = "language",
                         LastModified = "last_modified",
                         Name = "name",
                         Owner = "owner"
                     }
                 };

            request.As<VoiceCustomization>()
                   .Returns(Task.FromResult(response));

            TextToSpeechService service =
                new TextToSpeechService(client);

            var voiceCustomization =
                service.GetVoice(voiceName: "voice_name", customizationId: "customization_id");

            Assert.IsNotNull(voiceCustomization);
            Assert.IsNotNull(voiceCustomization.Customization);
        }

        [TestMethod]
        public void GetVoice_Without_CustomizationId_Success()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            VoiceCustomization response =
                 new VoiceCustomization()
                 {
                     Customizable = true,
                     Description = "description",
                     Gender = "gender",
                     Language = "language",
                     Name = "name",
                     SupportedFeatures = new SupportedFeatures()
                     {
                         CustomPronunciation = false,
                         VoiceTransformation = false
                     },
                     Url = "url",
                     Customization = new Customization()
                     {
                         Created = "created",
                         CustomizationId = "customization_id",
                         Description = "description",
                         Language = "language",
                         LastModified = "last_modified",
                         Name = "name",
                         Owner = "owner"
                     }
                 };

            request.As<VoiceCustomization>()
                   .Returns(Task.FromResult(response));

            TextToSpeechService service =
                new TextToSpeechService(client);

            var voiceCustomization =
                service.GetVoice(voiceName: "voice_name");

            Assert.IsNotNull(voiceCustomization);
            Assert.IsNotNull(voiceCustomization.Customization);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetVoice_VoiceName_Null()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            TextToSpeechService service =
                new TextToSpeechService(client);

            var voiceCustomization =
                 service.GetVoice(voiceName: null, customizationId: "customization_id");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetVoice_VoiceName_Empty()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            TextToSpeechService service =
                new TextToSpeechService(client);

            var voiceCustomization =
                 service.GetVoice(voiceName: string.Empty, customizationId: "customization_id");
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void GetVoice_Catch_Exception()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                  });

            TextToSpeechService service =
                new TextToSpeechService(client);

            var voiceCustomization =
                 service.GetVoice(voiceName: "voice_name", customizationId: "customization_id");
        }

        [TestMethod]
        public void Synthesize_WithText_Success()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            byte[] response =
                new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

            request.As<byte[]>()
                   .Returns(Task.FromResult(response));

            TextToSpeechService service =
                new TextToSpeechService(client);

            var audio =
                service.Synthesize(text: "voice_name", customizationId: "customization_id");

            Assert.IsNotNull(audio);
            Assert.IsTrue(response.Length > 0);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Synthesize_With_Null_Text()
        {
            IClient client = Substitute.For<IClient>();

            TextToSpeechService service =
                new TextToSpeechService(client);

            var audio =
                service.Synthesize(text: null, customizationId: "customization_id");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Synthesize_With_Empty_Text()
        {
            IClient client = Substitute.For<IClient>();

            TextToSpeechService service =
                new TextToSpeechService(client);

            var audio =
                service.Synthesize(text: string.Empty, customizationId: "customization_id");
        }

        [TestMethod]
        public void Synthesize_WithBody_Success()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithHeader(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            byte[] response =
                new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

            request.As<byte[]>()
                   .Returns(Task.FromResult(response));

            TextToSpeechService service =
                new TextToSpeechService(client);

            var audio =
                service.Synthesize(body: new Text() { TextProperty = "text" }, customizationId: "customization_id");

            Assert.IsNotNull(audio);
            Assert.IsTrue(response.Length > 0);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Synthesize_With_Null_Body()
        {
            IClient client = Substitute.For<IClient>();

            TextToSpeechService service =
                new TextToSpeechService(client);

            var audio =
                service.Synthesize(body: null, customizationId: "customization_id");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Synthesize_With_Null_Body_Text()
        {
            IClient client = Substitute.For<IClient>();

            TextToSpeechService service =
                new TextToSpeechService(client);

            var audio =
                service.Synthesize(body: new Text() { TextProperty = null }, customizationId: "customization_id");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Synthesize_With_Empty_Body_Text()
        {
            IClient client = Substitute.For<IClient>();

            TextToSpeechService service =
                new TextToSpeechService(client);

            var audio =
               service.Synthesize(body: new Text() { TextProperty = string.Empty }, customizationId: "customization_id");
        }

        [TestMethod]
        public void GetPronunciation_Success()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            var pronunciationResponse = new Pronunciation()
            {
                Value = "value"
            };

            request.As<Pronunciation>()
                   .Returns(Task.FromResult(pronunciationResponse));

            TextToSpeechService service =
                new TextToSpeechService(client);

            var pronunciation =
               service.GetPronunciation(text: "This is a pronunciation", customizationId: "customization_id");

            Assert.IsNotNull(pronunciation);
            Assert.IsNotNull(pronunciation.Value);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetPronunciation_With_Null_Text()
        {
            IClient client = Substitute.For<IClient>();

            TextToSpeechService service =
                new TextToSpeechService(client);

            var pronunciation =
               service.GetPronunciation(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetPronunciation_With_Empty_Text()
        {
            IClient client = Substitute.For<IClient>();

            TextToSpeechService service =
                new TextToSpeechService(client);

            var pronunciation =
               service.GetPronunciation(string.Empty);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void GetPronunciation_Catch_Exception()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                      .Returns(x =>
                      {
                          throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                   Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                   string.Empty));
                      });

            TextToSpeechService service =
                new TextToSpeechService(client);

            var pronunciation =
               service.GetPronunciation(text: "This is a pronunciation", customizationId: "customization_id");
        }

        [TestMethod]
        public void ListCustomModels_Success()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(request);

            var customizations = new Customizations()
            {
                CustomizationsList = new List<Customization>()
                {
                    new Customization()
                    {
                        Created = "created",
                        CustomizationId = "customization_id",
                        Description = "description",
                        Language = "language",
                        LastModified = "last_modified",
                        Name = "name",
                        Owner = "owner"
                    }
                }
            };

            request.As<Customizations>()
                   .Returns(Task.FromResult(customizations));

            TextToSpeechService service =
                new TextToSpeechService(client);

            var result =
               service.ListCustomModels(language: "language");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.CustomizationsList);
            Assert.IsTrue(result.CustomizationsList.Count == 1);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void ListCustomModels_Catch_Exception()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                      .Returns(request);

            request.WithArgument(Arg.Any<string>(), Arg.Any<string>())
                   .Returns(x =>
                   {
                       throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                string.Empty));
                   });

            TextToSpeechService service =
                new TextToSpeechService(client);

            var result =
               service.ListCustomModels(language: "language");
        }

        [TestMethod]
        public void ListCustomModel_Success()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                      .Returns(request);

            var customizationWords = new CustomizationWords()
            {
                Created = "created",
                CustomizationId = "customization_id",
                Description = "description",
                Language = "language",
                LastModified = "last_modified",
                Name = "name",
                Owner = "owner",
                Words = new List<Word>()
                {
                    new Word()
                    {
                        PartOfSpeech = "part_of_speech",
                        Translation = "translation",
                        WordProperty = "word_property"
                    }
                }
            };

            request.As<CustomizationWords>()
                   .Returns(Task.FromResult(customizationWords));

            TextToSpeechService service =
                new TextToSpeechService(client);

            var customization =
               service.ListCustomModel(customizationId: "customization_id");

            Assert.IsNotNull(customization);
            Assert.IsNotNull(customization.CustomizationId);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCustomModel_With_Null_Text()
        {
            IClient client = Substitute.For<IClient>();

            TextToSpeechService service =
                new TextToSpeechService(client);

            var customization =
               service.ListCustomModel(customizationId: null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ListCustomModel_With_Empty_Text()
        {
            IClient client = Substitute.For<IClient>();

            TextToSpeechService service =
                new TextToSpeechService(client);

            var customization =
               service.ListCustomModel(customizationId: string.Empty);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void ListCustomModel_Catch_Exception()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                      .Returns(x =>
                      {
                          throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                                   Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                                   string.Empty));
                      });

            TextToSpeechService service =
                new TextToSpeechService(client);

            var customization =
               service.ListCustomModel(customizationId: "customization_id");
        }
    }
}
/*
 * Copyright (c) Facebook, Inc. and its affiliates.
 *
 * This source code is licensed under the license found in the
 * LICENSE file in the root directory of this source tree.
 */

using Facebook.WitAi.Lib;
using UnityEngine;
using UnityEngine.Events;

namespace Facebook.WitAi.CallbackHandlers
{
    public class SimpleIntentHandler : WitResponseHandler
    {
        [SerializeField] public string intent;
        [Range(0, 1f)]
        [SerializeField] private float _confidence = .9f;
        [SerializeField] private UnityEvent onIntentTriggered = new UnityEvent();

        public UnityEvent OnIntentTriggered => onIntentTriggered;

        protected override void OnHandleResponse(WitResponseNode response)
        {
            var intentNode = WitResultUtilities.GetFirstIntent(response);
            Debug.Log(response?["intents"].Count);
            var confidence = float.Parse(intentNode["confidence"].Value);
            Debug.Log($"Схваченное намерение: {intentNode["name"].Value}, пойманное слово {response["text"]}, уверенность {intentNode["confidence"]}");
          
        }
    }
}

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
        [SerializeField] private UnityEvent onIntentTriggered = new UnityEvent();

        public UnityEvent OnIntentTriggered => onIntentTriggered;

        protected override void OnHandleResponse(WitResponseNode response)
        {
            var intentNode = WitResultUtilities.GetFirstIntent(response);
            var confidence = float.Parse(intentNode["confidence"].Value);
           
          
        }
    }
}
